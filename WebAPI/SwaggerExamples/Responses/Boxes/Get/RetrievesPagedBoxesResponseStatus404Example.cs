using Swashbuckle.AspNetCore.Filters;
using WebAPI.Wrappers;

namespace WebAPI.SwaggerExamples.Responses.Boxes.Get
{
    public class RetrievesPagedBoxesResponseStatus404Example : IExamplesProvider<RetrievesPagedBoxesResponseStatus404>
    {
        public RetrievesPagedBoxesResponseStatus404 GetExamples()
        {
            return new RetrievesPagedBoxesResponseStatus404
            {
                Succeeded = false,
                Message = "No boxes found.",
                Errors = null
            };
        }
    }

    public class RetrievesPagedBoxesResponseStatus404 : Response { }
}
