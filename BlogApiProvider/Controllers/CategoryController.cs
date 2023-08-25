using EntityLayer.Blog.DTOs.CategoryDTOs;
using EntityLayer.Blog.Entities;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Exceptions.Exceptions;
using ServiceLayer.Exceptions.Filters;
using ServiceLayer.Services.Blog.Abstract;

namespace BlogApiProvider.Controllers
{
    public class CategoryController : CustomBaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return CreateActionResult(categories);
        }

        [ServiceFilter(typeof(GenericNotFoundFilter<Category>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            return CreateActionResult(category);
        }
        [ServiceFilter(typeof(GenericNotFoundFilter<Category>))]
        [HttpPut]
        public async Task<IActionResult> CategoryUpdate(CategoryUpdateDTO request)
        {
            var updatedCategory = await _categoryService.CategoryUpdateAsync(request);
            return CreateActionResult(updatedCategory);
        }

        [HttpPost]
        public async Task<IActionResult> CategoryAdd(CategoryAddDTO request)
        {
            var newCategory = await _categoryService.CategoryAddAsync(request);
            return CreateActionResult(newCategory);
        }

        [ServiceFilter(typeof(GenericNotFoundFilter<Category>))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> CategoryDelete(int id)
        {
            var existingCategory = await _categoryService.DeleteCategoryAsync(id);
            return CreateActionResult(existingCategory);
        }



    }
}
