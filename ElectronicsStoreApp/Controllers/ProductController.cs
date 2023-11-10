using ElectronicsStoreApp.Data;
using ElectronicsStoreApp.Helpers;
using ElectronicsStoreApp.Models;
using ElectronicsStoreApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;

namespace ElectronicsStoreApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.categories = new SelectList(_context.Categories, "Id", "Name");
            ViewBag.tags = new SelectList(_context.Tags, "Id", "Name");
            return View();
        }

        [HttpGet]
        public ActionResult Shop()
        {
            IEnumerable<Product> products = _context.Products.ToList();
            IEnumerable<Category> categories = _context.Categories.ToList();
            IEnumerable<Tag> tags = _context.Tags.ToList();

            foreach (var item in products)
            {
                if (item.Description.Length>100)
                {
                    int lastSpace = item.Description.LastIndexOf(' ', 100);
                    if (lastSpace > 0)
                    {
                        item.Description = item.Description.Substring(0, lastSpace);
                    }
                    else
                    {
                        item.Description = item.Description.Substring(0, 100);
                    }
                }
            }

            IndexVewModel model = new IndexVewModel() { Categories = categories, Products = products, Tags = tags };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Product product, IFormFile Image)
        {
            try
            {
                product.ImageUrl = await FileUploadHelper.UploadAsync(Image);
                if (Image != null)
                {
                    var filename = $"{Guid.NewGuid()}{Path.GetExtension(Image.FileName)}";
                    using var fs = new FileStream(@$"wwwroot/uploads/{filename}", FileMode.Create);
                    await Image.CopyToAsync(fs);
                    product.ImageUrl = @$"/uploads/{filename}";

                }
                TempData["status"] = "New product added!";
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
                return RedirectToAction("shop");
            }
            catch (Exception)
            {
                return View();
            }

        }

    }
}
