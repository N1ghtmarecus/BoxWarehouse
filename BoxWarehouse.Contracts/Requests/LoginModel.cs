using System.ComponentModel;

namespace BoxWarehouse.Contracts.Requests
{
    public class LoginModel
    {
        [DefaultValue("MaciejAdmin")]
        public string? Username { get; set; }

        [DefaultValue("Pa$$w0rD123!")]
        public string? Password { get; set; }
    }
}
