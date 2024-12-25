using DrugStore.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DrugStore.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IOrderService _orderService;

        public HomeController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public IActionResult Index()
        {
            var model = _orderService.GetAllOrders()
                .OrderByDescending(order => order.Id)
                .Where(order => order.User.UserName == User.Identity.Name);
            return View(model);
        }
    }
}
