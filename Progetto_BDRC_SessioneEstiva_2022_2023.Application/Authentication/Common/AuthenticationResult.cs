using Progetto_BDRC_SessioneEstiva_2022_2023.Domain.Entities;

namespace Progetto_BDRC_SessioneEstiva_2022_2023.Application.Authentication.Common;

public record AuthenticationResult(
    User User,
    string Token
);
