using CoreLayer.BaseEntity;
using EntityLayer.GenericDTOs;
using Microsoft.AspNetCore.Http.HttpResults;
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
                    
            //if id is an int. we cast it to an int. than we check is there any match with this id.
            var id = (int)value!;
            var entityCheck = await _genericRepository.AnyAsync(x => x.Id == id);
            //if it is exist. no problem
            if (entityCheck)
            {
                await next.Invoke();
                return;
            }

            //if it is not data does not exist
            context.Result = new NotFoundObjectResult(CustomResponseDto<NoContentDto>.Fail(404, $"Data {id} is not found."));
            return;

        }
    }
}
