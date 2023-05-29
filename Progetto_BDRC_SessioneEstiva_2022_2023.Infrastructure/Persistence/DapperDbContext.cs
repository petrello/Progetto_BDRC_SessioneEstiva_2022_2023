using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Progetto_BDRC_SessioneEstiva_2022_2023.Infrastructure.Persistence.Configurations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto_BDRC_SessioneEstiva_2022_2023.Infrastructure
{
	public class DapperDbContext
	{
		private readonly DatabaseSettings dbSettings;
		private readonly string connectionString;

		public DapperDbContext(IOptions<DatabaseSettings> dbSettings)
		{
			this.dbSettings = dbSettings.Value;
			this.connectionString = this.dbSettings.ConnectionString;
		}

		public IDbConnection CreateConnection() => new SqlConnection(connectionString);

	}
}
