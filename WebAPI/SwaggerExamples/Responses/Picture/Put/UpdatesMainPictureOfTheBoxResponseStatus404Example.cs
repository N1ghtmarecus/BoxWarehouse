using Swashbuckle.AspNetCore.Filters;
using WebAPI.Wrappers;

namespace WebAPI.SwaggerExamples.Responses.Picture.Put
{
    public class UpdatesMainPictureOfTheBoxResponseStatus404Example : IExamplesProvider<UpdatesMainPictureOfTheBoxResponseStatus404>
    {
        public UpdatesMainPictureOfTheBoxResponseStatus404 GetExamples()
        {
            return new UpdatesMainPictureOfTheBoxResponseStatus404
            {
                Succeeded = false,
                Message = "Picture with ID 'ID' does not exist."
            };
        }
    }

    public class UpdatesMainPictureOfTheBoxResponseStatus404 : Response { }
}
