using Swashbuckle.AspNetCore.Filters;
using WebAPI.Wrappers;

namespace WebAPI.SwaggerExamples.Responses.Boxes.Get
{
    public class RetrievesSpecificBoxByCutterIdResponseStatus404Example : IExamplesProvider<RetrievesSpecificBoxByCutterIdResponseStatus404>
    {
        public RetrievesSpecificBoxByCutterIdResponseStatus404 GetExamples()
        {
            return new RetrievesSpecificBoxByCutterIdResponseStatus404
            {
                Succeeded = false,
                Message = "Box with cutter ID 'ID' not found",
                Errors = null
            };
        }
    }

    public class RetrievesSpecificBoxByCutterIdResponseStatus404 : Response { }
}
