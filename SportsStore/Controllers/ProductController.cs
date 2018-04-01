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
        public ViewResult List(string category, int productPage=1)
        {
            IEnumerable<Product> products = repo.Products
                .Where(p => category==null || p.Category==category)
                .OrderBy(p => p.ProductID)
                .Skip((productPage - 1) * PageSize)
                .Take(PageSize);
            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = productPage,
                ItemsPerPage = PageSize,
                TotalItems = category == null ? repo.Products.Count() : repo.Products.Where(p => p.Category == category).Count()
            };
            ProductListViewModel viewModel = new ProductListViewModel
            {
                Products = products,
                PagingInfo = pagingInfo,
                CurrentCategory=category
            };
            return View(viewModel);
        }
            
    }
}