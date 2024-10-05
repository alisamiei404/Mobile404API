using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mobile404API.Data;
using Mobile404API.Models;
using System.Data;

namespace Mobile404API.Controllers.Admin
{
    [Authorize(Roles = "admin")]
    [Route("api/admin/gallery")]
    [ApiController]
    public class ManageGalleryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment env;

        public ManageGalleryController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            this._context = context;
            this.env = env;
        }

        [HttpGet("{id}")]
        public IActionResult GetAllImage(int id)
        {
            var product = _context.Products.Find(id);
            if(product == null)
            {
                return NotFound();
            }

            var images = _context.Galleries.Where(g => g.ProductId == id).ToList();
            return Ok(images);

        }

        [HttpPost("{id}")]
        public IActionResult CreateImage(int id, [FromForm] GalleryDto galleryDto)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            if (galleryDto.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "عکس برند الزامی است");
                return ValidationProblem(ModelState);
            }

            string imageFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            imageFileName += Path.GetExtension(galleryDto.ImageFile.FileName);

            string imagesFolder = env.WebRootPath + "/images/products/";

            using (var stream = System.IO.File.Create(imagesFolder + imageFileName))
            {
                galleryDto.ImageFile.CopyTo(stream);
            }

            Gallery gallery = new Gallery()
            {
                ProductId = id,
                ImageFileName = imageFileName,
                CreatedAt = DateTime.Now
            };

            _context.Galleries.Add(gallery);
            _context.SaveChanges();
            return Ok(gallery);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteImage(int id)
        {
            var image = _context.Galleries.Find(id);
            if (image == null)
            {
                return NotFound();
            }

            string imagesFolder = env.WebRootPath + "/images/products/";
            System.IO.File.Delete(imagesFolder + image.ImageFileName);

            _context.Galleries.Remove(image);
            _context.SaveChanges();

            return Ok();
        }
    }
}
