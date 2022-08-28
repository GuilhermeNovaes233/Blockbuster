using Blockbuster.Application.Interfaces;
using Blockbuster.Application.ViewModels;
using Blockbuster.Application.ViewModels.Movie.Request;
using Blockbuster.Domain.Interfaces;
using Blockbuster.Domain.Models;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Blockbuster.Application.AppServices
{
    public class MoviesAppService : IMoviesAppService
    {
        private readonly ILogger<MoviesAppService> _logger;
        private readonly IMoviesRepository _moviesRepository;

        public MoviesAppService(ILogger<MoviesAppService> logger, IMoviesRepository moviesRepository)
        {
            _logger = logger;
            _moviesRepository = moviesRepository;
        }

        public async Task<Either<ErrorResponseViewModel, SuccessResponseViewModel>> AddMovieAsync(AddMovieViewModel requestModel)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch(Exception ex)
            {
                _logger.LogWarning($"Erro ao tentar adicionar um filme: {ex.Message}");

                return new Either<ErrorResponseViewModel, SuccessResponseViewModel>()
                    .CustomError(new ErrorResponseViewModel(ex.Message), (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}