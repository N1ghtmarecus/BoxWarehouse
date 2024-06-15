using Swashbuckle.AspNetCore.Filters;
using WebAPI.Wrappers;

namespace WebAPI.SwaggerExamples.Responses.Identity.Register
{
    public class RegisterResponseStatus200Example : IExamplesProvider<RegisterResponseStatus200>
    {
        RegisterResponseStatus200 IExamplesProvider<RegisterResponseStatus200>.GetExamples()
        {
            return new RegisterResponseStatus200()
            {
                Succeeded = true,
                Message = "User created successfully!"
            };
        }
    }

    public class RegisterResponseStatus200 : Response { }
}
