using Microsoft.EntityFrameworkCore;
using Progetto_BDRC_SessioneEstiva_2022_2023.Application.Common.Interfaces.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto_BDRC_SessioneEstiva_2022_2023.Infrastructure.Persistence.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
		public UnitOfWork(
			IItineraryRepository itineraryRepository,
			IUserRepository userRepository)
		{
			Itineraries = itineraryRepository;
			Users = userRepository;
		}

		public IItineraryRepository Itineraries { get; }

		public IUserRepository Users { get; }
	}
}
