using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using institute.DTOs;
using institute.Entities;
using institute.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace institute.Services;

public class AuthService: IAuthService
{
        private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _config;

    public AuthService(IUnitOfWork unitOfWork, IConfiguration config)
    {
        _unitOfWork = unitOfWork;
        _config = config;
    }

    public async Task<LoginResponseDto> LoginAsync(LoginRequestDto dto)
    {
        var user = (await _unitOfWork.Users
            .FindAsync(u => u.Email == dto.Email))
            .FirstOrDefault();

        if (user == null)
            throw new Exception("Invalid email or password");

        var validPassword = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);
        if (!validPassword)
            throw new Exception("Invalid email or password");

        var roles = (await _unitOfWork.UserRoles
            .FindAsync(ur => ur.UserId == user.Id))
            .Select(ur => ur.Role.Name)
            .ToList();

        var token = GenerateJwtToken(user, roles);

        return new LoginResponseDto
        {
            AccessToken = token,
            ExpireAt = DateTime.UtcNow.AddHours(
                _config.GetValue<int>("Jwt:ExpireHours")),
            Roles = roles
        };
    }
    public async Task RegisterAsync(RegisterRequestDto dto)
    {
        
        var exists = (await _unitOfWork.Users.FindIgnoreQueryFiltersAsync(u => u.Email == dto.Email)).Any();

            if (exists)
                throw new Exception("Email already exists");

            var user = new User
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                IsDeleted = false
            };

            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.CompleteAsync();

            var role = (await _unitOfWork.Roles
                    .FindAsync(r => r.Name == dto.Role))
                .FirstOrDefault();

            if (role == null)
                throw new Exception("Role not found");

            var userRole = new UserRole
            {
                UserId = user.Id,
                RoleId = role.Id
            };

            await _unitOfWork.UserRoles.AddAsync(userRole);
            await _unitOfWork.CompleteAsync();
    }

    private string GenerateJwtToken(User user, List<string> roles)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.FullName)
        };

        claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(
                _config.GetValue<int>("Jwt:ExpireHours")),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}