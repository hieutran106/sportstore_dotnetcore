using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repo;
        public int PageSize = 4;
        public ProductController(IProductRepository repo)
        {
            this.repo = repo;
        }
        public ViewResult List(int page=1)
        {
            IEnumerable<Product> products = repo.Products.OrderBy(p => p.ProductID).Skip((page - 1) * PageSize).Take(PageSize);
            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = page,
                ItemsPerPage = PageSize,
                TotalItems = repo.Products.Count()

            };
            ProductListViewModel viewModel = new ProductListViewModel
            {
                Products = products,
                PagingInfo = pagingInfo
            };
            return View(viewModel);
        }
            
    }
}