using MS.AFORO255.History.DTOs;
using MS.AFORO255.History.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MS.AFORO255.History.Services
{
    public interface IHistoryService
    {
        Task<IEnumerable<HistoryResponse>> GetAll();

        Task<bool> Add(HistoryTransaction historyTransaction);
    }
}
