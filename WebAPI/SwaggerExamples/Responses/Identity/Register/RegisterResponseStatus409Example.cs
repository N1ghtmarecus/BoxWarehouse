using Swashbuckle.AspNetCore.Filters;
using WebAPI.Models;
using WebAPI.Wrappers;

namespace WebAPI.SwaggerExamples.Responses.Identity.Register
{
    public class RegisterResponseStatus409Example : IExamplesProvider<RegisterResponseStatus409>
    {
        public RegisterResponseStatus409 GetExamples()
        {
            return new RegisterResponseStatus409()
            {
                Succeeded = false,
                Message = "User with this username already exists!"
            };
        }
    }

    public class RegisterResponseStatus409 : Response { }
}

