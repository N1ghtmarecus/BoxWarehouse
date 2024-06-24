using Swashbuckle.AspNetCore.Filters;
using WebAPI.Wrappers;

namespace WebAPI.SwaggerExamples.Responses.Boxes.Post
{
    public class CreatesNewBoxResponseStatus201Example : IExamplesProvider<CreatesNewBoxResponseStatus201>
    {
        public CreatesNewBoxResponseStatus201 GetExamples()
        {
            return new CreatesNewBoxResponseStatus201
            {
                Succeeded = true,
                Message = "New box with cutter ID 'ID' created successfully"
            };
        }
    }

    public class CreatesNewBoxResponseStatus201 : Response { }
}
