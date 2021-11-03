using BuildingMyFirstAPIOnion.BL.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingMyFirstAPIOnion.BL.Validations
{
    class LoanGetValidator : AbstractValidator<LoanDtoGet>
    {
        public LoanGetValidator()
        {
            {
                RuleFor(loan => loan.Amount)
                    .Must(amount => amount > 0)
                    .NotNull()
                    .WithMessage("El monto no puede ser igual o menor que cero.");

                RuleFor(loan => loan.StartDate)
                    .NotNull()
                    .Must(
                        startDate => (Convert.ToDateTime(startDate).Date.
                        CompareTo(DateTime.Now.Date)) > 0 || (Convert.ToDateTime(startDate).Date.
                        CompareTo(DateTime.Now.Date)) == 0);

                RuleFor(loan => loan.AmountPayments)
                    .NotNull()
                    .Must(AmountPayments => AmountPayments > 0)
                    .WithMessage("Cantidad de pagos no puede ser igual o menor que cero.");

                RuleFor(loan => loan.Term)
                    .NotEmpty()
                    .NotNull()
                    .WithMessage("El plazo no puede estar vacío.");
            }
        }
    }
}
