using Blockbuster.Domain.Indexes;
using Blockbuster.Domain.Interfaces;
using Blockbuster.Infra.Elastic;
using Nest;

namespace Blockbuster.Infra.Repositories
{
    public class MoviesRepository : ElasticBaseRepository<IndexMovies>, IMoviesRepository
    {
        public MoviesRepository(IElasticClient elasticClient)
            : base(elasticClient)
        {
        }

        public override string IndexName { get; } = "indexmovies";
    }
}