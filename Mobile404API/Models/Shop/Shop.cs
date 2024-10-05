using System.ComponentModel.DataAnnotations.Schema;

namespace Mobile404API.Models
{
    public class Shop
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;

        public ICollection<ProductToShop> ProductToShops { get; set; } = new List<ProductToShop>();
    }
}
