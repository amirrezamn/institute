// using System.IdentityModel.Tokens.Jwt;
// using System.Security.Claims;
// using System.Text;
// using institute.DTOs;
// using institute.Entities;
// using institute.Interfaces;
// using Microsoft.IdentityModel.Tokens;
//
// namespace institute.Services;
//
// public class AuthService: IAuthService
// {
//         private readonly IUnitOfWork _unitOfWork;
//         private readonly IConfiguration _config;
//         private readonly IEmailService _emailService;
//
//     
//
//     public AuthService(IUnitOfWork unitOfWork, IConfiguration config, IEmailService emailService)
//     {
//         _unitOfWork = unitOfWork;
//         _config = config;
//         _emailService = emailService;
//
//     }
//
//     public async Task<LoginResponseDto> LoginAsync(LoginRequestDto dto)
//     {
//         var user = (await _unitOfWork.Users
//             .FindAsync(u => u.Email == dto.Email))
//             .FirstOrDefault();
//
//         if (user == null)
//             throw new Exception("Invalid email or password");
//
//         var validPassword = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);
//         if (!validPassword)
//             throw new Exception("Invalid email or password");
//
//         // var roles = (await _unitOfWork.UserRoles
//         //     .FindAsync(ur => ur.UserId == user.Id))
//         //     .Select(ur => ur.Role.Name)
//         //     .ToList();
//         
//         var userRoles = await _unitOfWork.UserRoles
//             .FindAsync(ur => ur.UserId == user.Id, ur => ur.Role);
//
//         var roles = userRoles
//             .Select(ur => ur.Role.Name)
//             .ToList();
//
//
//         var token = GenerateJwtToken(user, roles);
//
//         return new LoginResponseDto
//         {
//             AccessToken = token,
//             ExpireAt = DateTime.UtcNow.AddHours(
//                 _config.GetValue<int>("Jwt:ExpireHours")),
//             Roles = roles
//         };
//     }
//     public async Task RegisterAsync(RegisterRequestDto dto)
//     {
//         
//         var exists = (await _unitOfWork.Users.FindIgnoreQueryFiltersAsync(u => u.Email == dto.Email)).Any();
//
//             if (exists)
//                 throw new Exception("Email already exists");
//
//             var user = new User
//             {
//                 FullName = dto.FullName,
//                 Email = dto.Email,
//                 PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
//                 IsDeleted = false
//             };
//
//             await _unitOfWork.Users.AddAsync(user);
//             await _unitOfWork.CompleteAsync();
//
//             var role = (await _unitOfWork.Roles
//                     .FindAsync(r => r.Name == dto.Role))
//                 .FirstOrDefault();
//
//             if (role == null)
//                 throw new Exception("Role not found");
//
//             var userRole = new UserRole
//             {
//                 UserId = user.Id,
//                 RoleId = role.Id
//             };
//
//             await _unitOfWork.UserRoles.AddAsync(userRole);
//             await _unitOfWork.CompleteAsync();
//     }
//
//     private string GenerateJwtToken(User user, List<string> roles)
//     {
//         var claims = new List<Claim>
//         {
//             new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
//             new Claim(ClaimTypes.Email, user.Email),
//             new Claim(ClaimTypes.Name, user.FullName)
//         };
//
//         claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));
//
//         var key = new SymmetricSecurityKey(
//             Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
//
//         var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
//
//         var token = new JwtSecurityToken(
//             issuer: _config["Jwt:Issuer"],
//             audience: _config["Jwt:Audience"],
//             claims: claims,
//             expires: DateTime.UtcNow.AddHours(
//                 _config.GetValue<int>("Jwt:ExpireHours")),
//             signingCredentials: creds
//         );
//
//         return new JwtSecurityTokenHandler().WriteToken(token);
//     }
//
//     public async Task ForgotPasswordAsync(string email)
//     {
//         var user = (await _unitOfWork.Users
//                 .FindAsync(u => u.Email == email))
//             .FirstOrDefault();
//         if (user == null)
//         {
//            return;
//         }
//
//         var token = Guid.NewGuid().ToString();
//         var resetToken = new PasswordResetToken
//         {
//             ExpireAt = DateTime.UtcNow.AddMinutes(30),
//             Token = token,
//             UserId = user.Id
//         };
//         await _unitOfWork.PasswordResetTokens.AddAsync(resetToken);
//         await _unitOfWork.CompleteAsync();
//         var resetLink = $"http://localhost:5229/reset-password?token={token}";
//
//         await _emailService.SendEmailAsync(
//             user.Email,
//             "Password Reset",
//             $@"
//         <h2>Password Reset</h2>
//         <p>Click the link below to reset your password:</p>
//         <a href='{resetLink}'>Reset Password</a>
//         <p>This link will expire in 1 hour.</p>
//     ");
//     }
//     // dndz fvnm piyq onoc
//
//
//     public async Task ResetPasswordAsync(PasswordResetDto dto)
//     {
//         // var resetToken = (await _unitOfWork.PasswordResetTokens
//         //         .FindIgnoreQueryFiltersAsync(t => t.Token == dto.Token))
//         //     .FirstOrDefault();
//         
//         var resetToken = (await _unitOfWork.PasswordResetTokens
//                 .FindAsync(t => t.Token == dto.Token))
//             .FirstOrDefault();
//
//         if (resetToken == null || resetToken.ExpireAt < DateTime.UtcNow)
//             throw new Exception("Invalid or expired token");
//
//         var user = await _unitOfWork.Users.GetByIdAsync(resetToken.UserId);
//         if (user == null)
//             throw new Exception("User not found");
//
//         user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
//         await _unitOfWork.Users.UpdateAsync(user);
//
//         await _unitOfWork.PasswordResetTokens.HardDeleteAsync(resetToken);
//
//         await _unitOfWork.CompleteAsync();
//     }
//
// }
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using institute.DTOs;
using institute.Entities;
using institute.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace institute.Services;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _config;
    private readonly IEmailService _emailService;

    public AuthService(IUnitOfWork unitOfWork, IConfiguration config, IEmailService emailService)
    {
        _unitOfWork = unitOfWork;
        _config = config;
        _emailService = emailService;
    }

    // ================== Login ==================
    public async Task<LoginResponseDto> LoginAsync(LoginRequestDto dto)
    {
        var user = await _unitOfWork.Users.FindOneWithIncludesAsync(u => u.Email == dto.Email);
        if (user == null)
            throw new Exception("Invalid email or password");

        if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            throw new Exception("Invalid email or password");

        // Load roles
        var userRoles = await _unitOfWork.UserRoles
            .FindWithIncludesAsync(ur => ur.UserId == user.Id, ur => ur.Role);

        var roles = userRoles.Select(ur => ur.Role.Name).ToList();

        var token = GenerateJwtToken(user, roles);

        return new LoginResponseDto
        {
            AccessToken = token,
            ExpireAt = DateTime.UtcNow.AddHours(_config.GetValue<int>("Jwt:ExpireHours")),
            Roles = roles
        };
    }

    // ================== Register ==================
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

        var role = (await _unitOfWork.Roles.FindOneWithIncludesAsync(r => r.Name == dto.Role))
            ?? throw new Exception("Role not found");

        var userRole = new UserRole
        {
            UserId = user.Id,
            RoleId = role.Id
        };

        await _unitOfWork.UserRoles.AddAsync(userRole);
        await _unitOfWork.CompleteAsync();
    }

    // ================== Forgot Password ==================
    public async Task ForgotPasswordAsync(string email)
    {
        var user = await _unitOfWork.Users.FindOneWithIncludesAsync(u => u.Email == email);
        if (user == null) return;

        // Remove old tokens
        var oldTokens = await _unitOfWork.PasswordResetTokens.FindAsync(t => t.UserId == user.Id);
        foreach (var t in oldTokens)
            await _unitOfWork.PasswordResetTokens.HardDeleteAsync(t);

        var token = Guid.NewGuid().ToString();
        var resetToken = new PasswordResetToken
        {
            Token = token,
            ExpireAt = DateTime.UtcNow.AddMinutes(30),
            UserId = user.Id
        };

        await _unitOfWork.PasswordResetTokens.AddAsync(resetToken);
        await _unitOfWork.CompleteAsync();

        var resetLink = $"http://localhost:4200/reset-password?token={token}";

        await _emailService.SendEmailAsync(
            user.Email,
            "Password Reset",
            $@"
            <h2>Password Reset</h2>
            <p>Click the link below to reset your password:</p>
            <a href='{resetLink}'>Reset Password</a>
            <p>This link will expire in 30 minutes.</p>"
        );
    }

    // ================== Reset Password ==================
    public async Task ResetPasswordAsync(PasswordResetDto dto)
    {
        var resetToken = (await _unitOfWork.PasswordResetTokens
            .FindIgnoreQueryFiltersAsync(t => t.Token == dto.Token))
            .FirstOrDefault();

        if (resetToken == null || resetToken.ExpireAt < DateTime.UtcNow)
            throw new Exception("Invalid or expired token");

        var user = await _unitOfWork.Users.GetByIdAsync(resetToken.UserId)
            ?? throw new Exception("User not found");

        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
        await _unitOfWork.Users.UpdateAsync(user);

        await _unitOfWork.PasswordResetTokens.HardDeleteAsync(resetToken);
        await _unitOfWork.CompleteAsync();
    }

    // ================== JWT Token Generator ==================
    private string GenerateJwtToken(User user, List<string> roles)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.FullName)
        };

        claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(_config.GetValue<int>("Jwt:ExpireHours")),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}