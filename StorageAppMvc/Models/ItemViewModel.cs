using Domain;

namespace StorageAppMvc.Models
{
    public class ItemViewModel
    {
        public List<Item>? Items { get; set; }
        public List<Container> Containers { get; set; } = new List<Container>();
        public List<Item>? SelectedContainerItems { get; set; } = new List<Item>();
        public List<Item>? UnAssignedItems { get; set; } = new List<Item>();
        public int tableSelectedContainer { get; set; }
        public int SelectedContainer { get; set; }
        public int SelectedItem { get; set; }
        public int RoomId { get; set; }
    }
}
