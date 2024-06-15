using Swashbuckle.AspNetCore.Filters;
using WebAPI.Wrappers;

namespace WebAPI.SwaggerExamples.Responses.Identity.Login
{
    public class LoginResponseStatus401Example : IExamplesProvider<LoginResponseStatus401>
    {
        public LoginResponseStatus401 GetExamples()
        {
            return new LoginResponseStatus401
            {
                Succeeded = false,
                Message = "Invalid credentials!"
            };
        }
    }

    public class LoginResponseStatus401 : Response { }
}
