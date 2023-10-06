using StorageAppMvc.Domain;

namespace StorageAppMvc.Models
{
    public class ItemViewModel
    {
        public List<Item>? Items { get; set; }
        public List<Container>? Containers { get; set; }
        public List<Item>? SelectedContainerItems { get; set; } = new List<Item>();
        public int SelectedContainer { get; set; }
        public int SelectedItem { get; set; }
    }
}
