using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Application.CoinDtos
{
    public class PortfolioHistoryDto
    {
        public DateTime TimeStamp { get; set; }
        public decimal TotalValue { get; set; }
    }
}
