using APIecommerce.Models;
using APIecommerce.Models.Dtos;
using Mapster;

namespace APIecommerce.Mapping;

public static class MapsterConfig
{
    public static void RegisterMappings()
    {
        var config = TypeAdapterConfig.GlobalSettings;

        config.NewConfig<Category, CategoryDto>();
        config.NewConfig<CategoryDto, Category>();
        config.NewConfig<Category, CreateCategoryDto>();
        config.NewConfig<CreateCategoryDto, Category>();

        config.NewConfig<Product, ProductDto>()
            .Map(destination => destination.CategoryName, source => source.Category.Name);
        config.NewConfig<CreateProductDto, Product>()
            .Ignore(destination => destination.Category);
        config.NewConfig<Product, CreateProductDto>();
        config.NewConfig<UpdateProductDto, Product>()
            .Ignore(destination => destination.Category);
        config.NewConfig<Product, UpdateProductDto>();

        config.NewConfig<User, UserDto>();
        config.NewConfig<UserDto, User>();
        config.NewConfig<User, CreateUserDto>();
        config.NewConfig<CreateUserDto, User>();
        config.NewConfig<User, UserLoginDto>();
        config.NewConfig<UserLoginDto, User>();
        config.NewConfig<User, UserLoginResponseDto>();
        config.NewConfig<UserLoginResponseDto, User>();
        config.NewConfig<ApplicationUser, UserDataDto>();
        config.NewConfig<UserDataDto, ApplicationUser>();
        config.NewConfig<ApplicationUser, UserDto>()
            .Map(destination => destination.id, source => source.Id)
            .Map(destination => destination.Username, source => source.UserName);
        config.NewConfig<UserDto, ApplicationUser>()
            .Map(destination => destination.Id, source => source.id)
            .Map(destination => destination.UserName, source => source.Username);
    }
}
