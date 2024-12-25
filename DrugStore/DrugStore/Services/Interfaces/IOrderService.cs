using DrugStore.Data.Entities.Product;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DrugStore.Services.Interfaces
{
    public interface IOrderService
    {
        public IEnumerable<Order> GetAllOrders();
        public IEnumerable<Order> GetAllOrders(string username);
        public OrderDetail? GetOrderDetail(int orderId);
        public OrderDetail GetOrderDetail(string username);

        public void DeleteOrder(int id);
        public void DeleteOrderDetail(int id);
        public Order AddOrder(Order order);
        public void AddOrderDetail(OrderDetail orderDetail);
        public Order? GetOrderById(int id);
        public Order? GetOpenOrderByUsername(string username);
        public void UpdateOrder(Order order);

        public void UpdateOrderDetail(OrderDetail orderDetail);
        public OrderDetail? GetOrderDetailById(int id);
        public OrderDetail? GetOrderDetailByOrderAndProduct(int orderId, int productId);

        public void ChangeStatus(int id, int status);

        public int SumPrice(int orderId);
        public int SumPrice(DateTime from, DateTime to, bool isOpen = false);
        public int SumPrice();

    }
}
