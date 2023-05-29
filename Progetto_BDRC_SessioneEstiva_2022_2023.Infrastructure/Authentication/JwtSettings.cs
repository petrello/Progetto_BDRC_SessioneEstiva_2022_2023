using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto_BDRC_SessioneEstiva_2022_2023.Infrastructure.Authentication
{
	public class JwtSettings
	{
		public const string SectionName = "JwtSettings";
		public string Secret { get; set; } = null!;
		public int ExpiryMinutes { get; set; }
		public string Issuer { get; set; } = null!;
		public string Audience { get; set; } = null!;
	}
}
