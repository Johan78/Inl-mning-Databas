using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InlämningDatabas1
{
    public class Contact
    {
        public int ContactID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        [Column(TypeName="date")]
        public DateTime DOB { get; set; }


        public void Clear()
        {

        }
    }

    
}
