using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoStore.Web.Models
{
    public class Product
    {
        public int Id { get; set; }

        public decimal Price { get; set; }

        public string Title { get; set; }

        public int CategoryId { get; set; }

        public int GuaranteeMonth { get; set; }

        public byte[] ImageData { get; set; }
    }
}
