using MasterClassApi.Core.Entities;
using MasterClassApi.Core.Services;
using MasterClassApi.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MasterClassApi.Services
{
    public class LocalUserService : IUserService
    {

        private readonly JwtSettings jwtSettings;
        private List<User> users = new List<User>()
        {
            new User{Id = 1, Name = "Guillermo", LastName="Pérez", Email="gperez@solidq.com",Password="test",Username="gperez", Roles = new []{"User","Admin" }},
            new User{Id = 2, Name = "Miguel", LastName="López", Email="mlopez@solidq.com",Password="test",Username="mlopez", Roles= new []{"User","Publisher" } }
        };

        public LocalUserService(IOptions<JwtSettings> jwtSettings)
        {
            if (jwtSettings?.Value==null)
            {
                throw new ArgumentNullException(nameof(jwtSettings),"Invalid users settings value");
            }

            this.jwtSettings = jwtSettings?.Value;
            
        }

        public string Authenticate(string username, string password)
        {
            //Check if username+password exists (login logic)
            var user = users.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user == null)
                return null;
            #region "Create token process"
            //User exists, create a new JWT token
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtSettings.Key);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name,$"{user.LastName}, {user.Name}"),
                    new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email), 

                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            foreach(var role in user.Roles)
            {
                tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, role));
            }
            
            var token = handler.CreateToken(tokenDescriptor);
            
            return handler.WriteToken(token);
            #endregion

        }

        public User Get(int id)
        {
            return users.FirstOrDefault(u => u.Id == id);
        }

        public Task<User> GetAsync(int id)
        {
            return Task.FromResult(Get(id));
        }
    }
}
