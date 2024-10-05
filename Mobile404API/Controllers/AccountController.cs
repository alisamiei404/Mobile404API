using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Mobile404API.Data;
using Mobile404API.Extention;
using Mobile404API.Models;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Mobile404API.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly ApplicationDbContext _context;

        public AccountController(IConfiguration configuration, ApplicationDbContext context)
        {
            this.configuration = configuration;
            this._context = context;
        }

        [HttpPost("register")]
        public IActionResult Register(UserDto userDto)
        {
            // check if the email address is already user or not
            var emailCount = _context.Users.Count(u => u.Email == userDto.Email);
            if(emailCount > 0)
            {
                ModelState.AddModelError("Email", "ایمیل تکراری است ایمیل دیگری وارد کنید");
                return ValidationProblem(ModelState);
            }

            // create new account
            User user = new User()
            {
                Name = userDto.Name,
                Email = userDto.Email,
                Password = PasswordHelper.EncodePasswordMd5(userDto.Password),
                Role = "client",
                CreatedAt = DateTime.Now
            };

            _context.Users.Add(user);
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

        [HttpPost("login")]
        public IActionResult Login(UserLoginDto userLoginDto)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == userLoginDto.Email);
            if(user == null)
            {
                ModelState.AddModelError("Email", "با این ایمیل حساب کاربری ثبت نشده است");
                return ValidationProblem(ModelState);
            }

            if (user.Password != PasswordHelper.EncodePasswordMd5(userLoginDto.Password))
            {
                ModelState.AddModelError("Password", "رمز عبور اشتباه است");
                return ValidationProblem(ModelState);
            }

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

        [Authorize]
        [HttpGet("profile")]
        public IActionResult Profile()
        {
            var id = int.Parse(User.FindFirstValue("id")!);
            var user = _context.Users.Find(id);

            UserProfileDto userProfileDto = new UserProfileDto()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
                CreatedAt = user.CreatedAt
            };

            return Ok(userProfileDto);
        }

        [Authorize]
        [HttpPut("updateProfile")]
        public IActionResult UpdateProfile(UserProfileUpdateDto userProfileUpdateDto)
        {
            var id = int.Parse(User.FindFirstValue("id")!);
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            var emailFind = _context.Users.FirstOrDefault(u => u.Email == userProfileUpdateDto.Email && u.Id != id);
            if (emailFind != null)
            {
                ModelState.AddModelError("Email", "ایمیل دیگری وارد کنید");
                return ValidationProblem(ModelState);
            }

            user.Name = userProfileUpdateDto.Name;
            user.Email = userProfileUpdateDto.Email;

            _context.SaveChanges();

            UserProfileDto userProfileDto = new UserProfileDto()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
                CreatedAt = user.CreatedAt
            };

            return Ok(userProfileDto);
        }

        [Authorize]
        [HttpPut("updatePassword")]
        public IActionResult UpdatePassword([Required, MinLength(4), MaxLength(100)]string password)
        {
            var id = int.Parse(User.FindFirstValue("id")!);
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            user.Password = PasswordHelper.EncodePasswordMd5(password);
            _context.SaveChanges();

            return Ok();
        }


        [HttpPost("forgotPassword")]
        public IActionResult ForgotPassword(string email)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if(user == null)
            {
                return NotFound();
            }

            var oldPwdReset = _context.PasswordResets.FirstOrDefault(r => r.Email == email);
            if(oldPwdReset != null)
            {
                _context.PasswordResets.Remove(oldPwdReset);
            }

            int code;
            code = 1234;

            PasswordReset passwordReset = new PasswordReset()
            {
                Email = email,
                Code = code,
                CreatedAt = DateTime.Now
            };

            _context.PasswordResets.Add(passwordReset);
            _context.SaveChanges();

            // send code by email

            return Ok();
        }

        [HttpPost("resetPassword")]
        public IActionResult ResetPassword(int code, string password)
        {
            var pwdReset = _context.PasswordResets.FirstOrDefault(r => r.Code == code);
            if(pwdReset == null)
            {
                ModelState.AddModelError("Code", "کد اشتباه است");
                return ValidationProblem(ModelState);
            }

            var user = _context.Users.FirstOrDefault(u => u.Email == pwdReset.Email);
            if (user == null)
            {
                return NotFound();
            }

            user.Password = PasswordHelper.EncodePasswordMd5(password);

            _context.PasswordResets.Remove(pwdReset);
            _context.SaveChanges();

            return Ok();
        }

        [HttpGet("testToken")]
        public IActionResult TestToken()
        {
            User user = new User() { Id = 2, Role = "admin" };
            string jwt = CreateJWToken(user);
            var response = new {JWToken = jwt};
            return Ok(response);
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
