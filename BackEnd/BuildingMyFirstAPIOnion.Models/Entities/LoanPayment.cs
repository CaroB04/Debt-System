using BuildingMyFirstAPIOnion.Core.BaseModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingMyFirstAPIOnion.Models.Entities
{
    public class LoanPayment : BaseEntity
    {
        public int LoanEntityId { get; set; }
        public int PaymentEntityId { get; set; }

    }
}
