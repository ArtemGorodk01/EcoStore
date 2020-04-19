using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoStore.EFCore.Entities
{
    public partial class User
    {
        public User()
        {
            Order = new HashSet<Order>();
        }

        [Key]
        public int Id { get; set; }
        public int? Role { get; set; }
        [StringLength(200)]
        public string Login { get; set; }
        [StringLength(20)]
        public string Phone { get; set; }
        [StringLength(1000)]
        public string Password { get; set; }
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
        [Column(TypeName = "datetime")]
        public DateTime? DateOfBirth { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? RegistrationDate { get; set; }
        public int? Gender { get; set; }

        [InverseProperty("UserdNavigation")]
        public virtual ICollection<Order> Order { get; set; }
    }
}
