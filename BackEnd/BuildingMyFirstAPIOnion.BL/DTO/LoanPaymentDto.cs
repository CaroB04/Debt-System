using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingMyFirstAPIOnion.BL.DTO
{
    public class LoanPaymentDto : BaseDTO
    {
        public int LoanId { get; set; }
        public int PaymentId { get; set; }
    }
}
