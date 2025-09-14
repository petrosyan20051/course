using db.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace db.Models {
    public class RegisterPrompt {
        [Required(ErrorMessage = "Имя пользователя обязательно")]
        public required string UserName { get; set; }

        [Required(ErrorMessage = "Пароль пользователя обязателен")]
        public required string Password { get; set; }

        [Required(ErrorMessage = "Тип регистрации обязателен")]
        public IInformation.RegisterType RegisterType { get; set; }

        [Required(ErrorMessage = "Тип регистрируемого пользователя обязателен")]
        public IInformation.UserRights RegisterRights { get; set; }
    }
}
