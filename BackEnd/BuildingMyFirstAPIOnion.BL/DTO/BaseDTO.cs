using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingMyFirstAPIOnion.BL.DTO
{
    public interface IBaseDTO
    {
        public int Id { get; set; }
        public bool Deleted { get; set; }
    }
   public class BaseDTO : IBaseDTO
    {
        public int Id { get; set; }
        public bool Deleted { get; set; }

    }
}
