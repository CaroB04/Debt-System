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
    public class LoanController : BaseController<LoanEntity, LoanDto>
    {
        readonly ILoanService _service;
        public LoanController(ILoanService service) : base(service)
        {
             _service = service;
        }

        [HttpGet("{id}")]
        public override async Task<IActionResult> GetById(int id)
        {
            var data = _service.GetByIdLoan(id);

            if (data == null) return NotFound($"The record with id {id} was not found");
            return Ok(data);

        }

        [HttpGet("loanstaken/{id}")]
        public async Task<IActionResult> GetTakenLoansByUser(int id)
        {
            var data = await _service.GetTakenLoansByUser(id);

            if (data == null) return NotFound($"The record with id {id} was not found");
            return Ok(data);

        }

        [HttpGet("loansgiven/{id}")]
        public async Task<IActionResult> GetGivenLoansByUser(int id)
        {
            var data = await _service.GetGivenLoansByUser(id);

            if (data == null) return NotFound($"The record with id {id} was not found");
            return Ok(data);

        }

    }
}
