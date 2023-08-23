using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityLayer.Blog.DTOs.CategoryDTOs;
using EntityLayer.Blog.Entities;
using EntityLayer.GenericDTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using RepositoryLayer.Repository.Abstract;
using RepositoryLayer.UnitOfWOrk.Abstract;
using ServiceLayer.Services.Blog.Abstract;

namespace ServiceLayer.Services.Blog.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Category> _repository;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;
        private const string CategoryCache = "category";

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper, IMemoryCache cache)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetGenericRepository<Category>();
            _mapper = mapper;
            _cache = cache;

            if (!_cache.TryGetValue(CategoryCache, out _))
            {
                _cache.Set(CategoryCache, _unitOfWork.GetGenericRepository<Category>().GetList().ToListAsync().Result);
            }
        }

        public async Task CacheCategoryAsync()
        {
            _cache.Set(CategoryCache, await _unitOfWork.GetGenericRepository<Category>().GetList().ToListAsync());
        }

        public Task<CustomResponseDto<List<CategoryListDTO>>> GetAllCategoriesAsync()
        {
            var categories = _cache.Get<List<Category>>(CategoryCache);
            var categoryDtoList = _mapper.Map<List<CategoryListDTO>>(categories);
            return Task.FromResult(CustomResponseDto<List<CategoryListDTO>>.Success(200, categoryDtoList));
        }

        public Task<CustomResponseDto<CategoryUpdateDTO>> GetCategoryByIdAsync(int id)
        {
            var category = _cache.Get<List<Category>>(CategoryCache)!.FirstOrDefault(x => x.Id == id);
            var categoryUpdateDto = _mapper.Map<CategoryUpdateDTO>(category);
            return Task.FromResult(CustomResponseDto<CategoryUpdateDTO>.Success(200, categoryUpdateDto));
        }

        public async Task<CustomResponseDto<NoContentDto>> CategoryUpdateAsync(CategoryUpdateDTO request)
        {
            var existingCategory = await _repository.GetByIdAsync(request.Id);
            var updatedCategory = _mapper.Map(request, existingCategory);
            _repository.Update(updatedCategory);
            await _unitOfWork.CommitAsync();
            await CacheCategoryAsync();
            return CustomResponseDto<NoContentDto>.Success(204);
        }

        public async Task<CustomResponseDto<CategoryAddDTO>> CategoryAddAsync(CategoryAddDTO request)
        {
            var newCategory = _mapper.Map<Category>(request);
            await _repository.AddAsync(newCategory);
            await _unitOfWork.CommitAsync();
            await CacheCategoryAsync();
            return CustomResponseDto<CategoryAddDTO>.Success(201, request);
        }

        public async Task<CustomResponseDto<NoContentDto>> DeleteCategoryAsync(int id)
        {
            var existingCategory = await _repository.GetByIdAsync(id);
            _repository.Delete(existingCategory);
            await _unitOfWork.CommitAsync();
            await CacheCategoryAsync();
            return CustomResponseDto<NoContentDto>.Success(204);

        }

    }
}
