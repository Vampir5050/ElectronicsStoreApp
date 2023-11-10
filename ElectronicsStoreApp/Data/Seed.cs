using ElectronicsStoreApp.Models;

namespace ElectronicsStoreApp.Data
{
    public class Seed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using(var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                context.Database.EnsureCreated();

                if(!context.Categories.Any())
                {
                    context.Categories.Add(new Category() { Name = "Smartphones" });
                    context.Categories.Add(new Category() { Name = "TV" });
                    context.Categories.Add(new Category() { Name = "Consoles" });
                    context.Categories.Add(new Category() { Name = "Drier" });
                    context.Categories.Add(new Category() { Name = "Laptops" });
                    context.SaveChanges();
                }
                if(!context.Tags.Any())
                {
                    context.Tags.Add(new Tag() { Name = "Gaming" });
                    context.Tags.Add(new Tag() { Name = "MacBook" });
                    context.Tags.Add(new Tag() { Name = "SmartTV" });
                    context.Tags.Add(new Tag() { Name = "4K UHD" });
                    context.Tags.Add(new Tag() { Name = "Android" });
                    context.Tags.Add(new Tag() { Name = "iOS" });
                    context.SaveChanges();
                }
            }
        }
    }
}
