using EntityLayer.Blog.DTOs.ArticleDTOs;
using FluentValidation;
using ServiceLayer.Messges;

namespace ServiceLayer.FluentValidaton.Blog.Article
{
    public class ArticleAddDtoValidation : AbstractValidator<ArticleAddDTO>
    {
        public ArticleAddDtoValidation()
        {
            //Article Validation (Child)
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage(FluentValidatonMessages.EmptyNullMessage("Title"))
                .NotNull().WithMessage(FluentValidatonMessages.EmptyNullMessage("Title"))
                .MaximumLength(150).WithMessage(FluentValidatonMessages.MaximumLength(150));
            RuleFor(x => x.Author)
                .NotEmpty().WithMessage(FluentValidatonMessages.EmptyNullMessage("Author"))
                .NotNull().WithMessage(FluentValidatonMessages.EmptyNullMessage("Author"))
                .MaximumLength(150).WithMessage(FluentValidatonMessages.MaximumLength(75));
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage(FluentValidatonMessages.EmptyNullMessage("Content"))
                .NotNull().WithMessage(FluentValidatonMessages.EmptyNullMessage("Content"));

            //Picture Section Validation (Child)
            RuleFor(x => x.Photo)
                .NotEmpty().WithMessage(FluentValidatonMessages.EmptyNullMessage("Picture"))
                .NotNull().WithMessage(FluentValidatonMessages.EmptyNullMessage("Picture"));
        
        }
    }
}
