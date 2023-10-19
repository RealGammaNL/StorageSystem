using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Data;
using StorageAppMvc.Models;
using Domain;
using Newtonsoft.Json;
using StorageSystem.Migrations;

namespace StorageAppMvc.Controllers
{
    public class NavigationController : Controller
    {
        private readonly StorageDb _context;
        private readonly string ApiUrl = "https://localhost:7133";
        public NavbarViewModel NavbarViewModel { get; set; }
        public NavigationController(StorageDb context)
        {
            _context = context;
        }

        /// <summary>
        /// Loads containers and items on the storage page
        /// </summary>
        /// <param name="id">RoomId</param>
        /// <param name="containerId">ContainerId</param>
        /// <returns></returns>
        // GET: NavigationController
        public async Task<ActionResult> Index(int? id, int? containerId)
        {
            ItemViewModel itemViewModel = new ItemViewModel();

            //Automatically select the first ROOM if nothing is selected.
            if (id == null)
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

            //Give the RoomId to the ItemViewModel.
            itemViewModel.RoomId = id;

            //Automatically select the first CONTAINER if nothing is selected.
            if (containerId == null)
            {
                if (_context.Containers.Count() == 0)
                {
                    return RedirectToAction("Index", "Item");
                }
                else
                {
                    containerId = _context.Containers.Where(c => c.RoomId == id).FirstOrDefault().Id;
                }
            }

            itemViewModel.tableSelectedContainer = containerId;

            //Check if there are containers
            if (containerId != null) 
            {
                var itemList = _context.Items.ToList();
                List<Container> containerList = await GetContainersFromApi(id);

                itemViewModel.Items = itemList;

                foreach(Container container in containerList)
                {
                    itemViewModel.Containers.Add(container);
                }

                if (containerId != null)
                {
                    foreach (Container container in containerList)
                    {
                        if (container.Id == containerId)
                        {
                            Container containerClicked = container;
                            List<Item> items = _context.Items.Where(i => i.ContainerId == containerId).ToList(); // Search for all items that have the same ContainerId as the container, put them in a list
                            itemViewModel.SelectedContainerItems = items;
                        }
                    }
                }
            }

            this.NavbarViewModel = new NavbarViewModel();//has property PageTitle
            NavbarViewModel.Rooms = _context.Rooms.ToList();

            foreach (Room room in NavbarViewModel.Rooms)
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

        private async Task<List<Container>> GetContainersFromApi(int? id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiUrl);
                var response = await client.GetAsync($"api/Room/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var room = JsonConvert.DeserializeObject<Room>(content);
                    if (room.Containers.Count() != 0)
                    {
                        return room.Containers;
                    }
                }
            }
            return new List<Container>();
        }

        //[HttpPost]
        //public IActionResult Index(int id, int containerId)
        //{
        //    ItemViewModel itemViewModel = new ItemViewModel();

        //    Container containerClicked = _context.Containers.FirstOrDefault(c => c.Id == containerId);
        //    List<Item> items = _context.Items.Where(i => i.ContainerId == containerId).ToList(); // Search for all items that have the same ContainerId as the container, put them in a list
        //    itemViewModel.SelectedContainerItems = items;

        //    var itemList = _context.Items.ToList();
        //    var containerList = _context.Containers.ToList();
        //    itemViewModel.Items = itemList;
        //    itemViewModel.Containers = containerList;
        //    itemViewModel.tableSelectedContainer = containerClicked.Id;

        //    this.NavbarViewModel = new NavbarViewModel();//has property PageTitle
        //    NavbarViewModel.Rooms = _context.Rooms.ToList();

        //    foreach (Room room in NavbarViewModel.Rooms)
        //    {
        //        if (room.Id == id)
        //        {
        //            NavbarViewModel.selectedRoom = room;
        //            break;
        //        }
        //    }

        //    this.ViewData["NavbarViewModel"] = this.NavbarViewModel;

        //    return View(itemViewModel);
        //}

        // GET: NavigationController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ItemId">ItemId</param>
        /// <param name="RoomId">RoomId</param>
        /// <param name="ContainerId">ContainerId</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Delete(int ItemId, int RoomId, int ContainerId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiUrl);
    
                // Send the POST request with the JSON content
                var response = await client.DeleteAsync($"api/Item/{ItemId}");

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", new { id = RoomId, containerId = ContainerId });
                }
                else
                {
                    return BadRequest(response);
                }
            }
        }


        [HttpPost]
        public async Task<IActionResult> Edit(string Name, string Desc, int Quant, int id, int ContainerId, int RoomId)
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
                    return RedirectToAction("Index", new { id = RoomId, containerId = ContainerId});
                }
                else
                {
                    return BadRequest(response);
                }
            }
        }

        [HttpPost]
        public IActionResult AddItemToContainer(int ItemId, int ContainerId, int RoomId)
        {
            Item item = _context.Items.First(i => i.Id == ItemId);
            Container container = _context.Containers.First(c => c.Id == ContainerId);

            container.Items.Add(item);

            _context.Update(container);
            _context.SaveChanges();

            return RedirectToAction("Index", new { id = RoomId, containerId = ContainerId });
        }
    }
}
