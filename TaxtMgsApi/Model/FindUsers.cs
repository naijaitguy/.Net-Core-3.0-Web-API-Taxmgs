using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaxtMgsApi.Model
{
    public class FindUsers
    {

        public string UserName { get; set; }

        public string Email { get; set; }

        public string PhoneNo { get; set; }

        public string BVN { get; set; }

        public string TIN { get; set; }
    }
}
