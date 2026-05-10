using FluentValidation;
using TripBookingAPI.DTOs;

public class TripValidator : AbstractValidator<TripDTO>
{
    public TripValidator()
    {
        RuleFor(x => x.Destination).NotEmpty().WithMessage("Destination is required!");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be more than zero!");
    }
}
