using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Polly;
using System.Security.Cryptography.X509Certificates;

var builder = WebApplication.CreateBuilder(args);
{
	builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
		.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
		.AddEnvironmentVariables();

	// var authenticationProviderKey = "keyformobileapp";
	// var key = builder.Configuration.GetValue<string>("ApiSettings:Secret");


    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
		//.AddJwtBearer(authenticationProviderKey, options =>
		.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
		{
			options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters()
			{
                ValidateLifetime = true,
                ValidateIssuer = true,
				ValidateAudience = true,
				ValidateIssuerSigningKey = true,
				ValidIssuer = "https://localhost:7009",
				ValidAudience = "bdrcApi",
				IssuerSigningKey = new X509SecurityKey(new X509Certificate2(Path.Combine(".", "keys", "AuthSample.cer")))
            };
		});

	builder.Services
		.AddOcelot(builder.Configuration)
		.AddCacheManager(opt =>
		{
			opt.WithDictionaryHandle();
		})
		.AddPolly();
}

var app = builder.Build();
{
    app.UseHttpsRedirection();

	await app.UseOcelot();

    app.Run();
}