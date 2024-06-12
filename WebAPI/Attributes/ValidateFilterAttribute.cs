using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebAPI.Wrappers;

namespace WebAPI.Attributes
{
    public class ValidateFilterAttribute : ResultFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            base.OnResultExecuting(context);

            if (!context.ModelState.IsValid)
            {
                var entry = context.ModelState.Values.FirstOrDefault();

                context.Result = new BadRequestObjectResult(new Response(false, "Something went wrong!", entry?.Errors.Select(x => x.ErrorMessage)));
            }
        }
    }
}
