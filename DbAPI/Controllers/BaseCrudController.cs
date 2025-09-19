using db.Interfaces;
using db.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseCrudController<TEntity, TKey> : ControllerBase {
    protected readonly IRepository<TEntity, TKey> _repository;

    public BaseCrudController(IRepository<TEntity, TKey> repository) {
        _repository = repository;
    }

    // GET: api/{entity}/GetAll
    [HttpGet("GetAll")]
    public abstract Task<ActionResult<IEnumerable<TEntity>>> GetAll();

    // GET: api/{entity}/GetById
    [HttpGet("GetById")]
    public abstract Task<ActionResult<TEntity>> Get(TKey id);

    // POST: api/{entity}/Post
    [HttpPost("Post")]
    public abstract Task<ActionResult<TEntity>> Create([FromBody] TEntity entity);

    // PUT: api/{entity}/UpdateById
    [HttpPut("UpdateById")]
    public abstract Task<IActionResult> Update(TKey id, [FromBody] TEntity entity);

    // DELETE: api/{entity}/DeleteById
    [HttpDelete("DeleteById")]
    public abstract Task<IActionResult> Delete(TKey id);

    // Update: api/{entity}/RecoverById
    [HttpGet("RecoverById")]
    public abstract Task<IActionResult> RecoverAsync(TKey id);
}