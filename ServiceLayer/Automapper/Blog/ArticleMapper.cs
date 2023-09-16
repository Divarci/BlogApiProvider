using AutoMapper;
using EntityLayer.Blog.DTOs.ArticleDTOs;
using EntityLayer.Blog.Entities;

namespace ServiceLayer.Automapper.Blog
{
    public class ArticleMapper : Profile
    {
        public ArticleMapper()
        {
            CreateMap<Article,ArticleListDTO>().ReverseMap();
            CreateMap<Article,ArticleAddDTO>().ReverseMap();
            CreateMap<Article,ArticleUpdateDTO>().ReverseMap();
            CreateMap<Article,ArticleImageDTO>().ReverseMap();
        }
    }
}
