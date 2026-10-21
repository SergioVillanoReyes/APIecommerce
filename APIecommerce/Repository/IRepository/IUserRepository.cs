using System;
using APIecommerce.Models;
using APIecommerce.Models.Dtos;

namespace APIecommerce.Repository.IRepository;

public interface IUserRepository
{
    ICollection<ApplicationUser> Getusers();

    ApplicationUser? GetUser(string id);

    bool IsUniqueUser(string name);

    Task<UserLoginResponseDto> Login(UserLoginDto user);

    Task<UserDataDto> Register(CreateUserDto user);
}
