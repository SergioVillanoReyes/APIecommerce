using System;
using APIecommerce.Models;
using APIecommerce.Models.Dtos;
using AutoMapper;

namespace APIecommerce.Mapping;

public class CategoryProfile: Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<Category, CreateCategoryDto>().ReverseMap();
    }
}
