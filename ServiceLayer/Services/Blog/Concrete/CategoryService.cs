using AutoMapper;
using AutoMapper.QueryableExtensions;
using CoreLayer.Response;
using EntityLayer.Blog.DTOs.CategoryDTOs;
using EntityLayer.Blog.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
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
        private readonly IHostEnvironment _environment;
        private readonly string _wwwroot;
        private const string ArticleFolder = "ArticleImages";

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper, IHostEnvironment environment)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetGenericRepository<Category>();
            _mapper = mapper;
            _environment = environment;
            _wwwroot = environment.ContentRootPath + "/wwwroot/";
        }

        public async Task<CustomResponseDto<List<CategoryListDTO>>> GetAllCategoriesAsync()
        {
            var categories = await _repository.GetList().Include(x => x.Articles).ProjectTo<CategoryListDTO>(_mapper.ConfigurationProvider).ToListAsync();
            foreach (var category in categories)
            {
                if (category.Articles != null)
                {
                    foreach (var article in category.Articles)
                    {
                        var path = Path.Combine(_wwwroot + $"{ArticleFolder}/", article.FileName!);
                        var fileBytes = File.ReadAllBytes(path);
                        article.FileByte = fileBytes;
                    }
                }
                

            }


            return CustomResponseDto<List<CategoryListDTO>>.Success(200, categories);
        }

        public async Task<CustomResponseDto<CategoryUpdateDTO>> GetCategoryByIdAsync(int id)
        {
            var category = await _repository.Where(x => x.Id == id).ProjectTo<CategoryUpdateDTO>(_mapper.ConfigurationProvider).SingleAsync();
            return CustomResponseDto<CategoryUpdateDTO>.Success(200, category);
        }

        public async Task<CustomResponseDto<NoContentDto>> CategoryUpdateAsync(CategoryUpdateDTO request)
        {
            var existingCategory = await _repository.Where(x => x.Id == request.Id).AsNoTracking().FirstOrDefaultAsync();
            var updatedCategory = _mapper.Map<Category>(request);
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
