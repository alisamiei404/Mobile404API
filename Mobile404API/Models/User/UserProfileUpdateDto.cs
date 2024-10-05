using System.ComponentModel.DataAnnotations;

namespace Mobile404API.Models
{
    public class UserProfileUpdateDto
    {
        [Display(Name = "نام ")]
        [Required(ErrorMessage = "وارد کردن {0} الزامی می باشد.")]
        [MinLength(2, ErrorMessage = "حداقل مقدار {0} {1} حرف می باشد.")]
        [MaxLength(30, ErrorMessage = "حداکثر مقدار {0} {1} حرف می باشد.")]
        public string Name { get; set; } = "";

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "وارد کردن {0} الزامی می باشد.")]
        [MinLength(2, ErrorMessage = "حداقل مقدار {0} {1} حرف می باشد.")]
        [MaxLength(30, ErrorMessage = "حداکثر مقدار {0} {1} حرف می باشد.")]
        [EmailAddress(ErrorMessage = "ایمیل معتبر وارد کنید")]
        public string Email { get; set; } = "";
    }
}
