using Swashbuckle.AspNetCore.Filters;
using WebAPI.Wrappers;

namespace WebAPI.SwaggerExamples.Responses.Picture.GET
{
    public class RetrievesPictureByIdResponseStatus404Example : IExamplesProvider<RetrievesPictureByIdResponseStatus404>
    {
        public RetrievesPictureByIdResponseStatus404 GetExamples()
        {
            return new RetrievesPictureByIdResponseStatus404
            {
                Succeeded = false,
                Message = "No picture found for Picture with Id 'ID'."
            };
        }
    }

    public class RetrievesPictureByIdResponseStatus404 : Response { }
}
