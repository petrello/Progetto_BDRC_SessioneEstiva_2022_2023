using Progetto_BDRC_SessioneEstiva_2022_2023.Domain.Entities;

namespace Progetto_BDRC_SessioneEstiva_2022_2023.Application.Common.Interfaces.Persistance;
public interface IUserRepository : IGenericRepository<User>
{
    User? GetUserByEmail(string email);
    void Add(User user);
}