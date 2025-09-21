using db.Interfaces;
using db.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TypeId = int;

namespace DbAPI.Controllers {

    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : BaseCrudController<Role, TypeId> {
        public RoleController(IRepository<Role, TypeId> repository) : base(repository) { }

        protected int GetEntityId(Role entity) {
            return entity.Id;
        }

        // GET: api/{entity}/GetAll
        [HttpGet("GetAll")]
        [Authorize(Roles = "Admin")]
        public override async Task<ActionResult<IEnumerable<Role>>> GetAll() {
            return Ok(await _repository.GetAllAsync());
        }

        // GET: api/{entity}/GetById
        [HttpGet("GetById")]
        [Authorize(Roles = "Admin")]
        public override async Task<ActionResult<Role>> Get(TypeId id) {
            var entity = await _repository.GetByIdAsync(id);
            return entity is null ? NotFound() : Ok(entity);
        }

        // POST: api/{entity}/Post
        [HttpPost("Post")]
        [Authorize(Roles = "Admin")]
        public override async Task<ActionResult<Role>> Create([FromBody] Role entity) {
            await _repository.AddAsync(entity);
            return CreatedAtAction(nameof(Get), new { id = GetEntityId(entity) }, entity);
        }

        // PUT: api/{entity}/UpdateById
        [HttpPut("UpdateById")]
        [Authorize(Roles = "Admin")]
        public override async Task<IActionResult> Update(TypeId id, [FromBody] Role entity) {
            if (!id.Equals(GetEntityId(entity))) {
                return BadRequest();
            }

            await _repository.UpdateAsync(entity);
            return NoContent();
        }

        // DELETE: api/{entity}/DeleteById
        [HttpDelete("DeleteById")]
        [Authorize(Roles = "Admin")]
        public override async Task<IActionResult> Delete(TypeId id) {
            await _repository.SoftDeleteAsync(id);
            return NoContent();
        }

        [HttpPost("RecoverById")]
        [Authorize(Roles = "Admin")]
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
