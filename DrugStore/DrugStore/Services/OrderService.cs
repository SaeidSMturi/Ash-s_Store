using DrugStore.Data;
using DrugStore.Data.Entities.Product;
using DrugStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DrugStore.Services
{
    public class OrderService : IOrderService
    {
        WebContext _context;

        public OrderService(WebContext context)
        {
            _context = context;
        }
        public IEnumerable<Order> GetAllOrders()
        {
            return _context.Orders.Include(o => o.OrderDetails).ThenInclude(o => o.Product)
                .Include(order => order.User);
        }

        public IEnumerable<Order> GetAllOrders(string username)
        {
            return _context.Orders.Include(o => o.OrderDetails)
                .Include(order => order.User).ThenInclude(o => o.Products).Where(o => o.User.UserName == username);
        }


        public OrderDetail? GetOrderDetail(int orderId)
        {
            return _context.OrderDetails.Include(o => o.Order).ThenInclude(o => o.User).SingleOrDefault(o => o.id == orderId);
        }

        public OrderDetail GetOrderDetail(string username)
        {
            throw new NotImplementedException();
        }

        public void DeleteOrder(int id)
        {
            Order? order = GetOrderById(id);
            _context.Orders.Remove(order);
            _context.SaveChanges();
        }

        public void DeleteOrderDetail(int id)
        {
            OrderDetail detail = GetOrderDetail(id);
            int orderId = detail.OrderId;
            _context.OrderDetails.Remove(detail);
            _context.SaveChanges();
            //if (!detail.Order.OrderDetails.Any())
            //{
            //    DeleteOrder(orderId);
            //}
        }

        public Order AddOrder(Order order)
        {
            _context.Add(order);
            _context.SaveChanges();

            return order;
        }
        public void AddOrderDetail(OrderDetail orderDetail)
        {
            _context.Add(orderDetail);
            _context.SaveChanges();
        }

        public Order? GetOrderById(int id)
        {
            return _context.Orders.Include(o => o.OrderDetails)
                .ThenInclude(o => o.Product)
                
                .Include(order => order.User).SingleOrDefault(o => o.Id == id);
        }

        public Order? GetOpenOrderByUsername(string username)
        {
            return _context.Orders.Include(o => o.OrderDetails).ThenInclude(o => o.Product).Include(o => o.User).SingleOrDefault(o => o.User.UserName == username && o.IsOpen);
        }

        public void UpdateOrder(Order order)
        {
            try
            {
                _context.Orders.Update(order);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void UpdateOrderDetail(OrderDetail orderDetail)
        {
            _context.OrderDetails.Update(orderDetail);
            _context.SaveChanges();
        }

        public OrderDetail? GetOrderDetailById(int id)
        {
            return _context.OrderDetails.Find(id);
        }

        public OrderDetail? GetOrderDetailByOrderAndProduct(int orderId, int productId)
        {
            return _context.OrderDetails.SingleOrDefault(d => d.OrderId == orderId && d.ProductId == productId);
        }

      public void ChangeStatus(int id, int status)
        {
            try
            {
                Order order = _context.Orders.Find(id);
                if (order != null)
                {
                    UpdateOrder(order);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        public int SumPrice(int orderId)
        {
            Order? order = _context.Orders.Include(d => d.OrderDetails).SingleOrDefault(d => d.Id == orderId);

            int sum = 0;
            foreach (var detail in order.OrderDetails)
            {
                sum += detail.Price * detail.Count;
            }
            return sum;
        }

        public int SumPrice(DateTime from, DateTime to, bool isOpen = false)
        {
            if (from >= to)
                return 0;
            IEnumerable<Order> orders = _context.Orders
                .Include(d => d.OrderDetails)
                .Where(o => o.CreateDate >= from && o.CreateDate <= to && o.IsOpen == isOpen).ToList();

            int sum = 0;
            foreach (var order in orders)
            {
                foreach (var detail in order.OrderDetails)
                {
                    sum += detail.Price * detail.Count;
                }
            }

            return sum;
        }

        public int SumPrice()
        {
            IEnumerable<Order> orders = _context.Orders.Include(d => d.OrderDetails).ToList();

            int sum = 0;
            foreach (var order in orders)
            {
                foreach (var detail in order.OrderDetails)
                {
                    sum += detail.Price * detail.Count;
                }
            }

            return sum;
        }
    }
}
