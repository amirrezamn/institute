using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using institute.Data;
using institute.DTOs;
using institute.Entities;
using institute.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace institute.Controllers;

    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
        {
            var result = await _authService.LoginAsync(dto);
            return Ok(result);
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestDto dto)
        {
            await _authService.RegisterAsync(dto);
            return Ok("User registered successfully");
        }
    }


    // [HttpPost("register")]
    // public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    // {
    //     if (_context.Users.Any(u => u.Email == dto.Email))
    //         return BadRequest("Email already exists");
    //
    //     var user = new User
    //     {
    //         FullName = dto.FullName,
    //         Email = dto.Email,
    //         PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
    //     };
    //
    //     _context.Users.Add(user);
    //     await _context.SaveChangesAsync();
    //
    //     
    //     var role = _context.Roles.FirstOrDefault(r => r.Name == dto.Role);
    //     if (role != null)
    //     {
    //         _context.UserRoles.Add(new UserRole { UserId = user.Id, RoleId = role.Id });
    //         await _context.SaveChangesAsync();
    //     }
    //
    //     return Ok("User registered successfully");
    // }
    //
    // [HttpPost("login")]
    // public IActionResult Login([FromBody] LoginDto dto)
    // {
    //     var user = _context.Users
    //         .Include(u => u.UserRoles)
    //         .ThenInclude(ur => ur.Role)
    //         .FirstOrDefault(u => u.Email == dto.Email);
    //
    //     if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
    //         return Unauthorized("Invalid credentials");
    //
    //     var tokenHandler = new JwtSecurityTokenHandler();
    //     var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);
    //
    //     var claims = new List<Claim>
    //     {
    //         new Claim(ClaimTypes.Name, user.Email),
    //         new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
    //     };
    //
    //     foreach (var role in user.UserRoles)
    //     {
    //         claims.Add(new Claim(ClaimTypes.Role, role.Role.Name));
    //     }
    //
    //     var tokenDescriptor = new SecurityTokenDescriptor
    //     {
    //         Subject = new ClaimsIdentity(claims),
    //         Expires = DateTime.UtcNow.AddMinutes(int.Parse(_config["Jwt:ExpireMinutes"])),
    //         Issuer = _config["Jwt:Issuer"],
    //         Audience = _config["Jwt:Audience"],
    //         SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
    //     };
    //
    //     var token = tokenHandler.CreateToken(tokenDescriptor);
    //     var tokenString = tokenHandler.WriteToken(token);
    //
    //     return Ok(new { Token = tokenString });
    // }


