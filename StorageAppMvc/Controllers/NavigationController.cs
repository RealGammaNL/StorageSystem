using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Data;
using StorageAppMvc.Models;
using Domain;
using Newtonsoft.Json;

namespace StorageAppMvc.Controllers
{
    public class NavigationController : Controller
    {
        private readonly StorageDb _context;
        private readonly string ApiUrl = "https://localhost:7133";
        public NavigationController(StorageDb context)
        {
            _context = context;
        }

        // GET: NavigationController
        public ActionResult Index(int? id)
        {
            ItemViewModel itemViewModel = new ItemViewModel();
            var itemList = _context.Items.ToList();
            var containerList = _context.Containers.ToList();
            itemViewModel.Items = itemList;
            itemViewModel.Containers = containerList;

            if (id != null)
            {
                Container containerClicked = _context.Containers.FirstOrDefault(c => c.Id == id);
                List<Item> items = _context.Items.Where(i => i.ContainerId == id).ToList(); // Search for all items that have the same ContainerId as the container, put them in a list
                itemViewModel.SelectedContainerItems = items;
                itemViewModel.tableSelectedContainer = containerClicked.Id;
            }

            return View(itemViewModel);
        }

        [HttpPost]
        public IActionResult Index(int id)
        {
            ItemViewModel itemViewModel = new ItemViewModel();

            Container containerClicked = _context.Containers.FirstOrDefault(c => c.Id == id);
            List<Item> items = _context.Items.Where(i => i.ContainerId == id).ToList(); // Search for all items that have the same ContainerId as the container, put them in a list
            itemViewModel.SelectedContainerItems = items;

            var itemList = _context.Items.ToList();
            var containerList = _context.Containers.ToList();
            itemViewModel.Items = itemList;
            itemViewModel.Containers = containerList;
            itemViewModel.tableSelectedContainer = containerClicked.Id;

            return View(itemViewModel);
        }

        // GET: NavigationController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, int containerId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiUrl);
    
                // Send the POST request with the JSON content
                var response = await client.DeleteAsync($"api/Item/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", new { id = containerId });
                }
                else
                {
                    return BadRequest(response);
                }
            }
        }


        [HttpPost]
        public async Task<IActionResult> Edit(string Name, string Desc, int Quant, int id, int containerId)
        {
            Item item = new Item(Name, Desc, Quant);
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
                    return RedirectToAction("Index", new { id = containerId });
                }
                else
                {
                    return BadRequest(response);
                }
            }
        }

        [HttpPost]
        public IActionResult AddItemToContainer(int ItemId, int ContainerId)
        {
            Item item = _context.Items.First(i => i.Id == ItemId);
            Container container = _context.Containers.First(c => c.Id == ContainerId);

            container.Items.Add(item);

            _context.Update(container);
            _context.SaveChanges();

            return RedirectToAction("Index", new {id = ContainerId});
        }
    }
}
