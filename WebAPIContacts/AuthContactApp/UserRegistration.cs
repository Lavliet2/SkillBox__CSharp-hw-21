using System.ComponentModel.DataAnnotations;


namespace WebAPIContacts.AuthContactApp
{
    public class UserRegistration
    {           
        [Required(ErrorMessage = "Введите логин")]
        [MaxLength(20, ErrorMessage = "Логин не может быть длиннее 20 символов")]
        public string LoginProp { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        //[DataType(DataType.Password), Compare(nameof(Password))]
        [Required(ErrorMessage = "Подтвердите пароль")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли должны совпадать")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Выберите роль")]
        public string Role { get; set; }
    }
}
