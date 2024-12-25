using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace DrugStore.Data.Entities.Product
{
    public class Order
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsOpen { get; set; }
        public string? TrackingCode { get; set; }
        [AllowNull]
        public DateTime? BuyDate { get; set; }
        public bool IsDelete { get; set; }

        [ForeignKey("UserId")]
        public User.User User { get; set; }
        public IEnumerable<OrderDetail?> OrderDetails { get; set; }
    }
}
