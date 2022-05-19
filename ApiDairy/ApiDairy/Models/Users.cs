namespace ApiDairy.Models
{
    public class Users
    {
        public int UserId { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string middle_name { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public string role { get; set; }
    }
}
