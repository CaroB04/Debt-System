using BuildingMyFirstAPIOnion.BL.DTO;
using BuildingMyFirstAPIOnion.Models.Entities;
using BuildingMyFirstAPIOnion.Services.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildingMyFirstAPIOnion.Presentation.Controllers
{
    public class PaymentController : BaseController<PaymentEntity, PaymentDto>
    {
        readonly IPaymentService _service;
        public PaymentController(IPaymentService service) : base(service)
        {
            _service = service;
        }

        [HttpGet("loan/{id}")]
        public IActionResult GetPaymentsByLoan(int id)
        {
            var data = _service.GetPaymentByLoan(id);

            if (data == null) return NotFound($"The record with id {id} was not found");
            return Ok(data);
        }

    }
}
