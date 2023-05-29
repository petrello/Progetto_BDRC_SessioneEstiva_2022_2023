using Duende.IdentityServer;
using Duende.IdentityServer.AspNetIdentity;
using EmailServiceProvider;
using IdentityServer;
using IdentityServer.CustomTokenProviders;
using IdentityServer.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

var seed = args.Contains("/seed");
if (seed)
{
    args = args.Except(new[] { "/seed" }).ToArray();
}

var builder = WebApplication.CreateBuilder(args);
{
    var migrationsAssembly = typeof(Program).Assembly.GetName().Name;
	var connectionString = builder.Configuration.GetConnectionString("ConnectionString");

    builder.Services.AddAutoMapper(typeof(Program));

    if (seed)
    {
        SeedData.EnsureSeedData(connectionString);
    }

    builder.Services.AddDbContext<AspNetIdentityDbContext>(options =>
    {
        options.UseSqlServer(connectionString, 
            b => b.MigrationsAssembly(migrationsAssembly));
    });

    builder.Services
        .AddIdentity<IdentityUser, IdentityRole>(opt =>
        {
            opt.Password.RequiredLength = 7;
            opt.Password.RequireDigit = false;
            opt.Password.RequireUppercase = false;

            opt.User.RequireUniqueEmail = true;

            opt.SignIn.RequireConfirmedEmail = true;

            opt.Tokens.EmailConfirmationTokenProvider = "emailconfirmation";

            opt.Lockout.AllowedForNewUsers = true;
            opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
            opt.Lockout.MaxFailedAccessAttempts = 5;
        })
        .AddEntityFrameworkStores<AspNetIdentityDbContext>()
        .AddTokenProvider<EmailConfirmationTokenProvider<IdentityUser>>("emailconfirmation")
        .AddDefaultTokenProviders();


    builder.Services.Configure<EmailConfirmationTokenProviderOptions>(opt =>
        opt.TokenLifespan = TimeSpan.FromHours(1));

    var emailConfig = builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfig>();
    builder.Services.AddSingleton(emailConfig);
    builder.Services.AddScoped<IEmailSender, EmailSender>();


    var certLocation = builder.Configuration["CertConfig:Location"];
    var certPassword = builder.Configuration["CertConfig:Password"];

    
    builder.Services.AddIdentityServer()
        .AddAspNetIdentity<IdentityUser>()
        .AddProfileService<ProfileService>()
        .AddConfigurationStore(options =>
        {
            options.ConfigureDbContext = b =>
                b.UseSqlServer(connectionString!,
                    sql => sql.MigrationsAssembly(migrationsAssembly));
        })
        .AddOperationalStore(options =>
        {
            options.ConfigureDbContext = b =>
                b.UseSqlServer(connectionString!,
                    sql => sql.MigrationsAssembly(migrationsAssembly));

            // this enables automatic token cleanup. this is optional.
            options.EnableTokenCleanup = true;
            options.TokenCleanupInterval = 3600; // interval in seconds (default is 3600)
        })
        .AddSigningCredential(new X509Certificate2(certLocation, certPassword));

    builder.Services.AddAuthentication()
        .AddGoogle(googleOptions =>
        {
            googleOptions.ClientId = builder.Configuration["ExternalAuth:Google:ClientId"];
            googleOptions.ClientSecret = builder.Configuration["ExternalAuth:Google:ClientSecret"];
            googleOptions.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
        });

    builder.Services.AddControllersWithViews();
}

var app = builder.Build();
{

	if (app.Environment.IsDevelopment())
	{
		app.UseDeveloperExceptionPage();
	}

    app.UseHttpsRedirection();

    app.UseStaticFiles();
	app.UseRouting();

	app.UseIdentityServer();

	app.UseAuthorization();
	app.UseEndpoints(endpoints =>
	{
		endpoints.MapDefaultControllerRoute();
	});

	app.Run();
}