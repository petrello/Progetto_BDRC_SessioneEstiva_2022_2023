using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Progetto_BDRC_SessioneEstiva_2022_2023.Application.Common.Interfaces.Authentication;
using Progetto_BDRC_SessioneEstiva_2022_2023.Application.Common.Services;
using Progetto_BDRC_SessioneEstiva_2022_2023.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Progetto_BDRC_SessioneEstiva_2022_2023.Infrastructure.Authentication;
public class JwtTokenGenerator : IJwtTokenGenerator
{
	private readonly IDateTimeProvider dateTimeProvider;
	private readonly JwtSettings jwtSettings;

	public JwtTokenGenerator(IDateTimeProvider dateTimeProvider, IOptions<JwtSettings> jwtOptions)
	{
		this.dateTimeProvider = dateTimeProvider;
		this.jwtSettings = jwtOptions.Value;
	}

	public string GenerateToken(User user)
	{
		var signinCredentials = new SigningCredentials(
				new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
				SecurityAlgorithms.HmacSha256
			);

		var claims = new[]
		{
			new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
			new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
			new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
			// new Claim(ClaimTypes.Role, user.Role),
			new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
		};

		var securityToken = new JwtSecurityToken(
				issuer: jwtSettings.Issuer,
				audience: jwtSettings.Audience,
				expires: dateTimeProvider.UtcNow.AddMinutes(jwtSettings.ExpiryMinutes),
				claims: claims,
				signingCredentials: signinCredentials
			);

		return new JwtSecurityTokenHandler().WriteToken(securityToken);
	}
}
