namespace BoxWarehouse.Contracts.Responses
{
    public class AuthSuccessResponse
    {    
        public Response? Response { get; set; }
        public string? Token { get; set; }
        public DateTime? Expiration { get; set; }
    }
}
