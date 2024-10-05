using System.ComponentModel.DataAnnotations.Schema;

namespace Mobile404API.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? ShopId { get; set; }
        public int? ProductId { get; set; }
        public int Count { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; } = null!;
        public Shop Shop { get; set; } = null!;
    }
}
