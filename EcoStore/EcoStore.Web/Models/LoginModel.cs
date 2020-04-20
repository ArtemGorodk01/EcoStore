using System.ComponentModel.DataAnnotations;

namespace EcoStore.Web.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Не указан логин")]
        [StringLength(200)]
        public string Login { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
