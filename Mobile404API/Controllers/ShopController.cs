using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Mobile404API.Data;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Azure;
using System.ComponentModel.DataAnnotations;
using Mobile404API.Models;


namespace Mobile404API.Controllers
{
    [Authorize]
    [Route("api/shop")]
    [ApiController]
    public class ShopController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration configuration;

        public ShopController(IConfiguration configuration, ApplicationDbContext context)
        {
            this._context = context;
            this.configuration = configuration;
        }

        [HttpGet("products")]
        public IActionResult GetProductNotMyShop()
        {
            var id = int.Parse(User.FindFirstValue("id")!);
            var user = _context.Users.Find(id);

            var shop = _context.Shops.FirstOrDefault(u => u.UserId == user!.Id);
            if (shop == null)
            {
                return NotFound();
            }

            var productIds = _context.ProductToShops
                .Where(p => p.ShopId == shop.Id)
                .Select(x => x.ProductId)
                .ToList();


            var products = _context.Products
                .Include(h => h.Brand)
                .Include(x => x.ProductToShops).ThenInclude(x => x.Shop)
                .Where(x => !productIds.Contains(x.Id))
                .OrderByDescending(b => b.Id).ToList();

            return Ok(products);

            
        }

        [HttpGet("myProducts")]
        public IActionResult GetProductMyShop()
        {
            var user = _context.Users.Include(h => h.Shop)
                .FirstOrDefault(u => u.Id == int.Parse(User.FindFirstValue("id")!));
            var shop = _context.Shops.FirstOrDefault(u => u.UserId == user!.Id);
            if (shop == null)
            {
                return NotFound();
            }

            var productIds = _context.ProductToShops
                .Where(p => p.ShopId == shop.Id)
                .Select(x => x.ProductId).ToList();
            

            var products = _context.Products
                .Where(x => productIds.Contains(x.Id))
                .Include(h => h.Brand)
                .Include(h => h.ProductToShops).ThenInclude(x => x.Shop)
                .OrderByDescending(b => b.Id).ToList();

            var response = new
            {
                shop = shop,
                products = products
            };

            return Ok(response);
        }

        [HttpPost()]
        public IActionResult CreateShop(ShopDto shopDto)
        {
            var user = _context.Users.Find(int.Parse(User.FindFirstValue("id")!));

            var b1 = _context.Shops.FirstOrDefault(b => b.Name == shopDto.Name);
            if (b1 != null)
            {
                ModelState.AddModelError("Name", "این نام وجود دارد");
                return ValidationProblem(ModelState);
            }


            Shop shop = new Shop()
            {
                Name = shopDto.Name,
                UserId = user.Id,
                CreatedAt = DateTime.Now
            };

            user.Role = "seller";

            _context.Shops.Add(shop);
            _context.Users.Update(user);
            _context.SaveChanges();

            var jwt = CreateJWToken(user);

            UserProfileDto userProfileDto = new UserProfileDto()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
                CreatedAt = user.CreatedAt
            };

            var response = new
            {
                token = jwt,
                user = userProfileDto
            };

            return Ok(response);
        }

        [HttpDelete()]
        public IActionResult DeleteShop()
        {
            var shop = _context.Shops.FirstOrDefault(x => x.UserId == int.Parse(User.FindFirstValue("id")!));
            if (shop == null || shop.Id == 1)
            {
                return NotFound();
            }

            var user = _context.Users.Find(int.Parse(User.FindFirstValue("id")!));

            user.Role = "client";           

            _context.Shops.Remove(shop);
            _context.Users.Update(user);
            _context.SaveChanges();

            var jwt = CreateJWToken(user);

            UserProfileDto userProfileDto = new UserProfileDto()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
                CreatedAt = user.CreatedAt
            };

            var response = new
            {
                token = jwt,
                user = userProfileDto
            };

            return Ok(response);
        }

        [HttpPost("product/{id}")]
        public IActionResult AddPriceProductToShop(int id,[Required, Range(3000000,90000000)] int price)
        {

            var product = _context.Products.Find(id);
            if(product == null)
            {
                return NotFound();
            }

            var shop = _context.Shops.FirstOrDefault(u => u.UserId == int.Parse(User.FindFirstValue("id")!));
            if (shop == null)
            {
                return NotFound();
            }

            var productToShopCheck = _context.ProductToShops.FirstOrDefault(x => x.ProductId == product.Id && x.ShopId == shop.Id);
            if (productToShopCheck == null)
            {
                ProductToShop productToShop = new ProductToShop()
                {
                    ProductId = product.Id,
                    ShopId = shop.Id,
                    Price = price
                };

                _context.ProductToShops.Add(productToShop);
            }
            else
            {
                productToShopCheck.Price = price;
            }

            
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete("product/{id}")]
        public IActionResult DeleteProductToShop(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            var shop = _context.Shops.FirstOrDefault(u => u.UserId == int.Parse(User.FindFirstValue("id")!));
            if (shop == null)
            {
                return NotFound();
            }

            var ps = _context.ProductToShops.FirstOrDefault(u => u.ShopId == shop.Id && u.ProductId == product.Id);
            if (ps == null)
            {
                return NotFound();
            }

            _context.ProductToShops.Remove(ps!);
            _context.SaveChanges();

            return Ok();
        }

        private string CreateJWToken(User user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim("id", ""+user.Id),
                new Claim("role", user.Role),

            };

            string strKey = configuration["JwtSettings:Key"]!;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(strKey));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var token = new JwtSecurityToken(
                issuer: configuration["JwtSettings:Issuer"],
                audience: configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

    }
}
