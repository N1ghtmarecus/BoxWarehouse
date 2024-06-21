using Swashbuckle.AspNetCore.Filters;
using WebAPI.Wrappers;

namespace WebAPI.SwaggerExamples.Responses.Boxes.Get
{
    public class RetrievesSortFieldsResponseStatus200Example : IExamplesProvider<RetrievesSortFieldsResponseStatus200>
    {
        public RetrievesSortFieldsResponseStatus200 GetExamples()
        {
            return new RetrievesSortFieldsResponseStatus200
            {
                Succeeded = true,
                Message = "Examples of fields that can be sorted",
                Errors = null
            };
        }
    }

    public class RetrievesSortFieldsResponseStatus200 : Response { }
}
