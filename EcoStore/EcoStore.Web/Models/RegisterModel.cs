using EcoStore.Web.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace EcoStore.Web.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Не указан логин")]
        [RegularExpression(@"^([a-z0-9_-]+\.)*[a-z0-9_-]+@[a-z0-9_-]+(\.[a-z0-9_-]+)*\.[a-z]{2,6}$",
         ErrorMessage = "Неправильный логин")]
        [StringLength(200)]
        public string Login { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Необходимо подтверждение пароля")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Не указан телефон")]
        [RegularExpression(@"^[0-9]{9}$",
         ErrorMessage = "Укажите телефон в формате XXNNNNNNN, где XX - код, NNNNNNN - номер телефона")]
        [StringLength(20)]
        public string Phone { get; set; }

        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }

        [StringLength(100)]
        public string Country { get; set; }

        [StringLength(100)]
        public string Region { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public DateTime? RegistrationDate { get; set; }

        public Gender Gender { get; set; }
    }
}
