using MongoDB.Driver;

namespace MS.AFORO255.History.Repositories
{
    public interface IMongoBookDBContext
    {
        IMongoCollection<T> GetCollection<T>(string name);
    }
}
