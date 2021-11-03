using AutoMapper;
using BuildingMyFirstAPIOnion.BL.DTO;
using BuildingMyFirstAPIOnion.Models.Contexts;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using BuildingMyFirstAPIOnion.Models.Entities;
using System.Threading.Tasks;

namespace BuildingMyFirstAPIOnion.Services.Services
{
    public class IntermediateService
    {
        ILoanService LoanService;
        IPaymentService PaymentService;
        BaseContext _context;

        public IntermediateService(BaseContext context, IMapper mapper, IValidator<PaymentDto>validatorPayment, IValidator<LoanDto> validatorLoan, IValidator<UserDto> validatorUser)
        {
            _context = context;
            PaymentService = new PaymentService(context, mapper, validatorPayment);
            LoanService = new LoanService(context, mapper,validatorLoan, validatorPayment,validatorUser );
        }
        
        public bool IsLoanPaidOff(int id)
        {
            bool paidOff = false;

            var loads = (from loan in _context.Set<LoanPayment>()
                         join payment in _context.Set<PaymentEntity>()
                         on loan.PaymentEntityId equals payment.Id
                         where loan.LoanEntityId == id
                         select new { loan.LoanEntityId, payment.Id, payment.Amount, payment.Voucher, payment.DateRealization, payment.SetedDate, payment.Done }).ToList();

            foreach (var load in loads)
            {
                if (!load.Done) return false;

                paidOff = true;
            }
            return paidOff;
        }

        public int GetLoanId(PaymentDto dto)
        {
            var loanEntity = from loan in _context.Set<LoanPayment>()
                             join payment in _context.Set<PaymentEntity>()
                             on loan.PaymentEntityId equals payment.Id
                             where loan.LoanEntityId == dto.Id
                             select new { loan.LoanEntityId };

            List<PaymentGetDto> loans = (List<PaymentGetDto>)loanEntity;
            int loanId = loans[0].Id;
            return loanId;
        }

        public async Task FinishLoan(PaymentDto dto)
        {
            int loanId = GetLoanId(dto);

            dto.DateRealization = Convert.ToDateTime(DateTime.Now.ToString("dd-MM-yyyy"));

            if (IsLoanPaidOff(loanId))
            {
                LoanDto loan = LoanService.GetById(loanId);
                loan.Status = "Finalized";
                await LoanService.Update(loan);
            }
        }
    }
}
