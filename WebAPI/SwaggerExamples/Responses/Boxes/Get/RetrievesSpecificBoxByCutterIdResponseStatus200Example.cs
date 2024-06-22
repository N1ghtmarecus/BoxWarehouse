using Swashbuckle.AspNetCore.Filters;
using WebAPI.Wrappers;

namespace WebAPI.SwaggerExamples.Responses.Boxes.Get
{
    public class RetrievesSpecificBoxByCutterIdResponseStatus200Example : IExamplesProvider<RetrievesSpecificBoxByCutterIdResponseStatus200>
    {
        public RetrievesSpecificBoxByCutterIdResponseStatus200 GetExamples()
        {
            return new RetrievesSpecificBoxByCutterIdResponseStatus200
            {
                Succeeded = true,
                Message = "Box with cutter ID 'ID' retrieved successfully!"
            };
        }
    }

    public class RetrievesSpecificBoxByCutterIdResponseStatus200 : Response { }
}
