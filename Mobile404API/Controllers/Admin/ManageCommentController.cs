using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mobile404API.Data;
using Mobile404API.Models;
using System.Data;
using System.Security.Claims;

namespace Mobile404API.Controllers.Admin
{
    [Authorize(Roles = "admin")]
    [Route("api/admin/comment")]
    [ApiController]
    public class ManageCommentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ManageCommentController(ApplicationDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public IActionResult GetAllComment(int? page)
        {
            if (page == null || page < 1) page = 1;
            int pageSize = 8;
            int totalPages = 0;

            decimal count = _context.Comments.Count();
            totalPages = (int)Math.Ceiling(count / pageSize);

            var comments = _context.Comments.Include(x => x.User)
                .OrderByDescending(u => u.Id)
                .Skip((int)(page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var response = new
            {
                comments = comments,
                totalPages = totalPages,
                pageSize = pageSize,
                page = page
            };

            return Ok(response);
        }

        [HttpGet("{status}")]
        public IActionResult GetAllCommentByStatus(string status, int? page)
        {
            if (page == null || page < 1) page = 1;
            int pageSize = 8;
            int totalPages = 0;

            bool active = true;

            if (status == "active")
            {
                active = true;
            }
            else if (status == "notActive")
            {
                active = false;
            }
            else
            {
                return NotFound();
            }

            decimal count = _context.Comments.Where(x => x.IsActive == active).Count();
            totalPages = (int)Math.Ceiling(count / pageSize);

            var comments = _context.Comments.Include(x => x.User).Where(x => x.IsActive == active)
                .OrderByDescending(u => u.Id)
                .Skip((int)(page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var response = new
            {
                comments = comments,
                totalPages = totalPages,
                pageSize = pageSize,
                page = page
            };

            return Ok(response);
        }

        [HttpGet("user/{id}")]
        public IActionResult GetAllCommentUser(int id) 
        {
            var user = _context.Users.Find(id);
            if(user == null) return NotFound();

            var comments = _context.Comments.Include(x => x.User).Where(x => x.UserId == user.Id).ToList();

            var response = new
            {
                comments = comments,
                totalPages = 0,
                pageSize = 0,
                page = 0
            };

            return Ok(response);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateIsActive(int id)
        {
            var comment = _context.Comments.Find(id);
            if (comment == null)
            {
                return NotFound();
            }

            comment.IsActive = !comment.IsActive;
            _context.Comments.Update(comment);
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteComment(int id)
        {
            var comment = _context.Comments.Find(id);
            if (comment == null)
            {
                return NotFound();
            }

            _context.Comments.Remove(comment);
            _context.SaveChanges();
            return Ok();

        }
    }
}
