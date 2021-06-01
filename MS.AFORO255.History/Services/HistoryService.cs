using MongoDB.Driver;
using MS.AFORO255.History.DTOs;
using MS.AFORO255.History.Models;
using MS.AFORO255.History.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MS.AFORO255.History.Services
{
    public class HistoryService : IHistoryService
    {
        private readonly IMongoBookDBContext _context;
        protected IMongoCollection<HistoryTransaction> _dbCollection;
        public HistoryService(IMongoBookDBContext context)
        {
            _context = context;
            _dbCollection = _context.GetCollection<HistoryTransaction>(typeof(HistoryTransaction).Name);
        }

        public async Task<bool> Add(HistoryTransaction historyTransaction)
        {
            await _dbCollection.InsertOneAsync(historyTransaction);
            return true;
        }

        public async Task<IEnumerable<HistoryResponse>> GetAll()
        {
            var data = await _dbCollection.Find(_ => true).ToListAsync();
            var response = new List<HistoryResponse>();
            foreach (var item in data)
            {
                response.Add(new HistoryResponse()
                {
                    AccountId = item.AccountId,
                    Amount = item.Amount,
                    CreationDate = item.CreationDate,
                    IdTransaction = item.IdTransaction,
                    Type = item.Type
                });
            }
            return response;
        }
    }
}
