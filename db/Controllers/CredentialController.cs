using db.Classes;
using db.Models;
using db.Repositories;
using Microsoft.AspNetCore.Mvc;
using static db.Interfaces.IInformation;

namespace db.Controllers {

    [ApiController]
    [Route("api/[controller]")]
    public class CredentialController : ControllerBase {

        CredentialRepository _credentialRepository;
        RoleRepository _roleRepository;

        public CredentialController(CredentialRepository credentialRepository, RoleRepository roleRepository) {
            _credentialRepository = credentialRepository;
            _roleRepository = roleRepository;
        }

        protected int GetEntityId(Credential entity) {
            return entity.Id;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginPrompt request) {
            // Standart checks
            if (_credentialRepository == null || _roleRepository == null)
                return StatusCode(500, new { message = "Внутренняя ошибка" });

            try {
                // Whether credential with such username exists
                var credential = await _credentialRepository.GetByUserNameAsync(request.Login);
                if (credential == null || credential.IsDeleted != null)
                    return Unauthorized(new { message = $"Пользователем с именем \"{request.Login}\" не существует" });

                // Password verification
                if (!PasswordHasher.VerifyPassword(request.Password, credential.Password))
                    return Unauthorized(new { messsage = "Введен неверный пароль" });

                // Whether such role exists 
                var role = await _roleRepository.GetByIdAsync(credential.RoleId);
                if (role == null || role.IsDeleted != null)
                    return BadRequest(new { message = "Внутренняя ошибка" });

                var response = new LoginResponse {
                    UserId = credential.Id,
                    Username = credential.Username,
                    CanGet = role.CanGet,
                    CanPost = role.CanPost,
                    CanUpdate = role.CanUpdate,
                    CanDelete = role.CanDelete,
                };

                return Ok(response);
            } catch (Exception ex) {
                return StatusCode(500, new { message = "Внутренняя ошбика" });
            }
            
            

        }

        // TODO: make registration
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterPrompt request) {
            // Standart checks
            if (_credentialRepository == null || _roleRepository == null)
                return StatusCode(500, new { message = "Внутренняя ошибка" });

            // Whether user with such name exists
            var credential = await _credentialRepository.GetByUserNameAsync(request.UserName);
            if (credential != null)
                return BadRequest(new { message = $"Пользователь с именем \"{request.UserName}\" уже существует" });

            // Check whether password is strong
            if (!PasswordHasher.IsPasswordStrong(request.Password))
                return BadRequest(new { message = "Введенный пароль ненадёжен" });

            // Whether selected role exists
            var role = await _roleRepository.GetByUserRights(request.RegisterRights);
            if (role == null)
                return BadRequest(new { message = "Введенные права пользователя не существуют" });

            bool canUpdate, canDelete;
            if (request.RegisterType != RegisterType.Anonymous) {
                canUpdate = request.RegisterType != RegisterType.Anonymous;
                canDelete = request.RegisterType != RegisterType.Anonymous;
            } else {
                canUpdate = false;
                canDelete = false;
            }

            var response = new RegisterResponse {
                UserName = request.UserName,
                CanGet = true,
                CanPost = request.RegisterType == RegisterType.Anonymous ? false : true,
                CanUpdate = canUpdate,
                CanDelete = canDelete
            };

            /*await _credentialRepository.AddAsync(new Credential {
                RoleId = role.Id,
                Username = request.UserName,
                Password = PasswordHasher.HashPassword(request.Password),
                Rights = 
            })*/

            return Ok(response);
        }

        #region Deprecated 

        /*// GET: api/{entity}/GetAll
        [HttpGet("GetAll")]
        public override async Task<ActionResult<IEnumerable<Credential>>> GetAll() {
            return Ok(await _repository.GetAllAsync());
        }

        // GET: api/{entity}/GetById
        [HttpGet("GetById")]
        public override async Task<ActionResult<Credential>> Get(TypeId id) {
            var entity = await _repository.GetByIdAsync(id);
            return entity is null ? NotFound() : Ok(entity);
        }

        // POST: api/{entity}/Post
        [HttpPost("Post")]
        public override async Task<ActionResult<Credential>> Create([FromBody] Credential entity) {
            await _repository.AddAsync(entity);
            return CreatedAtAction(nameof(Get), new { id = GetEntityId(entity) }, entity);
        }

        // PUT: api/{entity}/UpdateById
        [HttpPut("UpdateById")]
        public override async Task<IActionResult> Update(TypeId id, [FromBody] Credential entity) {
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

        // GET: api/{entity}/NewIdToAdd
        [HttpGet("NewIdToAdd")]
        public override async Task<TypeId> NewIdToAdd() {
            var entities = await _repository.GetAllAsync();
            if (entities == null)
                return -1; // entities are not found

            // Get All deleted Ids in ascending order
            var deletedIds = entities
                .Where(e => e.isDeleted != null)
                .Select(e => e.Id)
                .OrderBy(id => id)
                .ToList();
            if (deletedIds.Any())
                return deletedIds.First(); // return first free id
            var usedIds = new HashSet<TypeId>(entities.Select(c => c.Id).Where(id => id > 0).OrderBy(id => id));
            for (TypeId i = 1; i < TypeId.MaxValue; i++) {
                if (!usedIds.Contains(i))
                    return i;
            }
            return TypeId.MaxValue; // maybe all seats are reserved
        }

        // Update: api/{entity}/RecoverById
        [HttpGet("RecoverById")]
        public override async Task<IActionResult> RecoverAsync(TypeId id) {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) {
                entity.isDeleted = null;
                entity.WhenChanged = DateTime.Now;

                return Ok("Восстановление прошло успешно");
            }

            return NotFound("Сущность не найдено или уже существует");
        }*/

        #endregion
    }
}