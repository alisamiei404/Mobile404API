using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mobile404API.Data;
using Mobile404API.Models;

namespace Mobile404API.Controllers.Admin
{
    [Authorize(Roles = "admin")]
    [Route("api/admin/brand")]
    [ApiController]
    public class ManageBrandController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment env;

        public ManageBrandController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            this._context = context;
            this.env = env;
        }

        [HttpGet]
        public IActionResult GetAllBrand(int? page)
        {
            if (page == null || page < 1) page = 1;
            int pageSize = 2;
            int totalPages = 0;

            decimal count = _context.Brands.Count();
            totalPages = (int)Math.Ceiling(count / pageSize);

            var brands = _context.Brands
                .OrderByDescending(u => u.Id)
                .Skip((int)(page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var response = new
            {
                brands = brands,
                totalPages = totalPages,
                pageSize = pageSize,
                page = page
            };

            return Ok(response);
        }

        [HttpGet("list")]
        public IActionResult GetBrands()
        {
            var brands = _context.Brands.ToList();
            return Ok(brands);
        }

        [HttpGet("{slug}")]
        public IActionResult GetBrand(string slug)
        {
            var brand = _context.Brands.FirstOrDefault(b => b.Slug == slug);
            if(brand == null)
            {
                return NotFound();
            }

            return Ok(brand);
        }

        [HttpPost]
        public IActionResult CreateBrand([FromForm]BrandDto brandDto)
        {
            var b1 = _context.Brands.FirstOrDefault(b => b.Name == brandDto.Name);
            if (b1 != null)
            {
                ModelState.AddModelError("Name", "این نام وجود دارد");
                return ValidationProblem(ModelState);
            }

            var b2 = _context.Brands.FirstOrDefault(b => b.Slug == brandDto.Slug);
            if (b2 != null)
            {
                ModelState.AddModelError("Slug", "این اسلاگ وجود دارد");
                return ValidationProblem(ModelState);
            }

            if (brandDto.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "عکس برند الزامی است");
                return ValidationProblem(ModelState);
            }

            string imageFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            imageFileName += Path.GetExtension(brandDto.ImageFile.FileName);

            string imagesFolder = env.WebRootPath + "/images/brands/";

            using (var stream = System.IO.File.Create(imagesFolder + imageFileName))
            {
                brandDto.ImageFile.CopyTo(stream);
            }

            Brand brand = new Brand()
            {
                Name = brandDto.Name,
                Slug = brandDto.Slug,
                ImageFileName = imageFileName,
                IsActive = brandDto.IsActive,
                CreatedAt = DateTime.Now
            };

            _context.Brands.Add(brand);
            _context.SaveChanges();
            return Ok(brand);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBrand(int id, [FromForm] BrandDto brandDto)
        {
            var brand = _context.Brands.Find(id);
            if (brand == null)
            {
                return NotFound();
            }

            var b1 = _context.Brands.FirstOrDefault(b => b.Name == brandDto.Name && b.Id != id);
            if (b1 != null)
            {
                ModelState.AddModelError("Name", "این نام وجود دارد");
                return ValidationProblem(ModelState);
            }

            var b2 = _context.Brands.FirstOrDefault(b => b.Slug == brandDto.Slug && b.Id != id);
            if (b2 != null)
            {
                ModelState.AddModelError("Slug", "این اسلاگ وجود دارد");
                return ValidationProblem(ModelState);
            }

            string imageFileName = brand.ImageFileName;
            if (brandDto.ImageFile != null)
            {
                imageFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                imageFileName += Path.GetExtension(brandDto.ImageFile.FileName);

                string imagesFolder = env.WebRootPath + "/images/brands/";

                using (var stream = System.IO.File.Create(imagesFolder + imageFileName))
                {
                    brandDto.ImageFile.CopyTo(stream);
                }

                System.IO.File.Delete(imagesFolder + brand.ImageFileName);
            }


            brand.Name = brandDto.Name;
            brand.Slug = brandDto.Slug;
            brand.ImageFileName = imageFileName;
            brand.IsActive = brandDto.IsActive;

            _context.Brands.Update(brand);
            _context.SaveChanges();
            return Ok(brand);
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteBrand(int id)
        {
            var brand = _context.Brands.Find(id);
            if (brand == null)
            {
                return NotFound();
            }

            string imagesFolder = env.WebRootPath + "/images/brands/";
            System.IO.File.Delete(imagesFolder + brand.ImageFileName);

            _context.Brands.Remove(brand);
            _context.SaveChanges();

            return Ok();
        }
    }
}
