using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Configuration;
using System.Web.Http;
using WebApplication8.Models;

namespace WebApplication8.Controllers
{
    [Authorize]
    public class SalesModuleController : ApiController
    {
        [HttpGet]
        [Authorize(Roles = RoleNames.ROLE_SalesMudole+ "," + RoleNames.ROLE_ADMINISTRATOR)]
        public IHttpActionResult GoToSalesApp()
        {
            string JWT = "";
            string username = User.Identity.Name;
            JWT = GenerateToken(username);


            return Ok(new { token = JWT });
        }
        //generate token 

        //passit to sales app
        public static string GetSecret()
        {
            string userName = WebConfigurationManager.AppSettings["JWTKey"];

            return userName;
        }
        public static string GenerateToken(string username)
        {
            string expireMinutes = WebConfigurationManager.AppSettings["DurationInMinutes"];
            var symmetricKey = Convert.FromBase64String(GetSecret());
            var tokenHandler = new JwtSecurityTokenHandler();

            var now = DateTime.UtcNow;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),

                Expires = now.AddMinutes(Convert.ToInt32(expireMinutes)),

                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(symmetricKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var stoken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(stoken);

            return token;
        }
    }


}
