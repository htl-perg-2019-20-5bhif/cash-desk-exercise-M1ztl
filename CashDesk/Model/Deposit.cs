using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CashDesk.Model
{
    class Deposit : IDeposit
    {
        public int DepositId { get; set; }
        [Required]
        public IMembership Membership { get; set; }
        [Required]
        public decimal Amount { get; set; }
    }
}
