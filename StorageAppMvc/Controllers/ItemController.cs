using Microsoft.AspNetCore.Mvc;
using StorageAppMvc.Models;
using System.Diagnostics;
using StorageAppMvc.Domain;
using StorageAppMvc.Data;

namespace StorageAppMvc.Controllers
{
    public class ItemController : Controller
    {
        private readonly ILogger<ItemController> _logger;

        public ItemController(ILogger<ItemController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            using (var context = new StorageDb())
            {
                var itemList = context.Items.ToList();
                var containerList = context.Containers.ToList();

                ItemViewModel itemViewModel = new ItemViewModel();
                itemViewModel.Items = itemList;
                itemViewModel.Containers = containerList;
                return View(itemViewModel);
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateItem(string IName, string IDesc) 
        { 
            Item item = new Item(IName, IDesc);
            item.Create();

            return RedirectToAction("Index");
        }

        public IActionResult AddItemToContainer(int ItemId, int ContainerId)
        {
            using (var context = new StorageDb())
            {
                Item item = context.Items.First(i => i.Id == ItemId);
                Container container = context.Containers.First(c => c.Id == ContainerId);
                container.AddItem(item);

                return RedirectToAction("Index");
            }

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}