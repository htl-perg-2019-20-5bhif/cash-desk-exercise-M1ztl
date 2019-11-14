using System;
using System.Collections.Generic;
using System.Text;

namespace CashDesk.Model
{
    class DepositStatistic : IDepositStatistics
    {
        public IMember Member { get; set; }

        public int Year { get; set; }

        public decimal TotalAmount { get; set; }
    }
}
