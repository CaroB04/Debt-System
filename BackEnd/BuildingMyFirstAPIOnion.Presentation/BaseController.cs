using BuildingMyFirstAPIOnion.BL.DTO;
using BuildingMyFirstAPIOnion.Core.BaseModels;
using BuildingMyFirstAPIOnion.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;


namespace BuildingMyFirstAPIOnion.Presentation.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class BaseController<TEntity, TDto> : ControllerBase where TEntity : BaseEntity where TDto : BaseDTO
    {
        private readonly IBaseService<TEntity, TDto> _baseService;

        public BaseController(IBaseService<TEntity, TDto> baseService)
        {
            _baseService = baseService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var dtos = _baseService.GetAll();
            return Ok(dtos);
        }

        [HttpGet("Query")]
        public IActionResult Query()
        {
            var queryResult = _baseService.Query();
            return Ok(queryResult);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TDto dto)
        {
            var dtoResult = await _baseService.Create(dto);

            if (dtoResult.IsSuccess is false) 
                return UnprocessableEntity(dtoResult);

            return CreatedAtAction(WebRequestMethods.Http.Get, new { id = dtoResult.Entity.Id }, dtoResult.Entity);
            
        }

        [HttpPut]
        public async Task<IActionResult> Update( [FromBody] TDto dto)
        {
            var dtoResult = await _baseService.Update( dto);
            if (dtoResult is null)
                return NotFound($"The record with id {dto.Id} was not found");

            if (dtoResult.IsSuccess is false)
                return UnprocessableEntity(dtoResult);

            return Ok(dtoResult);

        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetById(int id)
        {
            var data = _baseService.GetById(id);

            if (data == null) return NotFound($"The record with id {id} was not found");
            return Ok(data);

        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete([FromRoute]int id)
        {
            var result = await _baseService.Delete(id);

            if (result.IsSuccess is false)
                return NotFound($"The record with id {id} was not found");

            return Ok(result);
        }

    }
}
