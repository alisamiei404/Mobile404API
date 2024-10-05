using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mobile404API.Data;
using Mobile404API.Models;
using System.Data;
using System.Runtime.Intrinsics.Arm;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Mobile404API.Controllers.Admin
{
    [Authorize(Roles = "admin")]
    [Route("api/admin/product")]
    [ApiController]
    public class ManageProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ManageProductController(ApplicationDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public IActionResult GetAllProduct(int? page, string? sort, string? order)
        {
            IQueryable<Product> query = _context.Products.Include(b => b.Brand);

            if (page == null || page < 1) page = 1;
            int pageSize = 5;
            int totalPages = 0;

            decimal count = _context.Products.Count();
            totalPages = (int)Math.Ceiling(count / pageSize);

            if (sort == null) sort = "id";
            if (order == null || order != "asc") order = "desc";

            if(sort.ToLower() == "ram")
            {
                if(order == "asc")
                {
                    query = query.OrderBy(p => p.Ram);
                }
                else
                {
                    query = query.OrderByDescending(p => p.Ram);
                }
            }
            else if (sort.ToLower() == "hard")
            {
                if (order == "asc")
                {
                    query = query.OrderBy(p => p.Hard);
                }
                else
                {
                    query = query.OrderByDescending(p => p.Hard);
                }
            }
            else if (sort.ToLower() == "camera")
            {
                if (order == "asc")
                {
                    query = query.OrderBy(p => p.Camera);
                }
                else
                {
                    query = query.OrderByDescending(p => p.Camera);
                }
            }
            else
            {
                if (order == "asc")
                {
                    query = query.OrderBy(p => p.Id);
                }
                else
                {
                    query = query.OrderByDescending(p => p.Id);
                }
            }

            var products = query
                .Skip((int)(page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var response = new
            {
                products = products,
                totalPages = totalPages,
                pageSize = pageSize,
                page = page
            };

            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public IActionResult CreateProduct([FromForm] ProductDto productDto)
        {
            Product product = new Product()
            {
                BrandId = productDto.BrandId,
                Title = productDto.Title,
                Ram = productDto.Ram,
                Hard = productDto.Hard,
                Camera = productDto.Camera,
                Description = productDto.Description,
                CreatedAt = DateTime.Now
            };

            _context.Products.Add(product);
            _context.SaveChanges();
            return Ok(product);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromForm] ProductDto productDto)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            product.BrandId = productDto.BrandId;
            product.Title = productDto.Title;
            product.Ram = productDto.Ram;
            product.Hard = productDto.Hard;
            product.Camera = productDto.Camera;
            product.Description = productDto.Description;

            _context.Products.Update(product);
            _context.SaveChanges();
            return Ok(product);
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            _context.SaveChanges();

            return Ok();
        }
    }
}
