using AuthApp.Form;
using AuthApp.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthApp.Service
{

    public partial interface IUserService
    {
         string authenticate(LoginForm form);

        List<User> get();
    }
    public class UserService : IUserService
    {
        private IHashingService hashingService;
        public UserService(IHashingService hashingService)
        {
            this.hashingService = hashingService;
        }
        private List<User> users = new List<User>()
        {
            new User{id = "1", username = "haine", 
                password = "$2b$10$NVR/k9OxHtuC08wJ1UY8aO3.XaynIkDixdteCjsuvkPdKSi.yfrCG", 
                email = "99haipham@gmail.com", 
                fullName = "Phạm Minh Hải" , 
                phone = "0907386696" , 
                role = new Role {id = "1", name = "admin" ,desc ="this is admin" }
            }
        };

        public string authenticate(LoginForm form)
        {
            string token = "";
            hashingService.HashPassword(form.password);
            var user = users.FirstOrDefault(s => {
                return (s.username == form.username && hashingService.CheckPassword(form.password,s.password));
                });

            if(user != null)
            {
                var tokenParser = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("this-is-fucking-long-secret-key-dudeee");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
               {
                    new Claim(ClaimTypes.Name, user.id.ToString()),
                    new Claim(ClaimTypes.Role, user.role.name)
               }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenCreated = tokenParser.CreateToken(tokenDescriptor);
                token = tokenParser.WriteToken(tokenCreated);
            }
            return token;
        }

        public List<User> get()
        {
            return this.users;
        }
    }
}
