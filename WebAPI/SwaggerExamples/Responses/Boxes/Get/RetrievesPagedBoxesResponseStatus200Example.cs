using Swashbuckle.AspNetCore.Filters;
using WebAPI.Wrappers;

namespace WebAPI.SwaggerExamples.Responses.Boxes.Get
{
    public class RetrievesPagedBoxesResponseStatus200Example : IExamplesProvider<RetrievesPagedBoxesResponseStatus200>
    {
        public RetrievesPagedBoxesResponseStatus200 GetExamples()
        {
            return new RetrievesPagedBoxesResponseStatus200
            {
                Succeeded = true,
                Message = "Paged boxes retrieved successfully!"
            };
        }
    }

    public class RetrievesPagedBoxesResponseStatus200 : Response { }
}
