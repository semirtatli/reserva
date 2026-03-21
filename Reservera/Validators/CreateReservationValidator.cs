using FluentValidation;
using Reservera.DTOs;

namespace Reservera.Validators;

public class CreateReservationValidator : AbstractValidator<CreateReservationRequest>
{
    public CreateReservationValidator()
    {
        RuleFor(r => r.GuestName)
            .NotEmpty().WithMessage("Misafir adı boş olamaz.")
            .MaximumLength(100).WithMessage("Misafir adı en fazla 100 karakter olabilir.");

        RuleFor(r => r.RoomId)
            .GreaterThan(0).WithMessage("Geçerli bir oda seçilmelidir.");

        RuleFor(r => r.CheckIn)
            .NotEmpty().WithMessage("Giriş tarihi boş olamaz.");

        RuleFor(r => r.CheckOut)
            .NotEmpty().WithMessage("Çıkış tarihi boş olamaz.")
            .GreaterThan(r => r.CheckIn).WithMessage("Çıkış tarihi giriş tarihinden sonra olmalıdır.");
    }
}
