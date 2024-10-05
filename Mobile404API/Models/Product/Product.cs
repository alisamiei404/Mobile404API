using System.ComponentModel.DataAnnotations.Schema;

namespace Mobile404API.Models
{
    public class Product
    {
        public int Id { get; set; }
        public int BrandId { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Ram { get; set; }
        public int Hard { get; set; }
        public int Camera { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [ForeignKey(nameof(BrandId))]
        public Brand Brand { get; set; } = null!;
        public ICollection<Gallery> Galleries { get; set; } = new List<Gallery>();
        public ICollection<ProductToShop> ProductToShops { get; set; } = new List<ProductToShop>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

    }
}
