using Swashbuckle.AspNetCore.Filters;
using WebAPI.Wrappers;

namespace WebAPI.SwaggerExamples.Responses.Identity.Register
{
    public class RegisterResponseStatus500Example : IExamplesProvider<RegisterResponseStatus500>
    {
        public RegisterResponseStatus500 GetExamples()
        {
            return new RegisterResponseStatus500()
            {
                Succeeded = false,
                Message = "User creation failed! Please check user details and try again."
            };
        }
    }

    public class RegisterResponseStatus500 : Response { }
}
