using EntityLayer.Blog.DTOs.ArticleDTOs;
using EntityLayer.Blog.Entities;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Exceptions.Filters;
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

        [ServiceFilter(typeof(GenericNotFoundFilter<Article>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetArticleById(int id)
        {
            var article = await _articleService.GetArticleByIdAsync(id);
            return CreateActionResult(article);
        }
        [HttpPut]
        public async Task<IActionResult> ArticleUpdate([FromForm]ArticleUpdateDTO request)
        {
            var updatedArticle = await _articleService.ArticleUpdateAsync(request);
            return CreateActionResult(updatedArticle);
        }

        [HttpPost]
        public async Task<IActionResult> ArticleAdd([FromForm]ArticleAddDTO request)
        {
            var newArticle = await _articleService.ArticleAddAsync(request);
            return CreateActionResult(newArticle);
        }
        [ServiceFilter(typeof(GenericNotFoundFilter<Article>))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> ArticleDelete(int id)
        {
            var existingArticle = await _articleService.ArticleDeleteAsync(id);
            return CreateActionResult(existingArticle);
        }

    }
}
