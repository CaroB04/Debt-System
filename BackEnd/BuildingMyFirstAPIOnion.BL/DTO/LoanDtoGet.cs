using BuildingMyFirstAPIOnion.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingMyFirstAPIOnion.BL.DTO
{
   public class LoanDtoGet : BaseDTO
    {
        public UserDto Lender { get; set; }
        public UserDto Debtor { get; set; }
        public double Amount { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime CreationDate { get; set; }
        public string Term { get; set; }
        public int AmountPayments { get; set; }
        public string Status { get; set; }
        public double AmountPerPayment { get; set; }
        public bool PaymentsCreated { get; set; }
    }
}
