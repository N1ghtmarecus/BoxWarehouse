using Swashbuckle.AspNetCore.Filters;
using WebAPI.Wrappers;

namespace WebAPI.SwaggerExamples.Responses.Boxes.Get
{
    public class SearchesBoxesByDimensionResponseStatus200Example : IExamplesProvider<SearchesBoxesByDimensionResponseStatus200>
    {
        public SearchesBoxesByDimensionResponseStatus200 GetExamples()
        {
            return new SearchesBoxesByDimensionResponseStatus200
            {
                Succeeded = true,
                Message =
                    [
                        "Found 'quantity' boxes with an exact 'dimension' of 'dimensionValue'mm",
                        "No boxes were found with an exact 'dimension' of 'dimensionValue'mm. Found 'quantity' boxes within the range 'lowerValue'-'upperValue'mm"
                    ]
            };
        }
    }

    public class SearchesBoxesByDimensionResponseStatus200 : Response
    {
        public new bool Succeeded { get; set; }
        public new List<string>? Message { get; set; }
    }
}
