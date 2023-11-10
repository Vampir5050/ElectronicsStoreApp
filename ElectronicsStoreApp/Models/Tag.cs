using System.ComponentModel.DataAnnotations;

namespace ElectronicsStoreApp.Models
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<ProductTag> ProductTags { get; set; }
    }
}
