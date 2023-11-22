﻿using ElectronicsStoreApp.Data;
using ElectronicsStoreApp.Data.Extensions;
using ElectronicsStoreApp.Helpers;
using ElectronicsStoreApp.Models;
using ElectronicsStoreApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.categories = new SelectList(_context.Categories, "Id", "Name");
            ViewBag.tags = new SelectList(_context.Tags, "Id", "Name");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Product product, IFormFile Image, int[]tags)
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
                await _context.ProductTags.AddRangeAsync(tags.Select(x => new ProductTag { ProductId = product.Id, TagId = x }));
                await _context.SaveChangesAsync();
                return RedirectToAction("shop");
            }
            catch (Exception)
            {
                return View();
            }

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _context.Products.FindAsync(id);
            ViewBag.categories = new SelectList(_context.Categories, "Id", "Name");
            var selectTags = _context.ProductTags.Where(x => x.ProductId == id).Select(x => x.TagId);
            ViewBag.tags = new MultiSelectList(_context.Tags, "Id", "Name", selectTags);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Product product, IFormFile Image, int[]tags)
        {
            if(Image != null)
            {
                var path = await FileUploadHelper.UploadAsync(Image);
                product.ImageUrl = path;
            }
            _context.Products.Update(product);
            await _context.SaveChangesAsync();

            var productWithTags = await _context.Products.Include(x => x.ProductTags).FirstOrDefaultAsync(x => x.Id == product.Id);
            _context.UpdateManyToMany( productWithTags.ProductTags,tags.Select(x => new ProductTag { ProductId = product.Id, TagId = x }),x => x.TagId);
            await _context.SaveChangesAsync();
            return RedirectToAction("Shop");

        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var product = await _context.Products.Include(x => x.ProductTags).ThenInclude(x => x.Tag).Include(x => x.Category).FirstOrDefaultAsync(product => product.Id == id);
            return View(product);
        }

    }
}
