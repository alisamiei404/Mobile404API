using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mobile404API.Data;
using Mobile404API.Models;
using System.Data;

namespace Mobile404API.Controllers.Admin
{
    [Authorize(Roles = "admin")]
    [Route("api/admin/user")]
    [ApiController]
    public class ManageUserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ManageUserController(ApplicationDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public IActionResult GetAllUser(int? page)
        {
            if (page == null || page < 1) page = 1;
            int pageSize = 20;
            int totalPages = 0;

            decimal count = _context.Users.Count();
            totalPages = (int)Math.Ceiling(count / pageSize);

            var users = _context.Users
                .OrderByDescending(u => u.Id)
                .Skip((int)(page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            List<UserProfileDto> userProfiles = new List<UserProfileDto>();
            foreach (var user in users)
            {
                var userProfileDto = new UserProfileDto()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Role = user.Role,
                    CreatedAt = user.CreatedAt,
                };

                userProfiles.Add(userProfileDto);
            }

            var response = new
            {
                users = userProfiles,
                totalPages = totalPages,
                pageSize = pageSize,
                page = page
            };

            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id) 
        {
            var user = _context.Users.Find(id);

            if(user == null)
            {
                return NotFound();
            }

            var userProfileDto = new UserProfileDto()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
                CreatedAt = user.CreatedAt,
            };

            return Ok(userProfileDto);
        }
    }
}
