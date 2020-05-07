using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaxtMgsApi.Model
{
    public partial class ContactRecord
    {

        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Comment { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
    }
}
