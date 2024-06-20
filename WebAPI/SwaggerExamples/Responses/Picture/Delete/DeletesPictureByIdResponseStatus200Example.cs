using Swashbuckle.AspNetCore.Filters;
using WebAPI.Wrappers;

namespace WebAPI.SwaggerExamples.Responses.Picture.Delete
{
    public class DeletesPictureByIdResponseStatus200Example : IExamplesProvider<DeletesPictureByIdResponseStatus200>
    {
        public DeletesPictureByIdResponseStatus200 GetExamples()
        {
            return new DeletesPictureByIdResponseStatus200
            {
                Succeeded = true,
                Message = "The picture 'name' has been deleted successfully."
            };
        }
    }

    public class DeletesPictureByIdResponseStatus200 : Response { }
}
