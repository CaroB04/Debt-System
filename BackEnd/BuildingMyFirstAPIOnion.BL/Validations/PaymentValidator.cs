using BuildingMyFirstAPIOnion.BL.DTO;
using BuildingMyFirstAPIOnion.Models.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingMyFirstAPIOnion.BL.Validations
{
    public class PaymentValidator : AbstractValidator<PaymentDto>
    {
        public PaymentValidator()
        {
            //RuleFor(payment => payment.Voucher)
            //    .NotEmpty()
            //    .NotNull()
            //    .WithMessage("El comprobante no puede estar vacío.");


            RuleFor(payment => payment.SetedDate)
                .NotNull()
                .Must(
                    setedDate => (Convert.ToDateTime(setedDate).Date.
                    CompareTo(DateTime.Now.Date)) > 0 || (Convert.ToDateTime(setedDate).Date.
                    CompareTo(DateTime.Now.Date)) == 0);
        }
    }
}
