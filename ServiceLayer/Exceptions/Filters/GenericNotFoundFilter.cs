using CoreLayer.BaseEntity;
using CoreLayer.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RepositoryLayer.Repository.Abstract;

namespace ServiceLayer.Exceptions.Filters
{
    public class GenericNotFoundFilter<T> : IAsyncActionFilter where T : BaseEntity
    {
        private readonly IGenericRepository<T> _genericRepository;

        public GenericNotFoundFilter(IGenericRepository<T> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var value = context.ActionArguments.Values.FirstOrDefault();

            var idCheck = value is int;
            if (!idCheck)
            {
                context.Result = new NotFoundObjectResult(CustomResponseDto<NoContentDto>.Fail(404, "Id is not valid."));
                return;
            }

            //if id is an int. we cast it to an int. than we check is there any match with this id.
            var id = (int)value!;

            if (id == 0)
            {
                context.Result = new NotFoundObjectResult(CustomResponseDto<NoContentDto>.Fail(404, "Id is not valid."));
                return;
            }

            var entityCheck = await _genericRepository.AnyAsync(x => x.Id == id);
            //if it is exist. no problem
            if (entityCheck)
            {
                await next.Invoke();
                return;
            }

            //if it is not data does not exist
            context.Result = new NotFoundObjectResult(CustomResponseDto<NoContentDto>.Fail(404, "Data is not found."));
            return;

        }
    }
}
