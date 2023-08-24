using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityLayer.Blog.DTOs.CategoryDTOs;
using EntityLayer.Blog.Entities;
using EntityLayer.GenericDTOs;
using Microsoft.EntityFrameworkCore;
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
        

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetGenericRepository<Category>();
            _mapper = mapper;           

        }       

        public async Task<CustomResponseDto<List<CategoryListDTO>>> GetAllCategoriesAsync()
        {
            var categories = await _repository.GetList().ProjectTo<CategoryListDTO>(_mapper.ConfigurationProvider).ToListAsync();
            return CustomResponseDto<List<CategoryListDTO>>.Success(200, categories);
        }

        public async Task<CustomResponseDto<CategoryUpdateDTO>> GetCategoryByIdAsync(int id)
        {
            var category =await _repository.Where(x=>x.Id == id).ProjectTo<CategoryUpdateDTO>(_mapper.ConfigurationProvider).SingleAsync();
            return CustomResponseDto<CategoryUpdateDTO>.Success(200, category);
        }

        public async Task<CustomResponseDto<NoContentDto>> CategoryUpdateAsync(CategoryUpdateDTO request)
        {
            var existingCategory = await _repository.GetByIdAsync(request.Id);
            var updatedCategory = _mapper.Map(request, existingCategory);
            _repository.Update(updatedCategory);
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<NoContentDto>.Success(204);
        }

        public async Task<CustomResponseDto<CategoryAddDTO>> CategoryAddAsync(CategoryAddDTO request)
        {
            var newCategory = _mapper.Map<Category>(request);
            await _repository.AddAsync(newCategory);
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<CategoryAddDTO>.Success(201, request);
        }

        public async Task<CustomResponseDto<NoContentDto>> DeleteCategoryAsync(int id)
        {
            var existingCategory = await _repository.GetByIdAsync(id);
            _repository.Delete(existingCategory);
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<NoContentDto>.Success(204);

        }

    }
}
