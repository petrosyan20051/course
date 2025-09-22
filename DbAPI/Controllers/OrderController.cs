using db.Interfaces;
using db.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TypeId = int;

namespace db.Controllers {

    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : BaseCrudController<Order, TypeId> {
        public OrderController(IRepository<Order, TypeId> repository) : base(repository) { }

        protected TypeId GetEntityId(Order entity) {
            return entity.Id;
        }

        // GET: api/{entity}/
        [HttpGet]
        [Authorize(Roles = "Basic, Editor, Admin")]
        public override async Task<ActionResult<IEnumerable<Order>>> GetAll() {
            return Ok(await _repository.GetAllAsync());
        }

        // GET: api/{entity}/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "Basic, Editor, Admin")]
        public override async Task<ActionResult<Order>> Get(TypeId id) {
            var entity = await _repository.GetByIdAsync(id);
            return entity is null ? NotFound(new { message = $"�������� � ID = {id} �� �������" }) : Ok(entity);
        }

        // POST: api/{entity}/
        [HttpPost]
        [Authorize(Roles = "Editor, Admin")]
        public override async Task<ActionResult<Order>> Create([FromBody] Order entity) {
            try {
                await _repository.AddAsync(entity);
            } catch (Exception ex) {
                return BadRequest(new { message = ex.Message });
            }

            return CreatedAtAction(nameof(Get), new { id = GetEntityId(entity) }, entity);
        }

        // PUT: api/{entity}/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Editor, Admin")]
        public override async Task<IActionResult> Update(TypeId id, [FromBody] Order entity) {
            if (!id.Equals(GetEntityId(entity))) {
                return BadRequest(new { message = $"�������� � ID = {id} �� �������" });
            }

            await _repository.UpdateAsync(entity);
            return NoContent();
        }

        // DELETE: api/{entity}/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public override async Task<IActionResult> Delete(TypeId id) {
            if (await _repository.GetByIdAsync(id) == null) {
                return BadRequest(new { message = $"�������� � ID = {id} �� �������" });
            }
            await _repository.SoftDeleteAsync(id);
            return NoContent();
        }

        // Update: api/{entity}/{id}/recover
        [HttpPatch("{id}/recover")]
        [Authorize(Roles = "Admin")]
        public override async Task<IActionResult> RecoverAsync(TypeId id) {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) {
                entity.IsDeleted = null;
                entity.WhenChanged = DateTime.Now;
                await _repository.UpdateAsync(entity);

                return Ok("�������������� ������ �������");
            }

            return NotFound(new { message = $"�������� � ID = {id} �� ������� ��� ��� ����������" });
        }
    }
}