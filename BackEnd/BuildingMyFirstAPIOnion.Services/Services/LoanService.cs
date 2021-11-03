using AutoMapper;
using BuildingMyFirstAPIOnion.BL.DTO;
using BuildingMyFirstAPIOnion.BL.Extensions;
using BuildingMyFirstAPIOnion.Core.Abstract;
using BuildingMyFirstAPIOnion.Core.Enums;
using BuildingMyFirstAPIOnion.Models.Contexts;
using BuildingMyFirstAPIOnion.Models.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BuildingMyFirstAPIOnion.Core.Enums.DeadlineEnum;

namespace BuildingMyFirstAPIOnion.Services.Services
{
    public interface ILoanService : IBaseService<LoanEntity, LoanDto>
    {
        LoanDtoGet GetByIdLoan(int id);
        Task<ICollection<LoanDtoGet>> GetTakenLoansByUser(int id);
        Task<ICollection<LoanDtoGet>> GetGivenLoansByUser(int id);
    }

    public class LoanService : BaseService<LoanEntity, LoanDto>, ILoanService
    {
        readonly PaymentService paymentService;
        readonly UserService userService;

        //public LoanService(BaseContext context, IMapper mapper, IValidator<LoanDto> validator, IValidator<PaymentDto> validatorPayment, IValidator<UserDto> validatorUser) : base (context, mapper, validator)
        //{
        //    paymentService = new PaymentService(context, mapper, validatorPayment);
        //    userService = new UserService(context, mapper, validatorUser, validator, validatorPayment);
        //}
        public LoanService(BaseContext context, IMapper mapper, IValidator<LoanDto> validator, IValidator<PaymentDto> validatorPayment, IValidator<UserDto> validatorUser) : base (context, mapper, validator)
        {
            paymentService = new PaymentService(context, mapper, validatorPayment);
            userService = new UserService(context, mapper, validatorUser);
        }

        public async override Task<IEntityOperationResult<LoanDto>> Create(LoanDto dto)
        {
            var validationResult = _validator.Validate(dto);
            if (validationResult.IsValid is false)
                return validationResult.ToOperationResult<LoanDto>();

            var entity = _mapper.Map<LoanEntity>(dto);

            CalculateAmount(entity);

            entity.CreationDate = Convert.ToDateTime(DateTime.Now.ToString("dd-MM-yyyy"));
            entity.Status = 0;
            entity.PaymentsCreated = false;

            _dbSet.Add(entity);

            await _context.SaveChangesAsync();

            if (Convert.ToInt32(entity.Status) == 1 && entity.PaymentsCreated is false)
            {
                await CreatePayments(entity);
            }

            _mapper.Map(entity, dto);

            var result = dto.ToOperationResult();

            return result;
        }

        public LoanDtoGet GetByIdLoan(int id)
        {
            var loan = base.GetById(id);

            var debtor = userService.GetById(loan.DebtorId);
            var lender = userService.GetById(loan.LenderId);

            LoanDtoGet loanGet = new LoanDtoGet();
            loanGet.Amount = loan.Amount;
            loanGet.AmountPayments = loan.AmountPayments;
            loanGet.AmountPerPayment = loan.AmountPerPayment;
            loanGet.Deleted = loan.Deleted;
            loanGet.Id = loan.Id;
            loanGet.PaymentsCreated = loan.PaymentsCreated;
            loanGet.StartDate = loan.StartDate;
            loanGet.Status = loan.Status;
            loanGet.Term = loan.Term;
            loanGet.Lender = lender;
            loanGet.Debtor = debtor;

            return loanGet;

        }

        private static void CalculateAmount(LoanEntity entity)
        {
            int AmountPayments = Convert.ToInt32(entity.Amount / (int)entity.Term);
            int AmountPerPayment = Convert.ToInt32(entity.Amount) / AmountPayments;

            int subTotal = AmountPayments * AmountPerPayment;

            int subTotalAmount = Convert.ToInt32(entity.Amount) - subTotal;

            if (subTotalAmount > 0)
            {
                entity.AmountPayments = AmountPayments + 1;
            }
            else
            {
                entity.AmountPayments = AmountPayments;
            }

            entity.AmountPerPayment = AmountPerPayment;
        }

        public async override Task<IEntityOperationResult<LoanDto>> Update(LoanDto dto)
        {
            var validationResult = _validator.Validate(dto);
            if (validationResult.IsValid is false)
                return validationResult.ToOperationResult<LoanDto>();

            var entityExist = Query().Any(x => x.Id == dto.Id);

            if (entityExist is false) return null;

            //string Status = dto.Status.ToString();

            //if (dto.Status == "Finalized")
            //{
            //    if (!IsLoanPaidOff(dto.Id))
            //    {
            //        var entities = Query().Where(loan => loan.Id == dto.Id).ToList();
            //        foreach (var item in entities)
            //        {
            //            dto.Status = item.Status.ToString();
            //        }
            //    }
            //}

            //Status = dto.Status.ToString();

            var entity = _mapper.Map<LoanEntity>(dto);

            int status = (int)entity.Status;

            if ((int)entity.Status == 1 && entity.PaymentsCreated is false)
            { 
                await CreatePayments(entity);
                entity.PaymentsCreated = true;
            }

            _dbSet.Update(entity);

            await _context.SaveChangesAsync();
           
             _mapper.Map(entity, dto);

            var result = dto.ToOperationResult();

            return result;
        }

        private async Task CreatePayments(LoanEntity entity)
        {
            DateTime nextDate = Convert.ToDateTime(entity.StartDate);

            double amount = entity.Amount;

                for (int payment = 0; payment <= entity.AmountPayments; payment++)
                {
                    PaymentDto paymentDto = new PaymentDto();
                    paymentDto.Amount = entity.AmountPerPayment;
                    paymentDto.Done = false;
                    paymentDto.LoanEntityId = entity.Id;
                    paymentDto.Voucher = "";

                    if (payment == 0)
                    {
                        paymentDto.SetedDate = Convert.ToDateTime(entity.StartDate);
                        continue;
                    }
                    paymentDto.SetedDate = nextDate.AddDays((int)entity.Term);
                    nextDate = paymentDto.SetedDate;

                if(payment != 0 )
                {
                    amount -= entity.AmountPerPayment;
                }
                

                if (amount <= entity.AmountPerPayment && amount != 0)
                {
                    paymentDto.Amount = amount;
                }

                await paymentService.Create(paymentDto);
                }

            }

        public async Task<ICollection<LoanDtoGet>> GetTakenLoansByUser(int id)
        {
            var entities = Query().Where(loan => loan.DebtorId == id).ToList();
            var dtos = _mapper.Map<ICollection<LoanDto>>(entities);

            List<LoanDto> data = (List<LoanDto>)dtos;
            List<LoanDtoGet> dataResult = new List<LoanDtoGet>();

            foreach (var loan in data)
            {
                var loanById = base.GetById(id);

                LoanDtoGet loanGet = new LoanDtoGet();

                var debtor = userService.GetById(loan.DebtorId);
                var lender = userService.GetById(loan.LenderId);
                loanGet.Amount = loan.Amount;
                loanGet.AmountPayments = loan.AmountPayments;
                loanGet.AmountPerPayment = loan.AmountPerPayment;
                loanGet.Deleted = loan.Deleted;
                loanGet.Id = loan.Id;
                loanGet.PaymentsCreated = loan.PaymentsCreated;
                loanGet.StartDate = loan.StartDate;
                loanGet.Status = loan.Status;
                loanGet.Term = loan.Term;
                loanGet.Lender = lender;
                //loanGet.Debtor = debtor;

                dataResult.Add(loanGet);

            }

            return dataResult; 
        }

        public async Task<ICollection<LoanDtoGet>> GetGivenLoansByUser(int id)
        {
            var entities = Query().Where(loan => loan.LenderId == id).ToList();
            var dtos = _mapper.Map<ICollection<LoanDto>>(entities);

            List<LoanDto> data = (List<LoanDto>)dtos;
            List<LoanDtoGet> dataResult = new List<LoanDtoGet>();

            foreach (var loan in data)
            {
                var loanById = base.GetById(id);

                LoanDtoGet loanGet = new LoanDtoGet();

                var debtor = userService.GetById(loan.DebtorId);
                var lender = userService.GetById(loan.LenderId);
                loanGet.Amount = loan.Amount;
                loanGet.AmountPayments = loan.AmountPayments;
                loanGet.AmountPerPayment = loan.AmountPerPayment;
                loanGet.Deleted = loan.Deleted;
                loanGet.Id = loan.Id;
                loanGet.PaymentsCreated = loan.PaymentsCreated;
                loanGet.StartDate = loan.StartDate;
                loanGet.Status = loan.Status;
                loanGet.Term = loan.Term;
                //loanGet.Lender = lender;
                loanGet.Debtor = debtor;

                dataResult.Add(loanGet);

            }

            return dataResult;
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
    }

}
