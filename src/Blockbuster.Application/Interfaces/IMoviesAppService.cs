using Blockbuster.Application.ViewModels;
using Blockbuster.Application.ViewModels.Movie.Request;
using Blockbuster.Domain.Models;

namespace Blockbuster.Application.Interfaces
{
    public interface IMoviesAppService
    {
        Task<Either<ErrorResponseViewModel, SuccessResponseViewModel>> AddMovieAsync(AddMovieViewModel requestModel);
    }
}