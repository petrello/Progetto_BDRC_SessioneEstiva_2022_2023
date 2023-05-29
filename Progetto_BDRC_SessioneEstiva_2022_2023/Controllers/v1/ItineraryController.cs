using MapsterMapper;
using MediatR;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Progetto_BDRC_SessioneEstiva_2022_2023.Contracts.Itineraries;
using Progetto_BDRC_SessioneEstiva_2022_2023.Application.Itineraries.Commands;
using Progetto_BDRC_SessioneEstiva_2022_2023.Application.Common.Interfaces.Persistance;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Mapster.Models;
using Microsoft.Net.Http.Headers;
using Progetto_BDRC_SessioneEstiva_2022_2023.Domain.Itinerary.ValueObjects;
using Microsoft.AspNetCore.Authorization;

namespace Progetto_BDRC_SessioneEstiva_2022_2023.Api.Controllers.v1;

[Route("api/itineraries")]
public class ItineraryController : ApiController
{
	private readonly IMapper mapper;
	private readonly ISender mediator;
	private readonly ItineraryTestRepo itineraryTestRepo;

	public ItineraryController(IMapper mapper, ISender mediator, ItineraryTestRepo itineraryTestRepo)
	{
		this.mapper = mapper;
		this.mediator = mediator;
		this.itineraryTestRepo = itineraryTestRepo;

    }

	//[HttpGet("{id}")]
	//public IActionResult TestGw(int id)
	//{
	//	return Ok(id);
	//}
    
	[HttpGet]
    public IActionResult GetAllItineraries()
    {
        //var token = Request.Headers[HeaderNames.Authorization];

        return Ok(itineraryTestRepo.Itineraries);
    }

    [HttpGet("{id}")]
    public IActionResult GetItineraryById(Guid Id)
    {
		//var token = Request.Headers[HeaderNames.Authorization];
		var res = itineraryTestRepo.Itineraries.Where(x => x.Id == Id).FirstOrDefault();
		
        if(res is null)
			return NotFound();
        
        return Ok(res);
    }

    [HttpPut]
    [Authorize(Policy = "AdminCRUDPolicy")]
    public IActionResult EditItineraryById([FromBody] ItineraryTest itinerary)
	{
		//var token = Request.Headers[HeaderNames.Authorization];
		var index = itineraryTestRepo.Itineraries.FindIndex(x => x.Id == itinerary.Id);
        
		if(index < 0)
			return NotFound();

        itineraryTestRepo.Itineraries[index] = itinerary;

        return Ok();
    }

    [HttpPost()]
    [Authorize(Policy = "AdminCRUDPolicy")]
    public IActionResult CreateNewItinerary([FromBody] ItineraryTest itinerary)
    {
        itineraryTestRepo.Itineraries.Add(itinerary);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminCRUDPolicy")]
    public IActionResult DeleteItineraryById(Guid Id)
    {
        //var token = Request.Headers[HeaderNames.Authorization];
        var index = itineraryTestRepo.Itineraries.FindIndex(x => x.Id == Id);

        if (index < 0)
            return NotFound();

        itineraryTestRepo.Itineraries.RemoveAt(index);

        return Ok();
    }

 //   [HttpPost]
 //   public async Task<IActionResult> CreateItinerary(CreateItineraryRequest request, string hostId)
	//{
	//	var command = mapper.Map<CreateItineryCommand>((request, hostId));
	//	var createItineraryResult = await mediator.Send(command);


	//	return createItineraryResult.Match(
	//		itinerary => Ok(mapper.Map<ItineraryResponse>(itinerary)),
	//		errors => Problem(errors));
	//}
}
