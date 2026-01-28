using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using institute.DTOs;
using institute.Entities;
using Microsoft.IdentityModel.Tokens;

namespace institute.Interfaces;

public interface IAuthService
{
    Task<LoginResponseDto> LoginAsync(LoginRequestDto dto);
    Task RegisterAsync(RegisterRequestDto dto);

}