using BuildingMyFirstAPIOnion.BL.Validations;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingMyFirstAPIOnion.BL.Config
{
    public static class FluentValidationConfig
    {
        public static IMvcBuilder ConfigFluentValidation(this IMvcBuilder mvcBuilder)
        {
            mvcBuilder.AddFluentValidation(x =>
            {
                x.AutomaticValidationEnabled = false;
                x.RegisterValidatorsFromAssemblyContaining<UserValidator>();
                x.RegisterValidatorsFromAssemblyContaining<LoanValidator>();
                x.RegisterValidatorsFromAssemblyContaining<PaymentValidator>();
                x.RegisterValidatorsFromAssemblyContaining<LoanGetValidator>();
            });

            return mvcBuilder;

        }
    }
}
