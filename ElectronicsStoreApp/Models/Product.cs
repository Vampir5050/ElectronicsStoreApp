using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectronicsStoreApp.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter a title")]
        [MaxLength(200)]
        [Display(Name = "Enter a title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Enter a description")]
        [MaxLength(10000)]
        [Display(Name = "Enter a description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Enter the model year. Min - 1990, max - 2999")]
        [Range(1990,2999)]
        [Display(Name = "Enter the model year")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Add a photo")]
        [Display(Name ="Photo url:")]
        [DataType(DataType.Upload)]
        public string ImageUrl { get; set; }
        
        public decimal Price { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        
        public Category Category { get; set; }
        public IEnumerable<ProductTag> ProductTags { get; set; }
    }
}
