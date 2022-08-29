using Blockbuster.Domain.Elastic;
using Blockbuster.Domain.Indexes;

namespace Blockbuster.Domain.Interfaces
{
    public interface IMoviesRepository : IElasticBaseRepository<IndexMovies>
    {
    }
}