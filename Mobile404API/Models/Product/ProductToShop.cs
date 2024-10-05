using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace Mobile404API.Models
{
    public class ProductToShop
    {
        public int ProductId { get; set; }
        public int ShopId { get; set; }
        public int Price { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; } = null!;

        [ForeignKey(nameof(ShopId))]
        public Shop Shop { get; set; } = null!;
    }
}
