using Nest;

namespace Blockbuster.Domain.Elastic
{
    public interface IElasticBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<bool> InsertManyAsync(IList<T> tList);
        Task<IEnumerable<T>> SearchAsync(Func<QueryContainerDescriptor<T>, QueryContainer> request);
        Task<bool> CreateIndexAsync();
    }
}