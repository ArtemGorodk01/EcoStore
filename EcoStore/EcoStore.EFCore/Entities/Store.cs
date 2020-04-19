using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoStore.EFCore.Entities
{
    public partial class Store
    {
        [Key]
        public int Id { get; set; }
        [StringLength(200)]
        public string Address { get; set; }
        [StringLength(20)]
        public string Phone { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
    }
}
