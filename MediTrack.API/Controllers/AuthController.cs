using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MediTrack.API.Data;
using MediTrack.API.DTOs.Auth;
using MediTrack.API.Models;
using System.ComponentModel.DataAnnotations;

namespace MediTrack.API.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

    public AuthController(AppDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody]RegisterDto dto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
                return BadRequest(new { message = "Email already in use" });

            var user = new User
            {
                Email= dto.Email,
                HashPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = dto.Role
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok(new {message= "Registered successfully" });
        }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody]LoginDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var user  = await _context.Users.FirstOrDefaultAsync(u=>u.Email == dto.Email);
        if(user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.HashPassword))
            return Unauthorized(new { message = "Invalid email or password" });

        var token = GenerateJwt(user);
        return Ok(new { token });   
    }

    private string GenerateJwt(User user)
    {
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_config["Jwt:Key"]!)); 

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

       
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role)  
        };

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(8),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
