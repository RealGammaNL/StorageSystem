using Microsoft.AspNetCore.Mvc;
using StorageAppMvc.Models;
using System.Diagnostics;
using Domain;
using Domain.Data;

namespace StorageAppMvc.Controllers
{
    public class ContainerController : Controller
    {
        private readonly StorageDb _context;

        public ContainerController(StorageDb context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateContainer(string Name, string Desc, int RoomId)
        {
            Container container = new Container(Name, Desc);
            container.RoomId = RoomId;
            _context.Add(container);
            _context.SaveChanges();

            return RedirectToAction("Index", "Item", new { id = RoomId});
        }

        [HttpPost]
        public IActionResult Delete(int? id, int RoomId)
        {
            Container container = _context.Containers.First(c => c.Id == id); // Find the container that is has this Id
            List<Item> items = _context.Items.Where(i => i.ContainerId == id).ToList(); // Search for all items that have the same ContainerId as the container, put them in a list

            foreach (Item item in items)
            {
                item.ContainerId = null; // De-asign all the items 
                _context.Update(item); // Update the changes
            }

            _context.Remove(container); 
            _context.SaveChanges();

            return RedirectToAction("Index", "Item", new { id = RoomId });
        }

        public IActionResult Edit(string Name, string Desc, int id, int RoomId)
        {
            Container container = _context.Containers.First(c => c.Id == id);
            container.Name = Name;
            container.Description = Desc;

            _context.Update(container);
            _context.SaveChanges();

            return RedirectToAction("Index", "Item", new { id = RoomId });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}