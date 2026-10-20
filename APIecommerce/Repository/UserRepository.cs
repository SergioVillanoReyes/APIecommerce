using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using APIecommerce.Data;
using APIecommerce.Models;
using APIecommerce.Models.Dtos;
using APIecommerce.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace APIecommerce.Repository;

public class UserRepository : IUserRepository
{

    private readonly ApplicationDBContext _db;

    private string? secretKey;

    public UserRepository(ApplicationDBContext db, IConfiguration configuration)
    {
        _db = db;
        secretKey = configuration.GetValue<string>("ApiSettings:SecretKey");
    }
    public User? GetUser(int id)
    {
        return _db.Users.FirstOrDefault(u => u.id == id);
    }

    public ICollection<User> Getusers()
    {
        return _db.Users.OrderBy(u => u.Username).ToList();
    }

    public bool IsUniqueUser(string name)
    {
        return !_db.Users.Any(u => u.Username.ToLower().Trim() == name.ToLower().Trim());
    }

    public async Task<UserLoginResponseDto> Login(UserLoginDto userLoginDto)
    {
        if (string.IsNullOrEmpty(userLoginDto.Username))
        {
            return new UserLoginResponseDto()
            {
                Token = "",
                User = null,
                Message = "El username es requerido.",
            };
        }

        var user = await _db.Users.FirstOrDefaultAsync<User>(
            u => u.Username.ToLower().Trim() == userLoginDto.Username.ToLower().Trim());

        if(user == null)
        {
            return new UserLoginResponseDto()
            {
                Token = "",
                User = null,
                Message = "El username no se encontro.",
            };  
        }

        if(!BCrypt.Net.BCrypt.Verify(userLoginDto.Password, user.Password))
        {
            return new UserLoginResponseDto()
            {
                Token = "",
                User = null,
                Message = "Las credenciales son incorrectas.",
            };   
        }

        // generando jwt
        var handlerToken = new JwtSecurityTokenHandler();
        if (string.IsNullOrWhiteSpace(secretKey))
        {
            throw new InvalidOperationException("secretkey no esta configurada.");
        }
        var key = Encoding.UTF8.GetBytes(secretKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity( new[]
            {
                new Claim("id", user.id.ToString()),
                new Claim("username", user.Username),
                new Claim(ClaimTypes.Role, user.Role ?? string.Empty),
            }
            ),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = handlerToken.CreateToken(tokenDescriptor);
        return new UserLoginResponseDto()
        {
            Token = handlerToken.WriteToken(token),
            User = new UserRegisterDto()
            {
                Username = user.Username,
                Name = user.Name,
                Role = user.Role,
                Password = user.Password ?? ""
            },
            Message = "Login successfully",
        };

    }

    public async Task<User> Register(CreateUserDto createUserDto)
    {
        var encriptedPassword = BCrypt.Net.BCrypt.HashPassword(createUserDto.Password);
        var user = new User()
        {
            Username = createUserDto.Username ?? "Not username provided",
            Name = createUserDto.Name,
            Role = createUserDto.Role,
            Password = encriptedPassword,
        };
        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        return user;
    }
}
