using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingMyFirstAPIOnion.Core.BaseModels
{
        public interface IBaseEntity
        {
            public int Id { get; set; }
            public bool Deleted { get; set; }
        }
        public class BaseEntity
        {
            public int Id { get; set; }
            public bool Deleted { get; set; }
        }
}
