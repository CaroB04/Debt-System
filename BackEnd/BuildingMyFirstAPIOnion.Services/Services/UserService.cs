using AutoMapper;
using BuildingMyFirstAPIOnion.BL.DTO;
using BuildingMyFirstAPIOnion.BL.Extensions;
using BuildingMyFirstAPIOnion.Core.Abstract;
using BuildingMyFirstAPIOnion.Models.Contexts;
using BuildingMyFirstAPIOnion.Models.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace BuildingMyFirstAPIOnion.Services.Services
{
    public interface IUserService : IBaseService<UserEntity, UserDto>
    {
        //UserDtoGet GetUser(int id);
    
    }
    public class UserService : BaseService<UserEntity, UserDto>, IUserService
    {
      
        public UserService(BaseContext context, IMapper mapper, IValidator<UserDto> validator) 
            : base(context, mapper, validator)
        {
           
        }

        public async override Task<IEntityOperationResult<UserDto>> Create(UserDto dto)
        {
            var validationResult = _validator.Validate(dto);
            if (validationResult.IsValid is false)
                return validationResult.ToOperationResult<UserDto>();

            var entity = _mapper.Map<UserEntity>(dto);

            entity.RegistrationDate = Convert.ToDateTime(DateTime.Now.ToString("dd-MM-yyyy"));

            _dbSet.Add(entity);

            await _context.SaveChangesAsync();

            _mapper.Map(entity, dto);

            var result = dto.ToOperationResult();

            return result;
        }

        //public UserDtoGet GetUser(int id)
        //{
        //    var user = GetById(id);
        //    var loansTaken = loanService.Query().Where(loan => loan.DebtorId == id).ToList();
        //    var loansGiven = loanService.Query().Where(loan => loan.LenderId == id).ToList();

        //    UserDtoGet newUser = new UserDtoGet();
        //    newUser.Address = user.Address;
        //    newUser.Deleted = user.Deleted;
        //    newUser.Email = user.Email;
        //    newUser.Id = user.Id;
        //    newUser.IdentityCard = user.IdentityCard;
        //    newUser.LoansTaken = (IEnumerable<LoanDto>)loansTaken;
        //    newUser.LoansGiven= (IEnumerable<LoanDto>)loansGiven;

        //    return newUser;
        //}
    }
}
