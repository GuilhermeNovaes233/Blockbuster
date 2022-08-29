using Nest;

namespace Blockbuster.Domain.Elastic
{
    public interface IElasticBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<bool> InsertManyAsync(IList<T> tList);
        Task<bool> CreateIndexAsync();
    }
}