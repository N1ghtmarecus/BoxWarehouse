using Swashbuckle.AspNetCore.Filters;
using WebAPI.Wrappers;

namespace WebAPI.SwaggerExamples.Responses.Boxes.Get
{
    public class SearchesBoxesByDimensionResponseStatus400Example : IExamplesProvider<SearchesBoxesByDimensionResponseStatus400>
    {
        public SearchesBoxesByDimensionResponseStatus400 GetExamples()
        {
            return new SearchesBoxesByDimensionResponseStatus400
            {
                Succeeded = false,
                Message = "Enter the correct dimension: length, width or height",
                Errors = null
            };
        }
    }

    public class SearchesBoxesByDimensionResponseStatus400 : Response { }
}
