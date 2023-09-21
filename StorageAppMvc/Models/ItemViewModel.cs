using StorageAppMvc.Domain;

namespace StorageAppMvc.Models
{
    public class ItemViewModel
    {
        public List<Item>? Items { get; set; }
        public List<Container>? Containers { get; set; }
        public int SelectedContainer { get; set; }
        public int SelectedItem { get; set; }
    }
}
