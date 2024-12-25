using DrugStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DrugStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _productService.GetAllProductsAsync();
            return View(model);
        }

        [Route("/Product/{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var model = await _productService.GetProductByIdAsync(id);
            if (model == null)
                return NotFound();
            return View(model);
        }
    }
}
