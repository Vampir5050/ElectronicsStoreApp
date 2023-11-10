using System.ComponentModel.DataAnnotations;

namespace ElectronicsStoreApp.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        
        [MaxLength(50)]
        public string Name { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
