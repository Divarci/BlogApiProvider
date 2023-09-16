using AutoMapper;
using EntityLayer.Blog.DTOs.CategoryDTOs;
using EntityLayer.Blog.Entities;

namespace ServiceLayer.Automapper.Blog
{
    public class CategoryMapper : Profile
    {
        public CategoryMapper()
        {
            CreateMap<Category,CategoryAddDTO>().ReverseMap();
            CreateMap<Category,CategoryUpdateDTO>().ReverseMap();
            CreateMap<Category,CategoryListDTO>().ReverseMap();
            CreateMap<Category,CategoryDTO>().ReverseMap();
        }
    }
}
