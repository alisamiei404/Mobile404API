using Azure;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Mobile404API.Data;
using Mobile404API.Models;
using System.Security.Claims;

namespace Mobile404API.Controllers
{
    [Route("api")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            this._context = context;
        }

        [HttpGet("home")]
        public IActionResult Index() 
        {
            var brands = _context.Brands.ToList();
            var brandsProducts = _context.Brands
                .Include(x => x.Products)
                .ThenInclude(g => g.Galleries.Take(1))
                .Include(p => p.Products)
                .ThenInclude(g => g.ProductToShops.Take(1))
                .Where(x => x.Products.Count() > 0)
                .Select(x => new
                {
                    id = x.Id,  
                    name = x.Name,
                    slug = x.Slug,
                    image = x.ImageFileName,
                    products = x.Products.Where(x => x.ProductToShops.Count() != 0).OrderByDescending(x => x.Id).Take(3).Select(x => new
                    {
                        id = x.Id,
                        title = x.Title,
                        price = x.ProductToShops.OrderBy(x => x.Price).Count() != 0 ? x.ProductToShops.OrderBy(x => x.Price).First().Price : 0,
                        image = x.Galleries.Count() != 0 ? x.Galleries.First().ImageFileName : null,
                    })
                })
                .ToList();

            var response = new
            {
                brandsProducts,
                brands
            };

            return Ok(response);
        }

        [HttpGet("brand/{slug}")]
        public IActionResult GetAllProductBrand(string slug)
        {
            var brand = _context.Brands.FirstOrDefault(x => x.Slug == slug);
            if (brand == null)
            {
                return NotFound();
            }

            var products = _context.Products
                .Include(g => g.Galleries)
                .Include(g => g.ProductToShops)
                .Where(x => x.BrandId == brand.Id)
                .Select(x => new
                {
                    id = x.Id,
                    title = x.Title,
                    price = x.ProductToShops.OrderBy(x => x.Price).Count() != 0 ? x.ProductToShops.OrderBy(x => x.Price).First().Price : 0,
                    image = x.Galleries.Count() != 0 ? x.Galleries.First().ImageFileName : null,
                })
                .OrderByDescending(x => x.id)
                .ToList();
            var response = new
            {
                products,
                brand
            };

            return Ok(response);
        }

        [HttpGet("products")]
        public IActionResult GetAllProduct(int? page, int? sort)
        {

            //System.Threading.Thread.Sleep(5000);

            if (page == null || page < 1) page = 1;
            int pageSize = 4;
            int totalPages = 0;

            decimal count = _context.Products.Count();
            totalPages = (int)Math.Ceiling(count / pageSize);

            var query = _context.Products.Include(g => g.Galleries).Include(x => x.ProductToShops)
                .Select(x => new
                {
                    id = x.Id,
                    title = x.Title,
                    price = x.ProductToShops.OrderBy(x => x.Price).Count() != 0 ? x.ProductToShops.OrderBy(x => x.Price).First().Price : 0,
                    image = x.Galleries.Count() != 0 ? x.Galleries.First().ImageFileName : null,
                });

            if (sort == null) sort = 1;

            if (sort == 3)
            {
                query = query.OrderByDescending(p => p.price);
            }
            else if (sort == 4)
            {
                query = query.OrderBy(p => p.price);
            }
            else if (sort == 2)
            {
                query = query.OrderBy(p => p.id);
            }
            else
            {
                query = query.OrderByDescending(p => p.id);
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

        [HttpGet("product/{id}")]
        public IActionResult GetProduct(int id)
        {
            var product = _context.Products.Include(b => b.Brand).Include(g => g.Galleries).Include(p => p.ProductToShops).ThenInclude(g => g.Shop).FirstOrDefault(x => x.Id == id);
            if (product == null) 
            {
                return NotFound();
            }

            var comments = _context.Comments.Where(x => x.ProductId == product.Id).Include(u => u.User).OrderByDescending(x => x.Id).ToList();

            var response = new
            {
                product,
                comments
            };

            return Ok(response);
        }

    }
}
