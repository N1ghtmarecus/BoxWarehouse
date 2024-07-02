using Swashbuckle.AspNetCore.Filters;
using WebAPI.Wrappers;

namespace WebAPI.SwaggerExamples.Responses.Identity.ChangePassword
{
    public class ChangePasswordResponseStatus200Example : IExamplesProvider<ChangePasswordResponseStatus200>
    {
        public ChangePasswordResponseStatus200 GetExamples()
        {
            return new ChangePasswordResponseStatus200
            {
                Succeeded = true,
                Message = "Password changed successfully!"
            };
        }
    }

    public class ChangePasswordResponseStatus200 : Response { }
}
