using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Whu.BLM.NewsSystem.Server.Model
{
    public class JwtOptions
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        
        public SecurityKey SecurityKey => new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secret));
    }
}