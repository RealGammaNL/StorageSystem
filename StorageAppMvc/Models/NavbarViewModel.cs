using Domain;

namespace StorageAppMvc.Models
{
    public class NavbarViewModel
    {
        public List<Room> Rooms { get; set; }
        public Room selectedRoom { get; set; }
    }
}
