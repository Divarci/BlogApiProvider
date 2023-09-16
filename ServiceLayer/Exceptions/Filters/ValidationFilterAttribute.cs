using CoreLayer.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ServiceLayer.Exceptions.Filters
{
    public class ValidationFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {            
            if (!context.ModelState.IsValid)
            {
                if(context.ModelState.Keys.Count() == 1 && context.ModelState.Keys.Contains("id"))
                {
                    context.Result = new NotFoundObjectResult(CustomResponseDto<NoContentDto>.Fail(404, new List<string> {"Id is not valid."}));
                    return;
                }
                var errors = context.ModelState.Values.SelectMany(x => x.Errors).Select(x=>x.ErrorMessage).ToList();

                context.Result = new BadRequestObjectResult(CustomResponseDto<NoContentDto>.Fail(400, errors));

            }
        }
    }
}
