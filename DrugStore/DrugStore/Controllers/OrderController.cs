using DrugStore.Data.Entities.Product;
using DrugStore.Data.Entities.User;
using DrugStore.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DrugStore.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        IOrderService _orderService;
        IProductService _productService;
        IUserService _userService;

        public OrderController(IOrderService orderService, IProductService productService, IUserService userService)
        {
            _orderService = orderService;
            _productService = productService;
            _userService = userService;
        }

        [Route("/Cart")]
        public IActionResult Index()
        {
            var order = _orderService.GetOpenOrderByUsername(User.Identity?.Name);

            return View(order);
        }

        [Route("/Order/{id}")]
        public async Task<IActionResult> AddOrderToCart(int id)
        {
            User? user = await _userService.FindUserWithUsernameAsync(User.Identity?.Name);
            if (user == null)
            {
                return NotFound();
            }

            Product? product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            Order? userOrder = _orderService.GetOpenOrderByUsername(user.UserName);
            if (userOrder == null)
            {
                Order order = new Order()
                {
                    UserId = user.UserId,
                    CreateDate = DateTime.Now,
                    IsOpen = true
                };

                userOrder = _orderService.AddOrder(order);
            }

            var details = _orderService.GetOrderDetailByOrderAndProduct(userOrder.Id, id);

            if (details == null)
            {
                OrderDetail orderDetail = new OrderDetail()
                {
                    Count = 1,
                    ProductId = id,
                    Price = product.Price,
                    OrderId = userOrder.Id
                };
                _orderService.AddOrderDetail(orderDetail);
            }
            else
            {
                details.Count += 1;
                _orderService.UpdateOrderDetail(details);
            }

            return RedirectToAction("Index");
        }

        public IActionResult RemoveOrderDetail(int id)
        {
            OrderDetail? detail = _orderService.GetOrderDetail(id);
            if (detail == null || detail.Order.User.UserName != User.Identity?.Name)
            {
                return NotFound();
            }

            if (detail.Count > 1)
            {
                detail.Count--;
                _orderService.UpdateOrderDetail(detail);
            }
            else
            {
                _orderService.DeleteOrderDetail(id);
            }

            return RedirectToAction("Index");
        }

        [Route("/Order/Payment")]
        public IActionResult Payment()
        {
            var order = _orderService.GetOpenOrderByUsername(User.Identity?.Name);
            if (order == null)
                return NotFound();

            order.IsOpen = false;
            order.BuyDate = DateTime.Now;
            _orderService.UpdateOrder(order);
            return View();
        }
    }
}
