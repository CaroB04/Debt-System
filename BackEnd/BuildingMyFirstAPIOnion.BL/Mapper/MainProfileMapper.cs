using AutoMapper;
using BuildingMyFirstAPIOnion.BL.DTO;
using BuildingMyFirstAPIOnion.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingMyFirstAPIOnion.BL.Mapper
{
    public class MainProfileMapper : Profile
    {
        public MainProfileMapper()
        {
            #region User
            CreateMap<UserEntity, UserDto>().ReverseMap();
            CreateMap<UserEntity, UserDtoGet>().ReverseMap();
            //.ForMember(dto => dto.Name, opt => opt.MapFrom(entity => $"{entity.Name} {entity.LastName}"))
            //.ReverseMap();
            #endregion

            #region Payment
            CreateMap<PaymentEntity, PaymentDto>().ReverseMap();
            #endregion

            #region Loan
            CreateMap<LoanEntity, LoanDtoGet>().ReverseMap()
                .ForMember(dest => dest.Term, opt => opt.MapFrom(src => src.Term.ToString()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ReverseMap();

            CreateMap<LoanEntity, LoanDto>().ReverseMap()
                .ForMember(dest => dest.Term, opt => opt.MapFrom(src => src.Term.ToString()))
                .ReverseMap();
            #endregion
        }


    }
}
