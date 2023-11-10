using ElectronicsStoreApp.Models;

namespace ElectronicsStoreApp.ViewModels
{
    public class IndexVewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
    }
}
