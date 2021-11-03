using BuildingMyFirstAPIOnion.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingMyFirstAPIOnion.BL.DTO
{
   public class UserDto : BaseDTO
    {
        public string Name { get; set; }
        public string IdentityCard { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string PassWord { get; set; }
        public string Email { get; set; }
    }
}
