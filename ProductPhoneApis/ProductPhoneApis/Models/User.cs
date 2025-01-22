namespace ProductPhoneApis.Models
{
    public class TokenReturn
    {
        public string token { get; set; }

        public int rol { get; set; }
    }
    public class LoginUser
    {
        public string UserName { get; set; } = "";
        public string Password { get; set; } = "";
    }
}
