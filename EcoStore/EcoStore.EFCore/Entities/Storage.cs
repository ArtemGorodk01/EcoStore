using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoStore.EFCore.Entities
{
    public partial class Storage
    {
        public int? ProductId { get; set; }
        public int? StoreId { get; set; }
        public int? VendorId { get; set; }
        public int? Amount { get; set; }

        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; }
        [ForeignKey(nameof(StoreId))]
        public virtual Store Store { get; set; }
        [ForeignKey(nameof(VendorId))]
        public virtual Vendor Vendor { get; set; }
    }
}
