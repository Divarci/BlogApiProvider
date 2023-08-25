using EntityLayer.Blog.DTOs.ArticleDTOs;
using FluentValidation;
using ServiceLayer.Messges;

namespace ServiceLayer.FluentValidaton.Blog.Article
{
    public class ArticleUpdateDtoValidation : AbstractValidator<ArticleUpdateDTO>
    {
        public ArticleUpdateDtoValidation()
        {
            //Article Validation (Child)
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage(FluentValidatonMessages.EmptyNullMessage("Id"))
                .NotNull().WithMessage(FluentValidatonMessages.EmptyNullMessage("Id"))
                .Must(id=>BeAnInteger(id.ToString())).WithMessage(FluentValidatonMessages.MustBeInteger());
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

            //Category Validation
            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage(FluentValidatonMessages.EmptyNullMessage("Category"))
                .NotNull().WithMessage(FluentValidatonMessages.EmptyNullMessage("Category"));


        }

        private bool BeAnInteger(string id)
        {
            return int.TryParse(id, out _);
        }
    }
}
