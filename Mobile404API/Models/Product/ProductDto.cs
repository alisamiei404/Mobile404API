using System.ComponentModel.DataAnnotations;

namespace Mobile404API.Models
{
    public class ProductDto
    {
        [Required(ErrorMessage = "وارد کردن {0} الزامی می باشد.")]
        public int BrandId { get; set; }

        [Required(ErrorMessage = "وارد کردن {0} الزامی می باشد.")]
        [MinLength(2, ErrorMessage = "حداقل مقدار {0} {1} حرف می باشد.")]
        [MaxLength(150, ErrorMessage = "حداکثر مقدار {0} {1} حرف می باشد.")]
        public string Title { get; set; } = string.Empty;
        public int Ram { get; set; }
        public int Hard { get; set; }
        public int Camera { get; set; }

        [MinLength(2, ErrorMessage = "حداقل مقدار {0} {1} حرف می باشد.")]
        [MaxLength(2000, ErrorMessage = "حداکثر مقدار {0} {1} حرف می باشد.")]
        public string? Description { get; set; }
    }
}
