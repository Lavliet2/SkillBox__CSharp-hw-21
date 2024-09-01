using System.ComponentModel.DataAnnotations;

namespace WebAPIContacts.AuthContactApp
{
    
    public class UserLogin
    {
        [Required(ErrorMessage = "Логин обязателен для заполнения")]
        [MaxLength(20)]
        public string LoginProp { get; set; }

        [Required(ErrorMessage = "Пароль обязателен для заполнения")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
}
