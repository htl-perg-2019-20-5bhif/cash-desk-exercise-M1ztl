using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CashDesk.Model
{
    class Membership : IMembership
    {
        [Required]
        public IMember Member { get; set; }
        [Required]
        public DateTime Begin { get; set; }

        public DateTime End { get; set; }
    }
}
