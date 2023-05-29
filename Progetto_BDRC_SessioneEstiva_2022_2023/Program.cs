using Progetto_BDRC_SessioneEstiva_2022_2023.Api;
using Progetto_BDRC_SessioneEstiva_2022_2023.Api.Common.Middlewares;
using Progetto_BDRC_SessioneEstiva_2022_2023.Application;
using Progetto_BDRC_SessioneEstiva_2022_2023.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddSingleton<ItineraryTestRepo>();

	builder.Services
		.AddPresentation()
		.AddApplication()
		.AddInfrastructure(builder.Configuration);

	
}

var app = builder.Build();
{
	if (app.Environment.IsDevelopment())
	{
		app.UseSwagger();
		app.UseSwaggerUI();

		// app.UseExceptionHandler("/error-development");
	}
	//else
	//{
	//	app.UseExceptionHandler("/error");
	//}

	// app.UseHttpTokenMiddleware();
	
	app.UseHttpsRedirection();

	app.UseAuthentication();

	app.UseAuthorization();

	app.MapControllers();

	app.Run();
}


public class ItineraryTestRepo
{
    private readonly List<ItineraryTest> itineraries
        = new()
        {
            new ItineraryTest
            {
                Id = Guid.Parse("05b2713b-94d0-49cd-aa00-ae8569c956b5"),
                Name = "Civitanova",
                Description = "Passeggiata lungo mare"
            },
            new ItineraryTest
            {
                Id = Guid.Parse("23a1713b-94d0-49cd-aa00-ae8569c956b5"),
                Name = "Ancona",
                Description = "Se magnane i moscioli e beve il vì"
            },
            new ItineraryTest
            {
                Id = Guid.Parse("41d9713b-94d0-49cd-aa00-ae8569c956b5"),
                Name = "Edinburgo",
                Description = "Viaggio di cinque giorni per la scozia"
            }
        };
    public List<ItineraryTest> Itineraries => itineraries;
}
public class ItineraryTest
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}