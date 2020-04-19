using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoStore.EFCore.Entities
{
    public partial class Category
    {
        public Category()
        {
            Product = new HashSet<Product>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        public string Title { get; set; }
        public string Description { get; set; }

        [InverseProperty("Category")]
        public virtual ICollection<Product> Product { get; set; }
    }
}
