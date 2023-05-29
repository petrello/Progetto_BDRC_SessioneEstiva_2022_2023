using Dapper;
using Microsoft.Extensions.Options;
using Progetto_BDRC_SessioneEstiva_2022_2023.Application.Common.Interfaces.Persistance;
using Progetto_BDRC_SessioneEstiva_2022_2023.Domain.Itinerary;
using Progetto_BDRC_SessioneEstiva_2022_2023.Infrastructure.Persistence.Configurations;
using Progetto_BDRC_SessioneEstiva_2022_2023.Infrastructure.Persistence.StoredProcedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto_BDRC_SessioneEstiva_2022_2023.Infrastructure.Persistence.Repositories
{
    public class ItineraryRepository : IItineraryRepository
    {
        private readonly DapperDbContext dbContext;
		private readonly ItineraryStoredProcedures itineraryStoredProcedures;

		public ItineraryRepository(DapperDbContext dbContext, IOptions<ItineraryStoredProcedures> itineraryStoredProcedures)
		{
			this.dbContext = dbContext;
			this.itineraryStoredProcedures = itineraryStoredProcedures.Value;
		}

		public async Task<Itinerary> GetItineraryById(string id)
		{
			var parameters = new DynamicParameters();
			parameters.Add("Id", id, System.Data.DbType.String, System.Data.ParameterDirection.Input);

			using var connection = dbContext.CreateConnection();
			
			var itinerary = await connection
				.QueryFirstOrDefaultAsync<Itinerary>(
					sql: itineraryStoredProcedures.GetItineraryById, 
					param: parameters, 
					commandType: System.Data.CommandType.StoredProcedure);

			return itinerary;
		}

		public async Task Add(Itinerary itinerary)
		{
			var query = "q";

			using (var connection = dbContext.CreateConnection())
			{
				await connection.ExecuteAsync(query);
			}
		}

		public Task<int> AddAsync(Itinerary itinerary)
		{
			throw new NotImplementedException();
		}

		public Task<int> DeleteAsync(int id)
		{
			throw new NotImplementedException();
		}

		public Task<IReadOnlyList<Itinerary>> GetAllAsync()
		{
			throw new NotImplementedException();
		}

		public Task<Itinerary> GetByIdAsync(int id)
		{
			throw new NotImplementedException();
		}

		public Task<int> UpdateAsync(Itinerary entity)
		{
			throw new NotImplementedException();
		}
	}
}
