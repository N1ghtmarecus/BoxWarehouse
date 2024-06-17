using Swashbuckle.AspNetCore.Filters;
using WebAPI.Wrappers;

namespace WebAPI.SwaggerExamples.Responses.Picture.GET
{
    public class RetrievesPicturesByCutterIdResponseStatus404Example : IExamplesProvider<RetrievesPicturesByCutterIdResponseStatus404>
    {
        public RetrievesPicturesByCutterIdResponseStatus404 GetExamples()
        {
            return new RetrievesPicturesByCutterIdResponseStatus404
            {
                Succeeded = false,
                Message = "No pictures found for Box with Cutter Id 'ID'."
            };
        }
    }

    public class RetrievesPicturesByCutterIdResponseStatus404 : Response { }
}
