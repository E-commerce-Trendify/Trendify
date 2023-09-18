using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Trendify.DTOs;
using Trendify.Interface;
using Trendify.Models;
using Trendify.Services;

namespace Trendify.Pages
{
    public class ProductsModel : PageModel
    {
        private readonly IProducts productsService;
        public List<ProductsDtoView> Products { get; set; }
        public ProductsModel(IProducts service)
        {
            productsService = service;
        }

        
        public async Task OnGet()
        {
            Products = await productsService.GetAllProducts();  
        }
    }
}
