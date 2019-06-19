using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalCabinet.DataBase.Models
{
    public class Purchase : BaseEntity
    {
        public Guid? LoyaltyEntity_id { get; set; } = null;
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public string PointOfSale { get; set; }
        public string Card { get; set; }
        public decimal Amount { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal CashPaidAmount { get; set; }
        public decimal GiftCardPaidAmount { get; set; }
        public decimal BonusesPaidAmount { get; set; }
        public decimal TotalPaidAmount { get; set; }
        public Guid? ParentPurchaseId { get; set; }
        public string CardAccount { get; set; }
        public decimal? BonusesAccrued { get; set; }
        public List<Product> Products { get; set; }
    }
}
