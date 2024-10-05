using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Mobile404API.Models
{
    [Index("Email", IsUnique = true)]
    public class PasswordReset
    {
        public int Id { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "وارد کردن {0} الزامی می باشد.")]
        [MinLength(2, ErrorMessage = "حداقل مقدار {0} {1} حرف می باشد.")]
        [MaxLength(30, ErrorMessage = "حداکثر مقدار {0} {1} حرف می باشد.")]
        public string Email { get; set; } = "";

        [Display(Name = "کد")]
        [Required(ErrorMessage = "وارد کردن {0} الزامی می باشد.")]
        [MinLength(2, ErrorMessage = "حداقل مقدار {0} {1} حرف می باشد.")]
        [MaxLength(20, ErrorMessage = "حداکثر مقدار {0} {1} حرف می باشد.")]
        public int Code { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
