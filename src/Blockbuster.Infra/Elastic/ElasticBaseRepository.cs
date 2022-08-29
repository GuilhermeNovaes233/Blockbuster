using Blockbuster.Domain.Elastic;
using Blockbuster.Domain.Indexes;
using Nest;

namespace Blockbuster.Infra.Elastic
{
    public abstract class ElasticBaseRepository<T> : IElasticBaseRepository<T> where T : ElasticBaseIndex
    {
        public abstract string IndexName { get; }

        private readonly IElasticClient _elasticClient;
        protected ElasticBaseRepository(
            IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public async Task<bool> InsertManyAsync(IList<T> tList)
        {
            await CreateIndexAsync();
            var response = await _elasticClient.IndexManyAsync(tList, IndexName);

            if (!response.IsValid)
                throw new Exception(response.ServerError?.ToString(), response.OriginalException);

            return true;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var search = new SearchDescriptor<T>(IndexName).MatchAll();
            var response = await _elasticClient.SearchAsync<T>(search);

            if (!response.IsValid)
                throw new Exception(response.ServerError?.ToString(), response.OriginalException);

            return response.Hits.Select(hit => hit.Source).ToList();
        }

        public async Task<bool> CreateIndexAsync()
        {
            if (!(await _elasticClient.Indices.ExistsAsync(IndexName)).Exists)
            {
                await _elasticClient.Indices.CreateAsync(IndexName, c =>
                {
                    c.Map<T>(p => p.AutoMap());
                    return c;
                });
            }
            return true;
        }
    }
}