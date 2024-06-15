using Swashbuckle.AspNetCore.Filters;
using WebAPI.Wrappers;

namespace WebAPI.SwaggerExamples.Responses.Identity.Delete
{
    public class DeleteResponseStatus500Example : IExamplesProvider<DeleteResponseStatus500>
    {
        public DeleteResponseStatus500 GetExamples()
        {
            return new DeleteResponseStatus500
            {
                Succeeded = false,
                Message = "User deletion failed!"
            };
        }
    }

    public class DeleteResponseStatus500 : Response { }
}
