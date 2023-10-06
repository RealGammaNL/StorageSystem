using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StorageAppMvc.Data;
using StorageAppMvc.Models;
using StorageAppMvc.Domain;

namespace StorageAppMvc.Controllers
{
    public class NavigationController : Controller
    {
        private readonly StorageDb _context;

        public NavigationController(StorageDb context)
        {
            _context = context;
        }

        // GET: NavigationController
        public ActionResult Index()
        {
            ItemViewModel itemViewModel = new ItemViewModel();
            var itemList = _context.Items.ToList();
            var containerList = _context.Containers.ToList();
            itemViewModel.Items = itemList;
            itemViewModel.Containers = containerList;

            return View(itemViewModel);
        }

        [HttpPost]
        public ActionResult Index(int id)
        {
            ItemViewModel itemViewModel = new ItemViewModel();

            Container containerClicked = _context.Containers.FirstOrDefault(c => c.Id == id);
            List<Item> items = _context.Items.Where(i => i.ContainerId == id).ToList(); // Search for all items that have the same ContainerId as the container, put them in a list
            itemViewModel.SelectedContainerItems = items;

            var itemList = _context.Items.ToList();
            var containerList = _context.Containers.ToList();
            itemViewModel.Items = itemList;
            itemViewModel.Containers = containerList;

            return View(itemViewModel);
        }

        // GET: NavigationController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
    }
}
