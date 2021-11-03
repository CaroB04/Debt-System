using BuildingMyFirstAPIOnion.Core.BaseModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingMyFirstAPIOnion.Models.Entities
{
    public class UserEntity : BaseEntity
    {
        public string Name { get; set; }
        public string LastName { get; set; } //Borrar
        public string IdentityCard { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string PassWord { get; set; }
        public string Email { get; set; }
        public DateTime RegistrationDate { get; set; }
        public virtual ICollection<LoanEntity> LoansGiven { get; set; }
        public virtual ICollection<LoanEntity> LoansTaken { get; set; }
    }
}
