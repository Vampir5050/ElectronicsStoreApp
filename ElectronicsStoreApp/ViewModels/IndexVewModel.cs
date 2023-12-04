using ElectronicsStoreApp.Models;
using Microsoft.Extensions.Hosting;

namespace ElectronicsStoreApp.ViewModels
{
    public class IndexVewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
        public IEnumerable<Product> RecentPosts { get; set; }
        public int CurrentPages { get; set; }
        public int? SelectedCategoryId { get; set; }
        public int? SelectedTagId { get; set; }
        public int TotalPages { get; set; }
        public int LimitPage { get; set; } = 3;

    }
}
