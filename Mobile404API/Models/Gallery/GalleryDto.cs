using System.ComponentModel.DataAnnotations;
using testasp1.Utility;

namespace Mobile404API.Models
{
    public class GalleryDto
    {
        [Display(Name = "عکس برند")]
        [MaxFileResolution(100)]
        [AllowedExtensions(new string[] { ".jpg", ".png", ".jpeg" })]
        public IFormFile? ImageFile { get; set; }
    }
}
