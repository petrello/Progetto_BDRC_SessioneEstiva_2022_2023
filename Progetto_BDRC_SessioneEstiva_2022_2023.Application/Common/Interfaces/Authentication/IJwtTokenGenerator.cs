using Progetto_BDRC_SessioneEstiva_2022_2023.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto_BDRC_SessioneEstiva_2022_2023.Application.Common.Interfaces.Authentication
{
	public interface IJwtTokenGenerator
	{
		string GenerateToken(User user);
	}
}
