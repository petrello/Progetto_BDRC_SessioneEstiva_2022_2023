using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto_BDRC_SessioneEstiva_2022_2023.Application.Itineraries.Commands;

public class CreateItineraryCommandValidator : AbstractValidator<CreateItineryCommand>
{
	public CreateItineraryCommandValidator()
	{
		RuleFor(x => x.Name).NotEmpty();
		RuleFor(x => x.Description).NotEmpty();
		RuleFor(x => x.Sections).NotEmpty();
	}
}
