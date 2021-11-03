using BuildingMyFirstAPIOnion.BL.DTO;
using BuildingMyFirstAPIOnion.Models.Entities;
using BuildingMyFirstAPIOnion.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildingMyFirstAPIOnion.Presentation.Controllers
{
    public class UserController : BaseController<UserEntity, UserDto>
    {
        public UserController(IUserService service) : base(service)
        {

        }
    }
}
