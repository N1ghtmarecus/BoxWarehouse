using Swashbuckle.AspNetCore.Filters;
using WebAPI.Wrappers;

namespace WebAPI.SwaggerExamples.Responses.Picture.Post
{
    public class AddsPictureToBoxResponseStatus400Example : IExamplesProvider<AddsPictureToBoxResponseStatus400>
    {
        public AddsPictureToBoxResponseStatus400 GetExamples()
        {
            return new AddsPictureToBoxResponseStatus400
            {
                Succeeded = false,
                Message = "Failed to add picture to the box."
            };
        }
    }

    public class AddsPictureToBoxResponseStatus400 : Response { }
}
