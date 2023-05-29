using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto_BDRC_SessioneEstiva_2022_2023.Application.Common.Interfaces.Persistance
{
	public interface IGenericRepository<T> where T : class
	{
		// Generic class for defining basic CRUD operations
		Task<T> GetByIdAsync(int id);
		Task<IReadOnlyList<T>> GetAllAsync();
		Task<int> AddAsync(T entity);
		Task<int> UpdateAsync(T entity);
		Task<int> DeleteAsync(int id);
	}
}
