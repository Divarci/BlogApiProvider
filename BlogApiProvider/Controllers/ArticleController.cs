using EntityLayer.Blog.DTOs.ArticleDTOs;
using EntityLayer.Blog.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Exceptions.Filters;
using ServiceLayer.Services.Blog.Abstract;


namespace BlogApiProvider.Controllers
{

    public class ArticleController : CustomBaseController
    {
        private readonly IArticleService _articleService;
        private readonly IHostEnvironment _environment;
        private const string ArticleFolder = "ArticleImages";
        private readonly string _wwwroot;
        public ArticleController(IArticleService articleService, IHostEnvironment environment)
        {
            _articleService = articleService;
            _environment = environment;
            _wwwroot = environment.ContentRootPath + "/wwwroot/";
        }

        
        [HttpGet]
        public async Task<IActionResult> GetArticleList()
        {

            var articleList = await _articleService.GetArticleListAsync();
            return CreateActionResult(articleList);
        }

        [Authorize(Roles = "Member,SuperAdmin")]
        [ServiceFilter(typeof(GenericNotFoundFilter<Article>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetArticleById(int id)
        {

            var article = await _articleService.GetArticleByIdAsync(id);
            return CreateActionResult(article);
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPut]
        public async Task<IActionResult> ArticleUpdate([FromForm] ArticleUpdateDTO request)
        {
            var updatedArticle = await _articleService.ArticleUpdateAsync(request);
            return CreateActionResult(updatedArticle);
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        public async Task<IActionResult> ArticleAdd([FromForm] ArticleAddDTO request)
        {
            var newArticle = await _articleService.ArticleAddAsync(request);
            return CreateActionResult(newArticle);
        }

        [Authorize(Roles = "SuperAdmin")]
        [ServiceFilter(typeof(GenericNotFoundFilter<Article>))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> ArticleDelete(int id)
        {
            var existingArticle = await _articleService.ArticleDeleteAsync(id);
            return CreateActionResult(existingArticle);
        }
          

    }
}
