using CoreLayer.Response;
using EntityLayer.Blog.DTOs.CategoryDTOs;

namespace ServiceLayer.Services.Blog.Abstract
{
    public interface ICategoryService
    {
        Task<CustomResponseDto<List<CategoryListDTO>>> GetAllCategoriesAsync();
        Task<CustomResponseDto<CategoryUpdateDTO>> GetCategoryByIdAsync(int id);
        Task<CustomResponseDto<CategoryAddDTO>> CategoryAddAsync(CategoryAddDTO request);
        Task<CustomResponseDto<NoContentDto>> CategoryUpdateAsync(CategoryUpdateDTO request);
        Task<CustomResponseDto<NoContentDto>> DeleteCategoryAsync(int id);
    }
}
