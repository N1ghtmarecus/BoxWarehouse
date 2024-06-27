using BoxWarehouse.Contracts.Requests;
using BoxWarehouse.Contracts.Responses;
using Refit;

namespace BoxWarehouse.Sdk
{
    [Headers("Authorization: Bearer")]
    public interface IBoxWarehouseApi
    {
        [Get("/api/boxes/{cutterId}")]
        Task<ApiResponse<Response<BoxDto>>> GetBoxAsync(int cutterId);

        [Post("/api/boxes")]
        Task<ApiResponse<Response<BoxDto>>> CreateBoxAsync(CreateBoxDto newBox);

        [Put("/api/boxes")]
        Task UpdateBoxAsync(BoxDto updateBox);

        [Delete("/api/boxes/{cutterId}")]
        Task DeleteBoxAsync(int cutterId);
    }
}
