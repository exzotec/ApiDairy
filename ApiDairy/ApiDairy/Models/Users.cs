namespace ApiDairy.Models
{
    //пользоваетль
    public class User
    {
        public string UserId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
