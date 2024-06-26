namespace WebAPI.Wrappers
{
    public class AuthSuccessResponse
    {    
        public Response? Response { get; set; }
        public string? Token { get; set; }
        public DateTime? Expiration { get; set; }
    }
}
