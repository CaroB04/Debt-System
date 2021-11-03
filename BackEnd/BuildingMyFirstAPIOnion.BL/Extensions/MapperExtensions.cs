using AutoMapper;
using BuildingMyFirstAPIOnion.BL.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BuildingMyFirstAPIOnion.BL.Extensions
{
    public static class MapperExtensions
    {
        public static Profile CreateMap_WithConventions_FromAssemblies<T, TDto>(this Profile profile, bool reverseMap = true) where T : class where TDto : class, IBaseDTO
        {
            Type source = typeof(T);
            Type deestination = typeof(TDto);

            var source_types = GetTypes(source);

            foreach (var item in source_types)
            {
                var types = GetTypes(source);

                var dto = types.FirstOrDefault(t => t.Name.ToLower() == $"{item.Name}Dto".ToLower());

                if (dto is null)
                {
                    continue;
                }

                var result = profile.CreateMap(item, dto);

                if (reverseMap)
                {
                    result = profile.CreateMap(dto, item);

                    foreach (var property in dto.GetProperties())
                    {
                        PropertyDescriptor descriptor = TypeDescriptor.GetProperties(dto)[property.Name];
                        ReadOnlyAttribute attribute = (ReadOnlyAttribute)descriptor.Attributes[typeof(ReadOnlyAttribute)];
                        if (attribute.IsReadOnly == true)
                            result.ForMember(property.Name, opt => opt.Ignore());

                    }
                }
            }

            return profile;
        }

        private static IEnumerable<Type> GetTypes(Type type)
        {
            Assembly asm = Assembly.GetAssembly(type);
            var types = asm.GetTypes().Where(t => t.Namespace == type.Namespace);
            return types;
            
        }
    }
}
 