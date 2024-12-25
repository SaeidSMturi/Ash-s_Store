using DrugStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DrugStore.Components
{
    public class ProductComponent : ViewComponent
    {
        readonly IProductService _productService;

        public ProductComponent(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var products = await _productService.GetAllProductsAsync();
            return View(products.Take(6));
        }
    }
}
