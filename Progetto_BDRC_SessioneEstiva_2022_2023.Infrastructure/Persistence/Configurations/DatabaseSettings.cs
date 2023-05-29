using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto_BDRC_SessioneEstiva_2022_2023.Infrastructure.Persistence.Configurations;

public class DatabaseSettings
{
	public const string SectionName = "DbSettings";
	public string ConnectionString { get; set; } = null!;
}
