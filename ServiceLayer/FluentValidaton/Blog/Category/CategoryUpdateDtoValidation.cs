using EntityLayer.Blog.DTOs.CategoryDTOs;
using FluentValidation;
using ServiceLayer.Messges;

namespace ServiceLayer.FluentValidaton.Blog.Category
{
    public class CategoryUpdateDtoValidation : AbstractValidator<CategoryUpdateDTO>
    {
        public CategoryUpdateDtoValidation()
        {
            RuleFor(x => x.Name)
              .NotEmpty().WithMessage(FluentValidatonMessages.EmptyNullMessage("Name"))
              .NotNull().WithMessage(FluentValidatonMessages.EmptyNullMessage("Name"))
              .MaximumLength(150).WithMessage(FluentValidatonMessages.MaximumLength(150));
        }
    }
}
