using Swashbuckle.AspNetCore.Filters;
using WebAPI.Wrappers;

namespace WebAPI.SwaggerExamples.Responses.Picture.Delete
{
    public class DeletesPictureByIdResponseStatus404Example : IExamplesProvider<DeletesPictureByIdResponseStatus404>
    {
        public DeletesPictureByIdResponseStatus404 GetExamples()
        {
            return new DeletesPictureByIdResponseStatus404
            {
                Succeeded = false,
                Message = "Picture with ID 'ID' currently does not exist."
            };
        }
    }

    public class DeletesPictureByIdResponseStatus404 : Response { }
}
