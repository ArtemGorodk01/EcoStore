using System;

namespace EcoStore.Web.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal? Price { get; set; }
        public bool? Status { get; set; }
        public int? Userd { get; set; }
    }
}
