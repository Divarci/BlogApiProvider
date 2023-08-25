using EntityLayer.Blog.DTOs.CategoryDTOs;
using FluentValidation;
using ServiceLayer.Messges;

namespace ServiceLayer.FluentValidaton.Blog.Category
{
    public class CategoryAddDtoValidation : AbstractValidator<CategoryAddDTO>
    {
        public CategoryAddDtoValidation()
        {
            //Add Validation (Parent)
            RuleFor(x=>x.Name)
                .NotEmpty().WithMessage(FluentValidatonMessages.EmptyNullMessage("Name"))
                .NotNull().WithMessage(FluentValidatonMessages.EmptyNullMessage("Name"))
                .MaximumLength(150).WithMessage(FluentValidatonMessages.MaximumLength(150));

        }
    }
}
