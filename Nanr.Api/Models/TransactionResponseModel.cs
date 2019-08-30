using Nanr.Api.Managers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nanr.Api.Models
{
    public class TransactionResponseModel
    {
        public TransactionResponseModel(IEnumerable<ClickTransaction> clicksRecieved, IEnumerable<ClickTransaction> clicksSent, IEnumerable<WithdrawTransaction> withdraws)
        {
            ClicksRecieved = clicksRecieved;
            ClicksSent = clicksSent;
            Withdraws = withdraws;
        }
        public IEnumerable<ClickTransaction> ClicksRecieved { get; }
        public IEnumerable<ClickTransaction> ClicksSent { get; }
        public IEnumerable<WithdrawTransaction> Withdraws { get; }
    }
}
