using System.ComponentModel.DataAnnotations.Schema;

namespace DrugStore.Data.Entities.Product
{
    public class OrderDetail
    {
        public int id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }

}
