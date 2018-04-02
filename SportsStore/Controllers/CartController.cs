using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Infrastructure;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers
{
    public class CartController : Controller
    {

        private IProductRepository repo;
        private Cart cart;
        public CartController(IProductRepository repo, Cart cartService)
        {
            this.repo = repo;
            this.cart = cartService;
        }
        public ViewResult Index(string returnUrl)
            => View(new CartIndexViewModel { Cart = cart, ReturnUrl = returnUrl });
        public RedirectToActionResult AddToCart(int productId, string returnUrl)
        {
            Product product = repo.Products.FirstOrDefault(p => p.ProductID == productId);
            if (product!=null)
            {    
                cart.AddItem(product, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        public RedirectToActionResult RemoveFromCart(int productId, string returnUrl)
        {
            Product product = repo.Products
                                .FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
    }
}