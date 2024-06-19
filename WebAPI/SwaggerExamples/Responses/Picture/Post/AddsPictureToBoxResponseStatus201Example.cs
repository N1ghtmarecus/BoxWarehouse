using Swashbuckle.AspNetCore.Filters;
using WebAPI.Wrappers;

namespace WebAPI.SwaggerExamples.Responses.Picture.Post
{
    public class AddsPictureToBoxResponseStatus201Example : IExamplesProvider<AddsPictureToBoxResponseStatus201>
    {
        public AddsPictureToBoxResponseStatus201 GetExamples()
        {
            return new AddsPictureToBoxResponseStatus201
            {
                Succeeded = true,
                Message = "Picture 'name' added to the box with cutter id 'ID' successfully."
            };
        }
    }

    public class AddsPictureToBoxResponseStatus201 : Response { }
}
