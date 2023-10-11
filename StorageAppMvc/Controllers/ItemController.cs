using Microsoft.AspNetCore.Mvc;
using StorageAppMvc.Models;
using System.Diagnostics;
using Domain;
using StorageAppMvc.Data;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace StorageAppMvc.Controllers
{
    public class ItemController : Controller
    {
        private readonly StorageDb _context;
        private readonly string ApiUrl = "https://localhost:7133";

        public ItemController(StorageDb context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int id)
        {
            //var itemList = _context.Items.ToList();

            // Make an HTTP request to your API to get items and containers
            var apiItems = await GetItemsFromApi();
            var containerList = await GetContainersFromApi(id);

            ItemViewModel itemViewModel = new ItemViewModel();

            foreach (var apiItem in apiItems)
            {
                if (apiItem.ContainerId == null)
                {
                    itemViewModel.UnAssignedItems.Add(apiItem);
                }
            }


            itemViewModel.Items = apiItems;
            itemViewModel.Containers = containerList;
            itemViewModel.RoomId = id;

            return View(itemViewModel);
        }

        private async Task<List<Item>> GetItemsFromApi()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiUrl);
                var response = await client.GetAsync("api/Item");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var items = JsonConvert.DeserializeObject<List<Item>>(content);
                    return items;
                }
                else
                {
                    return new List<Item>();
                }
            }
        }

        private async Task<ICollection<Container>> GetContainersFromApi(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiUrl);
                var response = await client.GetAsync($"api/Room/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var room = JsonConvert.DeserializeObject<Room>(content);
                    if (room.Containers.Any())
                    {
                        return room.Containers;
                    }
                }
            }
            return new List<Container>();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateItem(string Name, string Desc) 
        { 
            Item item = new Item(Name, Desc);
            await CreateItemFromApi(item);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> CreateItemFromApi(Item item)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiUrl);

                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                // Send the POST request with the JSON content
                var response = await client.PostAsync("api/Item", content);

                if (response.IsSuccessStatusCode)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response);
                }
            }
        }

        public IActionResult AddItemToContainer(int ItemId, int ContainerId)
        {
            Item item = _context.Items.First(i => i.Id == ItemId);
            Container container = _context.Containers.First(c => c.Id == ContainerId);

            container.Items.Add(item);

            _context.Update(container);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int? id)
        {
            Item item = _context.Items.First(i => i.Id == id);
            _context.Remove(item);
            _context.SaveChanges();

            return RedirectToAction("Index");

        }

        [HttpPost]
        public async Task<IActionResult> Edit(string Name, string Desc, int id) {
            Item item = new Item(Name, Desc);
            item.Id = id;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiUrl);

                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                // Send the POST request with the JSON content
                var response = await client.PutAsync($"api/Item/{item.Id}", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return BadRequest(response);
                }
            }
        }

        public async Task<IActionResult> SelectRoom(int id)
        {
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}