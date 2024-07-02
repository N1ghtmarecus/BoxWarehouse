using Swashbuckle.AspNetCore.Filters;
using WebAPI.Wrappers;

namespace WebAPI.SwaggerExamples.Responses.Identity.ChangePassword
{
    public class ChangePasswordResponseStatus400Example : IExamplesProvider<ChangePasswordResponseStatus400>
    {
        public ChangePasswordResponseStatus400 GetExamples()
        {
            return new ChangePasswordResponseStatus400
            {
                Succeeded = false,
                Message = "Password change failed! Please check user details and try again."
            };
        }
    }

    public class ChangePasswordResponseStatus400 : Response { }
}
