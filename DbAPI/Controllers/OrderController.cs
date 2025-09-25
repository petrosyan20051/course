using db.Interfaces;
using db.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TypeId = int;

namespace db.Controllers {

    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : BaseCrudController<Order, TypeId> {
        private readonly ILogger<Order> _logger;

        public OrderController(IRepository<Order, TypeId> repository, ILogger<Order> logger) : base(repository) {
            _logger = logger;
        }

        protected TypeId GetEntityId(Order entity) {
            return entity.Id;
        }

        // GET: api/{entity}/
        [HttpGet]
        [Authorize(Roles = "Basic, Editor, Admin")]
        public override async Task<ActionResult<IEnumerable<Order>>> GetAll() {
            _logger.LogInformation($"\"{User.Identity.Name}\" ������ ������ \"Order.GetAll()\"");
            return Ok(await _repository.GetAllAsync());
        }

        // GET: api/{entity}/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "Basic, Editor, Admin")]
        public override async Task<ActionResult<Order>> Get(TypeId id) {
            var entity = await _repository.GetByIdAsync(id);
            _logger.LogInformation($"\"{User.Identity.Name}\" ������ ������ \"Order.Get({id})\"");
            return entity is null ? NotFound(new { message = $"�������� � ID = {id} �� �������" }) : Ok(entity);
        }

        // POST: api/{entity}/
        [HttpPost]
        [Authorize(Roles = "Editor, Admin")]
        public override async Task<ActionResult<Order>> Create([FromBody] Order entity) {
            _logger.LogWarning($"\"{User.Identity.Name}\" ������ ������ \"Order.Create()\"");
            try {
                await _repository.AddAsync(entity);
            } catch (Exception ex) {
                _logger.LogError($"������ \"Order.Create()\" ������������ \"{User.Identity.Name}\" ���������� �������. �������: {ex.Message}");
                return BadRequest(new { message = ex.Message });
            }

            _logger.LogInformation($"������ \"Order.Create()\" ������������ \"{User.Identity.Name}\" �������");
            return CreatedAtAction(nameof(Get), new { id = GetEntityId(entity) }, entity);
        }

        // PUT: api/{entity}/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Editor, Admin")]
        public override async Task<IActionResult> Update(TypeId id, [FromBody] Order entity) {
            _logger.LogWarning($"\"{User.Identity.Name}\" ������ ������ \"Order.Update({id})\"");
            if (!id.Equals(GetEntityId(entity))) {
                _logger.LogError($"������ \"Order.Update({id})\" ������������ \"{User.Identity.Name}\" ���������� �������. " +
                    $"�������: �������� �� �������");
                return BadRequest(new { message = $"�������� � ID = {id} �� �������" });
            }

            await _repository.UpdateAsync(entity);
            _logger.LogInformation($"������ \"Order.Update({id})\" ������������ \"{User.Identity.Name}\" �������");
            return NoContent();
        }

        // DELETE: api/{entity}/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public override async Task<IActionResult> Delete(TypeId id) {
            _logger.LogWarning($"\"{User.Identity.Name}\" ������ ������ \"Order.Delete({id})\"");
            if (await _repository.GetByIdAsync(id) == null) {
                _logger.LogError($"������ \"Order.Delete({id})\" ������������ \"{User.Identity.Name}\" ���������� �������. " +
                    $"�������: �������� �� �������");
                return BadRequest(new { message = $"�������� � ID = {id} �� �������" });
            }
            await _repository.SoftDeleteAsync(id);
            _logger.LogInformation($"������ \"Order.Delete({id})\" ������������ \"{User.Identity.Name}\" �������");
            return NoContent();
        }

        // Update: api/{entity}/{id}/recover
        [HttpPatch("{id}/recover")]
        [Authorize(Roles = "Admin")]
        public override async Task<IActionResult> RecoverAsync(TypeId id) {
            _logger.LogWarning($"\"{User.Identity.Name}\" ������ ������ \"Order.RecoverAsync({id})\"");
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) {
                entity.IsDeleted = null;
                entity.WhenChanged = DateTime.Now;
                await _repository.UpdateAsync(entity);

                _logger.LogInformation($"������ \"Order.RecoverAsync({id})\" ������������ \"{User.Identity.Name}\" �������");
                return Ok("�������������� ������ �������");
            }

            _logger.LogError($"������ \"Order.RecoverAsync({id})\" ������������ \"{User.Identity.Name}\" ���������� �������. " +
                    $"�������: �������� �� ������� ��� ��� ����������");
            return NotFound(new { message = $"�������� � ID = {id} �� ������� ��� ��� ����������" });
        }
    }
}