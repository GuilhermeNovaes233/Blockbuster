using Blockbuster.Application.ViewModels;
using Blockbuster.Application.ViewModels.Movie.Request;
using Blockbuster.Application.ViewModels.Movies.Response;
using Blockbuster.Domain.Models;

namespace Blockbuster.Application.Interfaces
{
    public interface IMoviesAppService
    {
        Task<Either<ErrorResponseViewModel, MoviesResponseViewModel>> GetAllAsync();
        Task<Either<ErrorResponseViewModel, MoviesResponseViewModel>> GetMoviesByNameAsync(string name);
        Task<Either<ErrorResponseViewModel, MoviesResponseViewModel>> GetByNameWithWildcard(string name);
        Task<Either<ErrorResponseViewModel, SuccessResponseViewModel>> AddMovieAsync(AddMovieViewModel requestModel);
    }
}