using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mobile404API.Data;
using System.Data;

namespace Mobile404API.Controllers.Admin
{
    [Authorize(Roles = "admin")]
    [Route("api/admin/shop")]
    [ApiController]
    public class ManageShopController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ManageShopController(ApplicationDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public IActionResult GetAllShop(int? page)
        {
            if (page == null || page < 1) page = 1;
            int pageSize = 20;
            int totalPages = 0;

            decimal count = _context.Shops.Count();
            totalPages = (int)Math.Ceiling(count / pageSize);

            var shops = _context.Shops.Include(x => x.User)
                .OrderByDescending(u => u.Id)
                .Skip((int)(page - 1) * pageSize)
                .Take(pageSize)
                .ToList();


            var response = new
            {
                shops,
                totalPages,
                pageSize,
                page
            };

            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetProductsShop(int id, int? page) 
        {
            if (page == null || page < 1) page = 1;
            int pageSize = 20;
            int totalPages = 0;

            var shop = _context.Shops.Find(id);
            if(shop == null)
            {
                return NotFound();
            }

            var productIds = _context.ProductToShops
                .Where(p => p.ShopId == shop.Id)
                .Select(x => x.ProductId)
                .ToList();
            

            decimal count = _context.Products.Where(x => productIds.Contains(x.Id)).Count();
            totalPages = (int)Math.Ceiling(count / pageSize);

            var products = _context.Products
                .Where(x => productIds.Contains(x.Id))
                .Include(h => h.Brand)
                .Include(h => h.ProductToShops)
                .ThenInclude(x => x.Shop)
                .Select(x => new
                {
                    id = x.Id,
                    title = x.Title,
                    price = x.ProductToShops.First(x => x.ShopId == shop.Id),
                    image = x.Galleries.Count() != 0 ? x.Galleries.First().ImageFileName : null,
                })
                .OrderByDescending(b => b.id)
                .Skip((int)(page - 1) * pageSize)
                .Take(pageSize)
                .ToList();


            var response = new
            {
                products,
                shop,
                totalPages,
                pageSize,
                page
            };

            return Ok(response);
        }
    }
}
