namespace Progetto_BDRC_SessioneEstiva_2022_2023.Contracts.Authentication;
public record AuthenticationResponse 
(
	Guid Id,
	string FirstName,
	string LastName,
	string Email,
	string Token
);