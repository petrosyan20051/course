using System.ComponentModel.DataAnnotations;

namespace src.DTO {
    public class LoginPrompt {
        [Required(ErrorMessage = "Имя пользователя обязательно")]
        public required string Login { get; set; }

        [Required(ErrorMessage = "Пароль пользователя обязателен")]
        public required string Password { get; set; }
    }
}
