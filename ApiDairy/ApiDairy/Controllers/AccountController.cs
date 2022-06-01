using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ApiDairy.Models;
using ApiDairy.Data;

namespace ApiDairy.Controllers
{
    [ApiController]
    [Route ("api/token")]
    public class AccountController : Controller
    {
        private readonly DataContext db;

        public AccountController(DataContext context)
        {
            db = context;
        }

        [HttpPost]
        public IActionResult Token(User user)
        {
            var identity = GetIdentity(user.login, user.password);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Некорректные логин и(или) пароль." });
            }

            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOption.ISSUER,
                    audience: AuthOption.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOption.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOption.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                login = identity.Name //?
            };

            return Ok(response);
        }

        private ClaimsIdentity GetIdentity(string Login, string Password)
        {
            User user = db.users.FirstOrDefault(x => x.login == Login && x.password == Password);
            Role userRole = db.roles.FirstOrDefault(r => r.roleid == user.roleid);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.login),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.role?.name)
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            // если пользователя не найдено
            return null;
        }
    }
}
