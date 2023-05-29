using ErrorOr;

namespace Progetto_BDRC_SessioneEstiva_2022_2023.Domain.Common.Errors;

public static partial class Errors
{
	public static class User
	{
		public static Error DuplicateEmail => Error.Conflict(code: "User.DuplicateEmail", description: "Email is already in use.");
	}
}
