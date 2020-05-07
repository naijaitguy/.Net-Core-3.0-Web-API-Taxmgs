using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaxtMgsApi.Model
{
    public partial class TaxApplication
    {
        public TaxApplication()
        {
            TaxRegistrations = new HashSet<TaxRegistration>();
        }

        [Key]
       
        public int TaxAppId { get; set; }
        public string Bvn { get; set; }
        public string Tin { get; set; }
        public string Email { get; set; }
        public string CompanyName { get; set; }
        public string TaxAmount { get; set; }
        public string CompanyAddress { get; set; }
        public string CurrentPositon { get; set; }
        public string StaffId { get; set; }
        public string CompanyPhoneNo { get; set; }
        public string CurrentSalary { get; set; }
        public string CompanyWebsite { get; set; }
        public string PaymentStatus { get; set; }

        public virtual ICollection<TaxRegistration> TaxRegistrations { get; set; }
    }
}
