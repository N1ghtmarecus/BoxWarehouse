using Swashbuckle.AspNetCore.Filters;
using WebAPI.Wrappers;

namespace WebAPI.SwaggerExamples.Responses.Picture.GET
{
    public class RetrievesPicturesByCutterIdResponseStatus200Example : IExamplesProvider<RetrievesPicturesByCutterIdResponseStatus200>
    {
        public RetrievesPicturesByCutterIdResponseStatus200 GetExamples()
        {
            return new RetrievesPicturesByCutterIdResponseStatus200
            {
                Succeeded = true,
                Message = "Retrieved 'x' pictures for Box with Cutter Id 'ID'."
            };
        }
    }

    public class RetrievesPicturesByCutterIdResponseStatus200 : Response { }
}
