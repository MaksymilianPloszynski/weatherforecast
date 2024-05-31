using FluentValidation;
using Weather.Forecast.API.Requests;

namespace Weather.Forecast.API.Validators;

public class GetAllLocationsRequestValidator: AbstractValidator<GetAllLocationsRequest>
{
    public GetAllLocationsRequestValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThan(0)
            .WithMessage("Page number must be greater than zero.");

        RuleFor(x => x.PageSize)
            .GreaterThan(0)
            .WithMessage("Page size must be greater than zero.");
    }
}