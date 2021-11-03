using BuildingMyFirstAPIOnion.Core.BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using static BuildingMyFirstAPIOnion.Core.Enums.DeadlineEnum;
using static BuildingMyFirstAPIOnion.Core.Enums.StatusEnum;

namespace BuildingMyFirstAPIOnion.Models.Entities
{
    public class LoanEntity : BaseEntity
    {
        public int LenderId { get; set; }
        public int DebtorId { get; set; }
        public double Amount { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime CreationDate { get; set; }
        public Deadline Term { get; set; }
        public int AmountPayments { get; set; }
        public double AmountPerPayment { get; set; }
        public Status Status { get; set; }
        public bool PaymentsCreated { get; set; }

        [ForeignKey("DebtorId")]
        public virtual UserEntity Debtor { get; set; }
        [ForeignKey("LenderId")]
        public virtual UserEntity Lender { get; set; }
        public virtual ICollection<LoanPayment> LoanPayments { get; set; }
    }
}
