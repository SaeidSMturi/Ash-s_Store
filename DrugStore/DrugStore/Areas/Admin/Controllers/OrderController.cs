using DrugStore.Data.Entities.Product;
using DrugStore.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DrugStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            var model = _orderService.GetAllOrders().OrderByDescending(order => order.Id);
            return View(model);
        }

        public IActionResult Details(int id)
        {
            var model = _orderService.GetOrderById(id);
            if (model==null)
            {
                return NotFound();
            }
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            var model = _orderService.GetOrderById(id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(int id, Order order)
        {
            _orderService.DeleteOrder(id);
            return RedirectToAction("Index","Order",new {area="Admin"});
        }


        public IActionResult Edit(int id)
        {
            var model = _orderService.GetOrderById(id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(int id, Order order)
        {
            _orderService.UpdateOrder(order);
            return RedirectToAction("Index", "Order", new { area = "Admin" });
        }
    }
}
