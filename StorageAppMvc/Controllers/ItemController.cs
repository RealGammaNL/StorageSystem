using Domain;
using Domain.Data;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StorageAppMvc.Models;
using System.Diagnostics;

namespace StorageAppMvc.Controllers
{
    public class ItemController : Controller
    {
        private readonly StorageDb _context;
        private readonly string ApiUrl = "https://localhost:7133";
        public NavbarViewModel NavbarViewModel { get; set; }

        public ItemController(StorageDb context)
        {
            _context = context;
        }

        /// <summary>
        /// Loads the page. Containers vary by room, items are universal.
        /// </summary>
        /// <param name="id">RoomId</param>
        /// <returns></returns>
        public async Task<IActionResult> Index(int id)
        {
            //var itemList = _context.Items.ToList();

            //Automatically select the first room if nothing is selected.
            if (id == 0)
            {
                if (_context.Rooms.Count() == 0)
                {
                    return RedirectToAction("Index", "Rooms");
                }
                else
                {
                    id = _context.Rooms.FirstOrDefault().Id;
                }
            }

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


            this.NavbarViewModel = new NavbarViewModel();//has property PageTitle
            NavbarViewModel.Rooms = _context.Rooms.ToList();

            foreach(Room room in NavbarViewModel.Rooms)
            {
                if (room.Id == id)
                {
                    NavbarViewModel.selectedRoom = room;
                    break;
                }
            }

            this.ViewData["NavbarViewModel"] = this.NavbarViewModel;

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

        private async Task<List<Container>> GetContainersFromApi(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiUrl);
                var response = await client.GetAsync($"api/Room/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var room = JsonConvert.DeserializeObject<Room>(content);
                    if (room != null && room.Containers.Count() != 0)
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
        public async Task<IActionResult> CreateItem(string Name, string Desc, int RoomId) 
        { 
            Item item = new Item(Name, Desc);
            await CreateItemFromApi(item);
            return RedirectToAction("Index", new { id = RoomId });
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

        public IActionResult AddItemToContainer(int ItemId, int ContainerId, int RoomId)
        {
            Item item = _context.Items.First(i => i.Id == ItemId);
            Container container = _context.Containers.First(c => c.Id == ContainerId);

            container.Items.Add(item);

            _context.Update(container);
            _context.SaveChanges();

            return RedirectToAction("Index", new { id = RoomId });
        }

        [HttpPost]
        public IActionResult Delete(int? id, int RoomId)
        {
            Item item = _context.Items.First(i => i.Id == id);
            _context.Remove(item);
            _context.SaveChanges();

            return RedirectToAction("Index", new { id = RoomId });

        }

        [HttpPost]
        public async Task<IActionResult> Edit(string Name, string Desc, int id, int RoomId) {
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
                    return RedirectToAction("Index", new { id = RoomId });
                }
                else
                {
                    return BadRequest(response);
                }
            }
        }

        //public async Task<IActionResult> SelectRoom(int id)
        //{
        //    return RedirectToAction("Index");
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}