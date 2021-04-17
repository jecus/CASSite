using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Entity.Entity;
using Entity.Infrastructure;
using Kendo.DynamicLinqCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CasApi.Absteractions.Controller
{
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class BaseGetController<TEntity> : ControllerBase
        where TEntity : class, IBaseEntity
    {
        private DatabaseContext _db;

        protected BaseGetController(DatabaseContext db)
        {
            _db = db;
        }
        
        /// <summary>
        /// Чтение объекта по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IBaseEntity), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public virtual async Task<ActionResult<TEntity>> Get(int id)
        {
            var entity = await _db.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(i => i.ItemId == id);
            
            if (entity == null)
                return NotFound();

            return Ok(entity);
        }
        
        /// <summary>
        /// Чтение списка
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost]
        public virtual async Task<ActionResult<List<TEntity>>> GetAll([FromBody] DataSourceRequest request)
        {
            var res = _db.Set<TEntity>()
                .AsNoTracking()
                .ToDataSourceResult(request.Take, request.Skip, request.Sort, request.Filter, request.Aggregate, request.Group);
            return Ok(res.Data.OfType<TEntity>());
        }
    }
}