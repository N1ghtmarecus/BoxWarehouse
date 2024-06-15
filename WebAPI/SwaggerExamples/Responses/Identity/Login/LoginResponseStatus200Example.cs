using Swashbuckle.AspNetCore.Filters;
using WebAPI.Wrappers;

namespace WebAPI.SwaggerExamples.Responses.Identity.Login
{
    public class LoginResponseStatus200Example : IExamplesProvider<LoginResponseStatus200>
    {
        public LoginResponseStatus200 GetExamples()
        {
            return new LoginResponseStatus200
            {
                Succeeded = true,
                Message = "User logged in successfully!",
            };
        }
    }

    public class LoginResponseStatus200 : Response { }
}
