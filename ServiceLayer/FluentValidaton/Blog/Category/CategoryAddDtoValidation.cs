using EntityLayer.Blog.DTOs.CategoryDTOs;
using FluentValidation;

namespace ServiceLayer.FluentValidaton.Blog.Category
{
    public class CategoryAddDtoValidation : AbstractValidator<CategoryAddDTO>
    {
        public CategoryAddDtoValidation()
        {
            RuleFor(x=>x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .NotNull().WithMessage("Name is required.")
                .MaximumLength(150).WithMessage("You are allowed to use maximum 150 character.");
        }
    }
}
