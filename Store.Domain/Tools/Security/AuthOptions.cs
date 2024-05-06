using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Store.Domain.Tools.Security
{
    public class AuthOptions
    {
        public string Issuer { get; set; } //издатель
        public string Audience { get; set; } //потребитель
        public string SecretKey { get; set; }
        public bool ValidateAudience { get; set; }
        public bool ValidateIssuer { get; set; }
        public bool ValidateLifetime { get; set; }
        public bool ValidateIssuerSigningKey { get; set; }
        public int TokenLifetime { get; set; }
    }
}
