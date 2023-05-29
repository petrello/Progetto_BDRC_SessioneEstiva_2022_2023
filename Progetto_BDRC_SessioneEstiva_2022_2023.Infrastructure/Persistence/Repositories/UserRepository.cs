using Progetto_BDRC_SessioneEstiva_2022_2023.Application.Common.Interfaces.Persistance;
using Progetto_BDRC_SessioneEstiva_2022_2023.Domain.Entities;

namespace Progetto_BDRC_SessioneEstiva_2022_2023.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{

    // temp DB
    private static readonly List<User> users = new List<User>();

    public void Add(User user)
    {
        users.Add(user);
    }

	public Task<int> AddAsync(User entity)
	{
		throw new NotImplementedException();
	}

	public Task<int> DeleteAsync(int id)
	{
		throw new NotImplementedException();
	}

	public Task<IReadOnlyList<User>> GetAllAsync()
	{
		throw new NotImplementedException();
	}

	public Task<User> GetByIdAsync(int id)
	{
		throw new NotImplementedException();
	}

	public User? GetUserByEmail(string email)
    {
        return users.SingleOrDefault(u => u.Email == email);
    }

	public Task<int> UpdateAsync(User entity)
	{
		throw new NotImplementedException();
	}
}
