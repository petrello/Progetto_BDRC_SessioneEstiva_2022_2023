using Progetto_BDRC_SessioneEstiva_2022_2023.Application.Common.Services;

namespace Progetto_BDRC_SessioneEstiva_2022_2023.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
	public DateTime UtcNow => DateTime.UtcNow;
}