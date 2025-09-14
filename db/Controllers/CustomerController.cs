using db.Interfaces;
using db.Models;
using Microsoft.AspNetCore.Mvc;

using TypeId = int;

namespace db.Controllers {

    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : BaseCrudController<Customer, TypeId> {

        public CustomerController(IRepository<Customer, TypeId> repository) : base(repository) { }

        protected int GetEntityId(Customer entity) {
            return entity.Id;
        }

        // GET: api/{entity}/GetAll
        [HttpGet("GetAll")]
        public override async Task<ActionResult<IEnumerable<Customer>>> GetAll() {
            return Ok(await _repository.GetAllAsync());
        }

        // GET: api/{entity}/GetById
        [HttpGet("GetById")]
        public override async Task<ActionResult<Customer>> Get(TypeId id) {
            var entity = await _repository.GetByIdAsync(id);
            return entity is null ? NotFound() : Ok(entity);
        }

        // POST: api/{entity}/Post
        [HttpPost("Post")]
        public override async Task<ActionResult<Customer>> Create([FromBody] Customer entity) {
            await _repository.AddAsync(entity);
            return CreatedAtAction(nameof(Get), new { id = GetEntityId(entity) }, entity);
        }

        // PUT: api/{entity}/UpdateById
        [HttpPut("UpdateById")]
        public override async Task<IActionResult> Update(TypeId id, [FromBody] Customer entity) {
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