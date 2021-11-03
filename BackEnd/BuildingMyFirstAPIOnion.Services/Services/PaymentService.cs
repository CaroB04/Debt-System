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
    public interface IPaymentService : IBaseService<PaymentEntity, PaymentDto>
    {
        IQueryable GetPaymentByLoan(int id);
    }
    public class PaymentService : BaseService<PaymentEntity, PaymentDto>, IPaymentService
    {
        private readonly LoanPaymentService loanPaymentService;
   
        //private readonly ILoanService LoanService;
        public PaymentService(BaseContext context, IMapper mapper, IValidator<PaymentDto> validator) : base(context, mapper, validator)
        {
            loanPaymentService = new LoanPaymentService(context);
        

            //LoanService = new LoanService(context, mapper, validatorLoan,validator, validatorUser);
        }

        public override async Task<IEntityOperationResult<PaymentDto>> Create(PaymentDto dto)
        {
            var validationResult = _validator.Validate(dto);
            if (validationResult.IsValid is false)
                return validationResult.ToOperationResult<PaymentDto>();

            var entity = _mapper.Map<PaymentEntity>(dto);

            _dbSet.Add(entity);

            await _context.SaveChangesAsync();

            _mapper.Map(entity, dto);

            loanPaymentService.Create(dto.Id, dto.LoanEntityId);
            

            var result = dto.ToOperationResult();

            return result;
        }

        public IQueryable GetPaymentByLoan(int id)
        {
            var payments = from loan in _context.Set<LoanPayment>()
                           join payment in _context.Set<PaymentEntity>()
                           on loan.PaymentEntityId equals payment.Id
                           where loan.LoanEntityId == id
                           select new {loan.LoanEntityId, payment.Id, payment.Amount, payment.Voucher, payment.DateRealization, payment.SetedDate, payment.Done };
            
            return payments;
        }

        //public bool IsLoanPaidOff(int id)
        //{
        //    bool paidOff = false;

        //    var loads = GetPaymentByLoan(id);
            
        //    foreach (var load in loads)
        //    {
        //        if (!load.Done) return false;

        //        paidOff = true;
        //    }
        //    return paidOff;
        //}

        public override async Task<IEntityOperationResult<PaymentDto>> Update(PaymentDto dto)
        {
            var validationResult = _validator.Validate(dto);
            if (validationResult.IsValid is false)
                return validationResult.ToOperationResult<PaymentDto>();

            var entityExist = Query().Any(x => x.Id == dto.Id);

            if (entityExist is false) return null;

            //if (dto.Done)
            //{
            //    await intermediateService.FinishLoan(dto);
            //}

            var entity = _mapper.Map<PaymentEntity>(dto);

            _dbSet.Update(entity);

            await _context.SaveChangesAsync();

            _mapper.Map(entity, dto);

            var result = dto.ToOperationResult();

            return result;

        }

        //private async Task FinishLoan(PaymentDto dto)
        //{
        //    int loanId = GetLoanId(dto);

        //    dto.DateRealization = Convert.ToDateTime(DateTime.Now.ToString("dd-MM-yyyy"));

        //    if (IsLoanPaidOff(loanId))
        //    {
        //        LoanDto loan = LoanService.GetById(loanId);
        //        loan.Status = "Finalized";
        //        await LoanService.Update(loan);
        //    }
        //}

        //private int GetLoanId(PaymentDto dto)
        //{
        //    var loanEntity = from loan in _context.Set<LoanPayment>()
        //                     join payment in _context.Set<PaymentEntity>()
        //                     on loan.PaymentEntityId equals payment.Id
        //                     where loan.LoanEntityId == dto.Id
        //                     select new { loan.LoanEntityId };

        //    List<PaymentGetDto> loans = (List<PaymentGetDto>)loanEntity;
        //    int loanId = loans[0].Id;
        //    return loanId;
        //}
    } 
}
