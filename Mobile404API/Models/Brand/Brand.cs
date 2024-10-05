using Microsoft.EntityFrameworkCore;

namespace Mobile404API.Models
{
    [Index("Name", IsUnique = true)]
    [Index("Slug", IsUnique = true)]
    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Slug { get; set; } = "";
        public string ImageFileName { get; set; } = "";
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
