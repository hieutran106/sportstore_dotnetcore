using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace SportsStore.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IProductRepository repo;
        public AdminController(IProductRepository repo)
        {
            this.repo = repo;
        }
        public IActionResult Index()
        {
            return View(repo.Products);
        }
        public ViewResult Edit(int productId) => View(repo.Products.FirstOrDefault(p => p.ProductID == productId));

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                repo.SaveProduct(product);
                TempData["message"] = $"{product.Name} has been saved";
                return RedirectToAction(nameof(Index));
            } else
            {
                return View(product);
            }
            
        }
        public ViewResult Create() => View("View", new Product());
        [HttpPost]
        public IActionResult Delete(int productId)
        {
            Product product=repo.DeleteProduct(productId);
            if (product!=null)
            {
                TempData["message"] = $"{product.Name} was deleted";
            }
            return RedirectToAction("Index");

        }
    }
}