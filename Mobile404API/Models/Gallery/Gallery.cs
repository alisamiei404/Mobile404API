using System.ComponentModel.DataAnnotations.Schema;

namespace Mobile404API.Models
{
    public class Gallery
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ImageFileName { get; set; } = "";
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; } = null!;
    }
}
