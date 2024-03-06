using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RealTimeUpdater.ApplicationCore.Services.Interfaces;
using RealTimeUpdater.Models.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RealTimeUpdater.ApplicationCore.Services.Services
{
	public class TokenService : ITokenService
	{
		private readonly SymmetricSecurityKey _symmetricKey;
		public TokenService(IConfiguration config, UserManager<ApplicationUser> userManager)
		{

			_symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["SecurityKey"]));

		}
		public async Task<string> CreateToken(ApplicationUser user)
		{
			List<Claim> claims = new()
			{
				new Claim(JwtRegisteredClaimNames.NameId,user.Id.ToString()),
				new Claim(JwtRegisteredClaimNames.UniqueName,user.Email)
			};

			var creds = new SigningCredentials(_symmetricKey, SecurityAlgorithms.HmacSha256Signature);
			var tokenDescriptor = new SecurityTokenDescriptor()
			{
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.Now.AddDays(7),
				SigningCredentials = creds
			};

			var tokenHandler = new JwtSecurityTokenHandler();

			var securityToken = tokenHandler.CreateToken(tokenDescriptor);

			return tokenHandler.WriteToken(securityToken); ;
		}

	}
}
