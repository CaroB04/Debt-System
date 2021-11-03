using BuildingMyFirstAPIOnion.Core.BaseModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingMyFirstAPIOnion.Models.Entities
{
    public class PaymentEntity : BaseEntity
    {
        public string Voucher { get; set; }
        public DateTime? DateRealization { get; set; }
        public DateTime SetedDate { get; set; }
        public double Amount { get; set; }
        public bool Done { get; set; }
        public virtual ICollection<LoanPayment> LoanPayments { get; set; }
    }
}
