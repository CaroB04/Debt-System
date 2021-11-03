using BuildingMyFirstAPIOnion.Models.Contexts;
using BuildingMyFirstAPIOnion.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingMyFirstAPIOnion.Services.Services
{
   public class LoanPaymentService
    {
        readonly BaseContext _context;

         public LoanPaymentService(BaseContext context)
        {
            _context = context;
        } 

        public void Create(int idPayment, int idLoan)
        {
            LoanPayment loanPayment = new LoanPayment();
            loanPayment.LoanEntityId = idLoan;
            loanPayment.PaymentEntityId = idPayment;
            loanPayment.Deleted = false;

            _context.LoanPayment.AddAsync(loanPayment);
        }

    }
}
