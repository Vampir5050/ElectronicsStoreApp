using System.ComponentModel.DataAnnotations;

namespace ElectronicsStoreApp.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter a title")]
        [MaxLength(50)]
        [Display(Name = "Enter a title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Enter a description")]
        [MaxLength(50)]
        [Display(Name = "Enter a description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Enter the model year. Min - 1990, max - 2999")]
        [Range(1990,2999)]
        [Display(Name = "Enter the model year")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Add a photo")]
        [Display(Name ="Photo url:")]
        public string ImageUrl { get; set; }
    }
}
