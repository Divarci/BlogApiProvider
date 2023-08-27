using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityLayer.Blog.DTOs.ArticleDTOs;
using EntityLayer.Blog.Entities;
using EntityLayer.GenericDTOs;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Repository.Abstract;
using RepositoryLayer.UnitOfWOrk.Abstract;
using ServiceLayer.Helpers.Image;
using ServiceLayer.Services.Blog.Abstract;

namespace ServiceLayer.Services.Blog.Concrete
{
    public class ArticleService : IArticleService
    {
        private readonly IGenericRepository<Article> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IImageHelper _imageHelper;

        public ArticleService(IUnitOfWork unitOfWork, IMapper mapper, IImageHelper imageHelper)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetGenericRepository<Article>();
            _mapper = mapper;
            _imageHelper = imageHelper;
        }

        public async Task<CustomResponseDto<List<ArticleListDTO>>> GetArticleListAsync()
        {
            var articleList = await _repository.GetList().Include(x=>x.Category).ProjectTo<ArticleListDTO>(_mapper.ConfigurationProvider).ToListAsync();
            return CustomResponseDto<List<ArticleListDTO>>.Success(200, articleList);
        }

        public async Task<CustomResponseDto<ArticleUpdateDTO>> GetArticleByIdAsync(int id)
        {
            var article = await _repository.Where(x => x.Id == id).ProjectTo<ArticleUpdateDTO>(_mapper.ConfigurationProvider).SingleAsync();
            return CustomResponseDto<ArticleUpdateDTO>.Success(200, article);
        }

        public async Task<CustomResponseDto<NoContentDto>> ArticleAddAsync(ArticleAddDTO request)
        {
            var imageUpload = await _imageHelper.ImageUpload(request.Photo);
            if (imageUpload.Error != null)
            {
                return CustomResponseDto<NoContentDto>.Fail(400, imageUpload.Error);
            }
            request.FileName = imageUpload.FileName!;
            request.FileType = request.Photo.ContentType;

            var article = _mapper.Map<Article>(request);
            await _repository.AddAsync(article);
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<NoContentDto>.Success(201);

        }

        public async Task<CustomResponseDto<NoContentDto>> ArticleUpdateAsync(ArticleUpdateDTO request)
        {
            var existingArticle = await _repository.GetByIdAsync(request.Id);
            string oldPictue = existingArticle.FileName;

            if (request.Photo != null)
            {
                var imageUpload = await _imageHelper.ImageUpload(request.Photo);
                if (imageUpload.Error != null)
                {
                    return CustomResponseDto<NoContentDto>.Fail(400, imageUpload.Error);
                }
                request.FileName = imageUpload.FileName!;
                request.FileType = request.Photo.ContentType;
            }
            else
            {
                request.FileName = existingArticle.FileName;
                request.FileType = existingArticle.FileType;
            }          

            var article = _mapper.Map(request,existingArticle);
            _repository.Update(article);
            await _unitOfWork.CommitAsync();

            if(request.Photo != null)
            {
                _imageHelper.Delete(oldPictue);
            }

            return CustomResponseDto<NoContentDto>.Success(204);
        }

        public async Task<CustomResponseDto<NoContentDto>> ArticleDeleteAsync(int id)
        {
            var article = await _repository.GetByIdAsync(id);
            _repository.Delete(article);
            await _unitOfWork.CommitAsync();
            _imageHelper.Delete(article.FileName);
            return CustomResponseDto<NoContentDto>.Success(201);
        }
    }
}
