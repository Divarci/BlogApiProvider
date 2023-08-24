using EntityLayer.Blog.DTOs.ArticleDTOs;
using EntityLayer.GenericDTOs;

namespace ServiceLayer.Services.Blog.Abstract
{
    public interface IArticleService
    {
        Task<CustomResponseDto<List<ArticleListDTO>>> GetArticleListAsync();
        Task<CustomResponseDto<ArticleUpdateDTO>> GetArticleByIdAsync(int id);
        Task<CustomResponseDto<ArticleAddDTO>> ArticleAddAsync(ArticleAddDTO request);
        Task<CustomResponseDto<NoContentDto>> ArticleUpdateAsync(ArticleUpdateDTO request);
        Task<CustomResponseDto<NoContentDto>> ArticleDeleteAsync(int id);

    }
}
