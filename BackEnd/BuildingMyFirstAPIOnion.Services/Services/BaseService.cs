using System;
using System.Collections.Generic;
using System.Text;
using BuildingMyFirstAPIOnion.Core.BaseModels;
using BuildingMyFirstAPIOnion.BL.DTO;
using AutoMapper;
using BuildingMyFirstAPIOnion.Models.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using BuildingMyFirstAPIOnion.Core.Abstract;
using BuildingMyFirstAPIOnion.BL.Extensions;

namespace BuildingMyFirstAPIOnion.Services
{
    public interface IBaseService<TEntity, TDto> where TEntity : BaseEntity where TDto : BaseDTO
     {
        IQueryable<TEntity> Query();
        ICollection<TDto> GetAll();
        TDto GetById(int id);
        Task<IEntityOperationResult<TDto>> Create(TDto dto);
        Task<IEntityOperationResult<TDto>> Update(TDto dto);
        Task<IEntityOperationResult<TDto>>  Delete(int id);

    }
    public class BaseService<TEntity, TDto> where TEntity : BaseEntity where TDto : BaseDTO
    {
        protected readonly BaseContext _context;
        protected readonly DbSet<TEntity> _dbSet;
        protected readonly IMapper _mapper;
        protected readonly IValidator<TDto> _validator;

        public BaseService(BaseContext context, IMapper mapper, IValidator<TDto> validator)
        {
            _mapper = mapper; 
            _context = context;
            _dbSet = _context.Set<TEntity>();
            _validator = validator;
        }

        public virtual IQueryable <TEntity> Query()
        {
            return _dbSet.AsQueryable();

        }

        public virtual ICollection<TDto> GetAll()
        {
            var entities = Query();

            var dtos = _mapper.Map<ICollection<TDto>>(entities);

            return dtos;
        }

        public virtual TDto GetById(int id)
        {
            var entity = Query().Where(x => x.Id == id).FirstOrDefault();
            return _mapper.Map<TDto>(entity);
        }

        public virtual async Task<IEntityOperationResult<TDto>> Create(TDto dto)
        {
            var validationResult = _validator.Validate(dto);
            if (validationResult.IsValid is false)
                return validationResult.ToOperationResult<TDto>();

            var entity = _mapper.Map<TEntity>(dto);

            _dbSet.Add(entity);

            await _context.SaveChangesAsync();

            _mapper.Map(entity, dto);

            var result = dto.ToOperationResult();

            return result;
        }

        public virtual async Task<IEntityOperationResult<TDto>> Update(TDto dto)
        {
            var validationResult = _validator.Validate(dto);
            if (validationResult.IsValid is false)
                return validationResult.ToOperationResult<TDto>();

            var entityExist = Query().Any(x => x.Id == dto.Id);

            if (entityExist is false) return null;

            var entity = _mapper.Map<TEntity>(dto); 

            _dbSet.Update(entity);

            await _context.SaveChangesAsync();

            _mapper.Map(entity, dto);

            var result = dto.ToOperationResult();

            return result;
             
        }

        public virtual async Task<IEntityOperationResult<TDto>> Delete(int id)
        {
            var entity = await Query().Where(x => x.Id == id).FirstOrDefaultAsync();

            if (entity is null) return null;

            _dbSet.Remove(entity);

            var dto = _mapper.Map<TDto>(entity);
            await _context.SaveChangesAsync();

            var result = dto.ToOperationResult();
            return result;   
        }
    }
}
