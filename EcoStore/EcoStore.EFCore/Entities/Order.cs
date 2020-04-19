using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoStore.EFCore.Entities
{
    public partial class Order
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? OrderDate { get; set; }
        [Column(TypeName = "money")]
        public decimal? Price { get; set; }
        public bool? Status { get; set; }
        public int? Userd { get; set; }

        [ForeignKey(nameof(Userd))]
        [InverseProperty(nameof(User.Order))]
        public virtual User UserdNavigation { get; set; }
    }
}
