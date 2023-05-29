using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto_BDRC_SessioneEstiva_2022_2023.Application.Common.Interfaces.Persistance;

public interface IUnitOfWork
{
	IItineraryRepository Itineraries { get; }
	IUserRepository Users { get; }

}
