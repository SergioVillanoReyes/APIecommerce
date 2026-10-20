using System;
using APIecommerce.Models;
using APIecommerce.Models.Dtos;

namespace APIecommerce.Repository.IRepository;

public interface IUserRepository
{
    ICollection<User> Getusers();

    User? GetUser(int id);

    bool IsUniqueUser(string name);

    Task<UserLoginResponseDto> Login(UserLoginDto user);

    Task<User> Register(CreateUserDto user);
}
