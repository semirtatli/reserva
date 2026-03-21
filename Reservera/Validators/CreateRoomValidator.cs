using FluentValidation;
using Reservera.Models;

namespace Reservera.Validators;

public class CreateRoomValidator : AbstractValidator<Room>
{
    public CreateRoomValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty().WithMessage("Oda adı boş olamaz.")
            .MaximumLength(100).WithMessage("Oda adı en fazla 100 karakter olabilir.");

        RuleFor(r => r.PricePerNight)
            .GreaterThan(0).WithMessage("Gecelik fiyat 0'dan büyük olmalıdır.");

        RuleFor(r => r.Capacity)
            .GreaterThan(0).WithMessage("Kapasite 0'dan büyük olmalıdır.");
    }
}
