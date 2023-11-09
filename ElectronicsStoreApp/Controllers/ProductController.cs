using ElectronicsStoreApp.Data;
using ElectronicsStoreApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicsStoreApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;

        public ProductController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Shop()
        {
            var post = applicationDbContext.Products;
            return View(post);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Product product)
        {
            if(ModelState.IsValid)
            {
                TempData["status"] = "New product added!";
                await applicationDbContext.Products.AddAsync(product);
                await applicationDbContext.SaveChangesAsync();
                return RedirectToAction("shop");
            }
            return View(product);
        }
      
    }
}
