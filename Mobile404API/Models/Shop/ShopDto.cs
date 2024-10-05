using System.ComponentModel.DataAnnotations;

namespace Mobile404API.Models
{
    public class ShopDto
    {

        [Display(Name = "نام فروشگاه")]
        [Required(ErrorMessage = "وارد کردن {0} الزامی می باشد.")]
        [MinLength(2, ErrorMessage = "حداقل مقدار {0} {1} حرف می باشد.")]
        [MaxLength(20, ErrorMessage = "حداکثر مقدار {0} {1} حرف می باشد.")]
        public string Name { get; set; } = string.Empty;
    }
}
