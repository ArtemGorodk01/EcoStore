using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoStore.EFCore.Entities
{
    public partial class Product
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "money")]
        public decimal? Price { get; set; }
        [StringLength(200)]
        public string Title { get; set; }
        public int? CategoryId { get; set; }
        public int? GuaranteeMonth { get; set; }
        public string ImageDataBase64 { get; set; }
        public string Description { get; set; }

        [ForeignKey(nameof(CategoryId))]
        [InverseProperty("Product")]
        public virtual Category Category { get; set; }
    }
}
