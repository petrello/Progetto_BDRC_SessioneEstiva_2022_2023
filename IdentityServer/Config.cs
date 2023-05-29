using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Test;
using IdentityModel;
using System.Security.Claims;
using static System.Net.Mime.MediaTypeNames;

namespace IdentityServer;

public static class Config
{
	public static IEnumerable<Client> Clients => 
		new Client[]
		{
            new Client
            {
                ClientId = "your-client-id",
                ClientName = "BDRC Client ASP.NET MVC",
                AllowedGrantTypes = GrantTypes.Hybrid,
                ClientSecrets =
                {
                    new Secret("your-client-secret".Sha256())
                },
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "bdrcApi",
                    "position",
                    "country",
                },
                RedirectUris =
                {
                    "https://localhost:7235/signin-oidc"
                },
                PostLogoutRedirectUris =
                {
                    "https://localhost:7235/signout-callback-oidc"
                },
                RequireClientSecret = true,
                RequirePkce = false
            },
        };

	public static IEnumerable<ApiScope> ApiScopes =>
		new ApiScope[]
		{
			new ApiScope(name: "bdrcApi", displayName: "Progetto BDRC API")
		};
	
	public static IEnumerable<ApiResource> ApiResources =>
		new ApiResource[]
		{
			new ApiResource("bdrcApi", "Progetto BDRC API")
			{
				Scopes = { "bdrcApi" }
			},
		};
	
	public static IEnumerable<IdentityResource> IdentityResources => 
		new IdentityResource[]
		{
			new IdentityResources.OpenId(),
			new IdentityResources.Profile(),
			// Custom Identity Resource. Es: IdentityResource named “verification” which would include the email and email_verified claims.
			new IdentityResource()
			{
				Name = "verification",
				UserClaims = new []
				{
					JwtClaimTypes.Email,
					JwtClaimTypes.EmailVerified
				}
			},
			new IdentityResource()
			{
				Name = "roles",
				DisplayName = "User role(s)",
				UserClaims = new []
				{
					"role"
				}
			},
			new IdentityResource("position", "Your position", new List<string> { "position" }),
			new IdentityResource("country", "Your country", new List<string> { "country" })
		};
}
