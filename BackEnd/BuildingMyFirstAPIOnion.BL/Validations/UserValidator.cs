using BuildingMyFirstAPIOnion.BL.DTO;
using BuildingMyFirstAPIOnion.Models.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingMyFirstAPIOnion.BL.Validations
{
    public class UserValidator : AbstractValidator<UserDto>
    {
        public UserValidator()
        {
            RuleFor(user => user.IdentityCard)
                .NotEmpty()
                .NotNull()
                .Length(11)
                .WithMessage("Cédula no válida");

            RuleFor(user => user.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("El nombre no puede estar vacio");

            RuleFor(user => user.PassWord)
                .NotEmpty()
                .NotNull()
                .MinimumLength(6)
                .WithMessage("Contraseña inválida");

            RuleFor(user => user.PhoneNumber)
                .NotEmpty()
                .NotNull()
                .Length(10)
                .WithMessage("Número de no válido");

            RuleFor(user => user.Email)
                .NotEmpty()
                .NotNull()
                .EmailAddress()
                .WithMessage("Correo no válido");

            RuleFor(user => user.Address)
                .NotEmpty()
                .NotNull()
                .WithMessage("La dirección no puede estar vacía.");

        }
    }
}
