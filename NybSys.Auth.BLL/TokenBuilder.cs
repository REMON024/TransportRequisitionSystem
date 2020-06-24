using Microsoft.IdentityModel.Tokens;
using NybSys.Common.Extension;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using static NybSys.Common.Enums.Enums;

namespace NybSys.Auth.BLL
{
    public class TokenBuilder
    {
        private List<Claim> claims = new List<Claim>();
        private string Issuer = string.Empty;
        private string Audience = string.Empty;
        private TimeSpan TokenExpiration = TimeSpan.FromDays(30);
        private bool UserId = false;

        private Guid Jti = Guid.Empty;

        private SigningCredentials signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ghg")), "SHA256");

        public TokenBuilder AddClaim(string claimType, string claimValue)
        {
            claims.Add(new Claim(claimType, claimValue));
            return this;
        }

        public TokenBuilder AddClaim(string claimType, params string[] claimValues)
        {
            claims.AddRange(claimValues.Select(p => new Claim(claimType, p)));
            return this;
        }

        public TokenBuilder AddClaim(string claimType, List<string> claimValues)
        {
            claims.AddRange(claimValues.Select(p => new Claim(claimType, p)));
            return this;
        }

        public TokenBuilder AddRoleClaim(List<string> claimValues)
        {
            claims.AddRange(claimValues.Select(p => new Claim(ClaimTypes.Role, p)));
            return this;
        }

        public TokenBuilder AddRoleClaim(params string[] claimValues)
        {
            claims.AddRange(claimValues.ToList().Select(p => new Claim(ClaimTypes.Role, p)));
            return this;
        }

        public TokenBuilder AddIssuer(string Issuer)
        {
            this.Issuer = Issuer;
            return this;
        }

        public TokenBuilder AddAudience(string Audience)
        {
            this.Audience = Audience;
            return this;
        }

        public TokenBuilder AddTokenCredential(SigningCredentials securityKey)
        {
            this.signingCredentials = securityKey;
            return this;
        }

        public TokenBuilder AddTokenCredential(SymmetricSecurityKey securityKey, TokenAlgorithm algorithmName)
        {
            this.signingCredentials = new SigningCredentials(securityKey, algorithmName.ToDescription());
            return this;
        }

        public TokenBuilder AddTokenCredential(string key, TokenAlgorithm algorithmName)
        {
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            this.signingCredentials = new SigningCredentials(securityKey, algorithmName.ToDescription());
            return this;
        }

        public TokenBuilder AddUniqueUser(string userId)
        {
            this.UserId = true;
            this.claims.Add(new Claim(JwtRegisteredClaimNames.UniqueName, userId));
            return this;
        }

        public TokenBuilder AddTokenExpiration(TimeSpan timeSpan)
        {
            this.TokenExpiration = timeSpan;
            return this;
        }

        public TokenBuilder AddJti(Guid jti)
        {
            Jti = jti;
            return this;
        }


        public string BuildToken()
        {
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Jti.ToString()));

            var token = new JwtSecurityToken(Issuer,
                  Audience,
                  claims,
                  expires: DateTime.Now.Add(TokenExpiration),
                  signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        
        private bool EnsureArguments()
        {
            if(!UserId)
            {
                throw new ArgumentNullException(nameof(UserId) + " is not provided");
            }

            if(Jti.Equals(Guid.Empty))
            {
                throw new ArgumentNullException(nameof(Jti) + " is not provided");
            }

            return true;
        }
    }
}
