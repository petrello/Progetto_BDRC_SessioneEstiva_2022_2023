using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Progetto_BDRC_SessioneEstiva_2022_2023.Client;
using Progetto_BDRC_SessioneEstiva_2022_2023.Client.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography.X509Certificates;

var builder = WebApplication.CreateBuilder(args);
{
    JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

    builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    builder.Services.AddScoped<IItineraryHttpClient, ItineraryHttpClient>();

    builder.Services.AddAuthentication(opt =>
    {
        opt.DefaultScheme = "Cookies";
        opt.DefaultChallengeScheme = "oidc";
    })
    .AddCookie("Cookies", opt =>
    {
        opt.AccessDeniedPath = "/Account/AccessDenied";
    })
    .AddOpenIdConnect("oidc", opt =>
    {
        opt.SignInScheme = "Cookies";
        opt.Authority = "https://localhost:7009";
        opt.ClientId = builder.Configuration["OidcConfig:ClientId"];
        opt.ResponseType = "code id_token";
        opt.SaveTokens = true;
        opt.ClientSecret = builder.Configuration["OidcConfig:ClientSecret"];
        opt.GetClaimsFromUserInfoEndpoint = true;

        opt.Scope.Add("bdrcApi");
        //opt.Scope.Add("roles");
        opt.Scope.Add("position");
        opt.Scope.Add("country");
        //opt.ClaimActions.MapUniqueJsonKey("role", "role");
        opt.ClaimActions.MapUniqueJsonKey("position", "position");
        opt.ClaimActions.MapUniqueJsonKey("country", "country");

        opt.TokenValidationParameters = new TokenValidationParameters
        {
            RoleClaimType = "position",
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new X509SecurityKey(
                new X509Certificate2(Path.Combine(".", "keys", "AuthSample.cer")))
        };
    });

    builder.Services.AddAuthorization(authOpt =>
    {
        authOpt.AddPolicy("AdminCRUDPolicy", policyBuilder =>
        {
            policyBuilder.RequireAuthenticatedUser();
            policyBuilder.RequireClaim("position", "Administrator");
            policyBuilder.RequireClaim("country", "ITA");
        });
    });

    // Add services to the container.
    builder.Services.AddControllersWithViews();
}



var app = builder.Build();
{

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
	    app.UseExceptionHandler("/Home/Error");
	    app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllerRoute(
	    name: "default",
	    pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();

}