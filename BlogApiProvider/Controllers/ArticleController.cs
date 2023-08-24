using EntityLayer.Blog.DTOs.ArticleDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.Blog.Abstract;

namespace BlogApiProvider.Controllers
{
    public class ArticleController : CustomBaseController
    {
        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetArticleList()
        {
            var articleList = await _articleService.GetArticleListAsync();
            return CreateActionResult(articleList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetArticleById(int id)
        {
            var article = await _articleService.GetArticleByIdAsync(id);
            return CreateActionResult(article);
        }

        [HttpPut]
        public async Task<IActionResult> ArticleUpdate(ArticleUpdateDTO request)
        {
            var updatedArticle = await _articleService.ArticleUpdateAsync(request);
            return CreateActionResult(updatedArticle);
        }

        [HttpPost]
        public async Task<IActionResult> ArticleAdd(ArticleAddDTO request)
        {
            var newArticle = await _articleService.ArticleAddAsync(request);
            return CreateActionResult(newArticle);
        }

        [HttpDelete]
        public async Task<IActionResult> ArticleDelete(int id)
        {
            var existingArticle = await _articleService.ArticleDeleteAsync(id);
            return CreateActionResult(existingArticle);
        }

    }
}
