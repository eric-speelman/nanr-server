using Nanr.Api.Managers.Models;
using Nanr.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nanr.Api.Managers
{
    public interface ITransactionManager
    {
        Task<IEnumerable<ClickTransaction>> GetClickTransactionsRecieved(User user);
        Task<IEnumerable<WithdrawTransaction>> GetWithdrawTransaction(User user);
        Task<IEnumerable<ClickTransaction>> GetClickTransactionsSent(User user);
        Task<(int sent, int recieved)> RecentTransactionCount(User user, int? length = null);
    }
}
