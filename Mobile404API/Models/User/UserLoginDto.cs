using System.ComponentModel.DataAnnotations;

namespace Mobile404API.Models
{
    public class UserLoginDto
    {
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "وارد کردن {0} الزامی می باشد.")]
        [MinLength(2, ErrorMessage = "حداقل مقدار {0} {1} حرف می باشد.")]
        [MaxLength(30, ErrorMessage = "حداکثر مقدار {0} {1} حرف می باشد.")]
        [EmailAddress(ErrorMessage = "ایمیل معتبر وارد کنید")]
        public string Email { get; set; } = "";

        [Display(Name = " رمز عبور")]
        [Required(ErrorMessage = "وارد کردن {0} الزامی می باشد.")]
        [MinLength(4, ErrorMessage = "حداقل مقدار {0} {1} حرف می باشد.")]
        [MaxLength(30, ErrorMessage = "حداکثر مقدار {0} {1} حرف می باشد.")]
        public string Password { get; set; } = "";
    }
}
