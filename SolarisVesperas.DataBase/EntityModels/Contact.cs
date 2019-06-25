using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalCabinet.DataBase.Models
{
    public class Contact : BaseEntity
    {
        public Guid? LoyaltyEntity_id { get; set; } = null;
        public string Email { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime? BirthDate { get; set; }
        public string MobilePhone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string SecondName { get; set; } = string.Empty;
        public bool Confirmed { get; set; } = false;
        public string Password { get; set; }
        public List<Purchase> Purchases { get; set; } = null;
    }
}
