using Swashbuckle.AspNetCore.Filters;
using WebAPI.Wrappers;

namespace WebAPI.SwaggerExamples.Responses.Identity.Delete
{
    public class DeleteResponseStatus200Example : IExamplesProvider<DeleteResponseStatus200>
    {
        public DeleteResponseStatus200 GetExamples()
        {
            return new DeleteResponseStatus200()
            {
                Succeeded = true,
                Message = "User deleted successfully!"
            };
        }
    }

    public class DeleteResponseStatus200 : Response { }
}
