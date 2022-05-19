using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ApiDairy
{
    public class AuthOption
    {
        public const string ISSUER = "Server"; // издатель токена
        public const string AUDIENCE = "User"; // потребитель токена
        const string KEY = "apiDairy";   // ключ для шифрации
        public const int LIFETIME = 5; // время жизни токена - 5 минута
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
