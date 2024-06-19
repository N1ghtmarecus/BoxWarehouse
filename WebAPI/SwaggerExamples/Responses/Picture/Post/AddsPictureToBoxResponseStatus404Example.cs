using Swashbuckle.AspNetCore.Filters;
using WebAPI.Wrappers;

namespace WebAPI.SwaggerExamples.Responses.Picture.Post
{
    public class AddsPictureToBoxResponseStatus404Example : IExamplesProvider<AddsPictureToBoxResponseStatus404>
    {
        public AddsPictureToBoxResponseStatus404 GetExamples()
        {
            return new AddsPictureToBoxResponseStatus404
            {
                Succeeded = false,
                Message = "Box with Cutter Id 'ID' does not exist."
            };
        }
    }

    public class AddsPictureToBoxResponseStatus404 : Response { }
}
