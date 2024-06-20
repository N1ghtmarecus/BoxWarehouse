using Swashbuckle.AspNetCore.Filters;
using WebAPI.Wrappers;

namespace WebAPI.SwaggerExamples.Responses.Picture.Put
{
    public class UpdatesMainPictureOfTheBoxResponseStatus200Example : IExamplesProvider<UpdatesMainPictureOfTheBoxResponseStatus200>
    {
        public UpdatesMainPictureOfTheBoxResponseStatus200 GetExamples()
        {
            return new UpdatesMainPictureOfTheBoxResponseStatus200
            {
                Succeeded = true,
                Message = "The main picture of the box has been updated successfully."
            };
        }
    }

    public class UpdatesMainPictureOfTheBoxResponseStatus200 : Response { }
}
