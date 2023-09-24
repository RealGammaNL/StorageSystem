using Microsoft.AspNetCore.Mvc;
using StorageAppMvc.Models;
using System.Diagnostics;
using StorageAppMvc.Domain;
using StorageAppMvc.Data;
using Microsoft.EntityFrameworkCore;

namespace StorageAppMvc.Controllers
{
    public class ItemController : Controller
    {
        private readonly StorageDb _context;

        public ItemController(StorageDb context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var itemList = _context.Items.ToList(); 
            var containerList = _context.Containers.ToList();

            ItemViewModel itemViewModel = new ItemViewModel();
            itemViewModel.Items = itemList;
            itemViewModel.Containers = containerList;
            return View(itemViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateItem(string IName, string IDesc) 
        { 
            Item item = new Item(IName, IDesc);
            _context.Add(item);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult AddItemToContainer(int ItemId, int ContainerId)
        {
            Item item = _context.Items.First(i => i.Id == ItemId);
            Container container = _context.Containers.First(c => c.Id == ContainerId);
            container.AddItem(item);

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
        public IActionResult Edit(string Name, string Desc, int id) {
            Item item = _context.Items.First(i => i.Id == id);
            item.Name = Name;
            item.Description = Desc;

            _context.Update(item);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}