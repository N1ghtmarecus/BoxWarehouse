using BoxWarehouse.Contracts.Requests;
using BoxWarehouse.Contracts.Responses;
using Refit;

namespace BoxWarehouse.Sdk.Sample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //var cachedToken = string.Empty;

            //var identityApi = RestService.For<IIdentityApi>("https://localhost:44347/");
            //var boxWarehouseApi = RestService.For<IBoxWarehouseApi>("https://localhost:44347/", new RefitSettings
            //{
            //    AuthorizationHeaderValueGetter = () => Task.FromResult(cachedToken)
            //});

            //var register = await identityApi.RegisterUserAsync(new RegisterModel()
            //{
            //    Email = "sdkaccount@gmail.com",
            //    Username = "sdkaccount",
            //    Password = "Pa$$w0rD123!"
            //});

            //var login = await identityApi.LoginAsync(new LoginModel()
            //{
            //    Username = "sdkaccount",
            //    Password = "Pa$$w0rD123!"
            //});


            //cachedToken = login.Content?.Token;


            //var createdBox = await boxWarehouseApi.CreateBoxAsync(new CreateBoxDto
            //{
            //    CutterID = 115,
            //    Fefco = 201,
            //    Length = 100,
            //    Width = 100,
            //    Height = 100
            //});

            //var retrievedBox = await boxWarehouseApi.GetBoxAsync(createdBox.Content!.Data!.CutterID);

            //await boxWarehouseApi.UpdateBoxAsync(new BoxDto
            //{
            //    CutterID = retrievedBox.Content!.Data!.CutterID,
            //    Fefco = 201,
            //    Length = 300,
            //    Width = 300,
            //    Height = 300
            //});

            //await boxWarehouseApi.DeleteBoxAsync(retrievedBox.Content!.Data!.CutterID);
        }
    }
}
