using BuildingMyFirstAPIOnion.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using FluentValidation.Results;

namespace BuildingMyFirstAPIOnion.BL.Extensions
{
    public static class EntityOperationResultExtension
    {
        public static IEntityOperationResult<TDto> ToOperationResult<TDto>(this TDto dto)
        {
            return new EntityOperationResult<TDto>(dto);
        }

        public static IEntityOperationResult<TDto> ToOperationResult<TDto>(this ValidationResult validationResult)
        {
            return new EntityOperationResult<TDto>
            {
                Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList()
            };
        }
    }
}
