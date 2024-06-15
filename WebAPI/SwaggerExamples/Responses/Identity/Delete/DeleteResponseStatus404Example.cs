using Swashbuckle.AspNetCore.Filters;
using WebAPI.Wrappers;

namespace WebAPI.SwaggerExamples.Responses.Identity.Delete
{
    public class DeleteResponseStatus404Example : IExamplesProvider<DeleteResponseStatus404>
    {
        public DeleteResponseStatus404 GetExamples()
        {
            return new DeleteResponseStatus404()
            {
                Succeeded = false,
                Message = "User not found!"
            };
        }
    }

    public class DeleteResponseStatus404 : Response { }
}
