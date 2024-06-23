using Swashbuckle.AspNetCore.Filters;
using WebAPI.Wrappers;

namespace WebAPI.SwaggerExamples.Responses.Boxes.Get
{
    public class SearchesBoxesByDimensionResponseStatus404Example : IExamplesProvider<SearchesBoxesByDimensionResponseStatus404>
    {
        public SearchesBoxesByDimensionResponseStatus404 GetExamples()
        {
            return new SearchesBoxesByDimensionResponseStatus404
            {
                Succeeded = false,
                Message = "No boxes were found with an exact 'dimension' of 'dimensionValue'mm or within the range 'lowerValue'-'upperValue'mm",
                Errors = null
            };
        }
    }

    public class SearchesBoxesByDimensionResponseStatus404 : Response { }
}
