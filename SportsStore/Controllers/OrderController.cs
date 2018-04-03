using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Controllers
{
    public class OrderController : Controller
    {
        private IOrderRepository repo;
        private Cart cart;
        public OrderController(IOrderRepository repo, Cart cart)
        {
            this.repo = repo;
            this.cart = cart;
        }
        public ViewResult List()
        {
            return View(repo.Orders.Where(o => !o.Shipped));
        }
        public IActionResult MarkShipped(int orderId)
        {
            Order order = repo.Orders.FirstOrDefault(o => o.OrderID == orderId);
            if (order !=null)
            {
                order.Shipped = true;
                repo.SaveOrder(order);
            }
            return RedirectToAction(nameof(List));
        }
        
        public ViewResult Checkout() => View(new Order());
        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            if (cart.Lines.Count()==0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty");
            }
            if (ModelState.IsValid)
            {
                order.Lines = cart.Lines.ToArray();
                repo.SaveOrder(order);
                return RedirectToAction(nameof(Completed));
            } else
            {
                return View(order);
            }
        }
        public ViewResult Completed()
        {
            cart.Clear();
            return View();
        }
    }
}