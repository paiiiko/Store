using System.ComponentModel.DataAnnotations;

namespace Store.WebUI.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Введите электронную почту")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        [StringLength(50, ErrorMessage = "Поле {0} должно иметь минимум {2} и максимум {1} символов.", MinimumLength = 6)]
        public string Password { get; set; }
    }
}
