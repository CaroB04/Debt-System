using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingMyFirstAPIOnion.BL.DTO
{
    public class PaymentGetDto : BaseDTO
    {
        public int LoanEntityId { get; set; }
        public string Voucher { get; set; }
        public DateTime? DateRealization { get; set; }
        public DateTime SetedDate { get; set; }
        public double Amount { get; set; }
        public bool Done { get; set; }
    }
}
