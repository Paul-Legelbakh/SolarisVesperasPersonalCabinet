using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalCabinet.DataBase.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPaidAmount { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal Quantity { get; set; }
    }
}
