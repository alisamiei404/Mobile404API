using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mobile404API.Data;
using Mobile404API.Models;
using System.Data;
using System.Security.Claims;

namespace Mobile404API.Controllers
{
    [Authorize]
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CommentController(ApplicationDbContext context)
        {
            this._context = context;
        }

        [HttpPost]
        public IActionResult CreateComment(CommentDto commentDto)
        {
            var product = _context.Products.Find(commentDto.ProductId);
            if (product == null)
            {
                return NotFound();
            }

            var userId = int.Parse(User.FindFirstValue("id")!);

            Comment comment = new Comment()
            {
                UserId = userId,
                ProductId = product.Id,
                Content = commentDto.Content,
                IsActive = false,
                CreatedAt = DateTime.Now
            };

            _context.Comments.Add(comment);
            _context.SaveChanges();

            var comments = _context.Comments.Where(x => x.ProductId == product.Id).Include(u => u.User).OrderByDescending(x => x.Id).ToList();
            return Ok(comments);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateComment(int id, [FromForm] CommentDto commentDto)
        {
            var comment = _context.Comments.Find(id);
            if (comment == null)
            {
                return NotFound();
            }

            var userId = int.Parse(User.FindFirstValue("id")!);

            if (userId == comment.UserId)
            {
                comment.Content = commentDto.Content;
                comment.IsActive = false;
                _context.Comments.Update(comment);
                _context.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteComment(int id)
        {
            var comment = _context.Comments.Find(id);
            if(comment == null)
            {
                return NotFound();
            }

            var userId = int.Parse(User.FindFirstValue("id")!);

            if(userId == comment.UserId)
            {
                _context.Comments.Remove(comment);
                _context.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound();
            }
            
        }
    }
}
