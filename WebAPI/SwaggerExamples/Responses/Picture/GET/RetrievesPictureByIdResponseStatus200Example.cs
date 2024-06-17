using Swashbuckle.AspNetCore.Filters;
using WebAPI.Wrappers;

namespace WebAPI.SwaggerExamples.Responses.Picture.GET
{
    public class RetrievesPictureByIdResponseStatus200Example : IExamplesProvider<RetrievesPictureByIdResponseStatus200>
    {
        public RetrievesPictureByIdResponseStatus200 GetExamples()
        {
            return new RetrievesPictureByIdResponseStatus200
            {
                Succeeded = true,
                Message = "Picture 'name' retrieved successfully."
            };
        }
    }
    
    public class RetrievesPictureByIdResponseStatus200 : Response { }
}
