using db.Interfaces;
using db.Models;
using Microsoft.AspNetCore.Mvc;

using TypeId = int;

namespace db.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class RouteController : BaseCrudController<Models.Route, TypeId> {
        public RouteController(IRepository<Models.Route, int> repository) : base(repository) { }

        protected int GetEntityId(Models.Route entity) {
            return entity.Id;
        }

        // GET: api/{entity}/GetAll
        [HttpGet("GetAll")]
        public override async Task<ActionResult<IEnumerable<Models.Route>>> GetAll() {
            return Ok(await _repository.GetAllAsync());
        }

        // GET: api/{entity}/GetById
        [HttpGet("GetById")]
        public override async Task<ActionResult<Models.Route>> Get(TypeId id) {
            var entity = await _repository.GetByIdAsync(id);
            return entity is null ? NotFound() : Ok(entity);
        }

        // POST: api/{entity}/Post
        [HttpPost("Post")]
        public override async Task<ActionResult<Models.Route>> Create([FromBody] Models.Route entity) {
            await _repository.AddAsync(entity);
            return CreatedAtAction(nameof(Get), new { id = GetEntityId(entity) }, entity);
        }

        // PUT: api/{entity}/UpdateById
        [HttpPut("UpdateById")]
        public override async Task<IActionResult> Update(TypeId id, [FromBody] Models.Route entity) {
            if (!id.Equals(GetEntityId(entity))) {
                return BadRequest();
            }

            await _repository.UpdateAsync(entity);
            return NoContent();
        }

        // DELETE: api/{entity}/DeleteById
        [HttpDelete("DeleteById")]
        public override async Task<IActionResult> Delete(TypeId id) {
            await _repository.SoftDeleteAsync(id);
            return NoContent();
        }

        [HttpPost("RecoverById")]
        public override async Task<IActionResult> RecoverAsync(TypeId id) {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) {
                entity.IsDeleted = null;
                entity.WhenChanged = DateTime.Now;
                await _repository.UpdateAsync(entity);
                return Ok(new string("Восстановление прошло успешно"));
            }

            return NotFound(new string("Сущность не удалена или не найдена"));
        }
    }
}
