using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaxtMgsApi.Model
{
    public partial class TaxRegistration
    {
        public TaxRegistration()
        {
            PaymentRecords = new HashSet<PaymentRecord>();
        }
        [Key]
        public int UserId { get; set; }
        public int? TaxAppId { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
        public string Date { get; set; }

        public virtual TaxApplication TaxApp { get; set; }
        public virtual ICollection<PaymentRecord> PaymentRecords { get; set; }
    }
}
