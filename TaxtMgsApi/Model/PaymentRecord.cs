using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaxtMgsApi.Model
{
    public partial class PaymentRecord
    {

        [Key]
        public int PaymentId { get; set; }
        public int? UserId { get; set; }
        public string CardNumber { get; set; }
        public string ExpiredDate { get; set; }
        public string Cv2 { get; set; }
        public string Pin { get; set; }
        public string Date { get; set; }
        public int TaxAppId { get; set; }

        public virtual TaxRegistration User { get; set; }
    }
}
