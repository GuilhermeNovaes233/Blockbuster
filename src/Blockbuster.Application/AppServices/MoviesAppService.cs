using Blockbuster.Application.Interfaces;
using Blockbuster.Application.ViewModels;
using Blockbuster.Application.ViewModels.Movie.Request;
using Blockbuster.Application.ViewModels.Movies;
using Blockbuster.Application.ViewModels.Movies.Response;
using Blockbuster.Domain.Indexes;
using Blockbuster.Domain.Interfaces;
using Blockbuster.Domain.Models;
using Microsoft.Extensions.Logging;
using Nest;
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
                var list = new List<IndexMovies>() {
                    new IndexMovies(
                        requestModel.Name,
                        requestModel.Description,
                        requestModel.AgeGroup,
                        requestModel.MovieGenre,
                        requestModel.ReleaseDate,
                        requestModel.Director)
                };

                await _moviesRepository.InsertManyAsync(list);

                return new Either<ErrorResponseViewModel, SuccessResponseViewModel>().Ok(new SuccessResponseViewModel("Sucesso ao cadastrar filmes"));
            }
            catch(Exception ex)
            {
                _logger.LogWarning($"Erro ao tentar adicionar um filme: {ex.Message}");

                return new Either<ErrorResponseViewModel, SuccessResponseViewModel>()
                    .CustomError(new ErrorResponseViewModel(ex.Message), (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<Either<ErrorResponseViewModel, MoviesResponseViewModel>> GetAllAsync()
        {
            try
            {
                var responseOnElastic = await _moviesRepository.GetAllAsync();
                if (responseOnElastic == null)
                    return new Either<ErrorResponseViewModel, MoviesResponseViewModel>().NotFound(new ErrorResponseViewModel("Filmes não encontrados"));

                var response = new MoviesResponseViewModel();
                foreach (var item in responseOnElastic)
                {
                    var movie = new MovieViewModel(item.Name, item.Description, item.AgeGroup, item.MovieGenre, item.ReleaseDate, item.Director);

                    response.Movies.Add(movie);
                }

                return new Either<ErrorResponseViewModel, MoviesResponseViewModel>().Ok(response);
            }
            catch(Exception ex)
            {
                _logger.LogWarning($"Erro ao tentar buscar filmes: {ex.Message}");

                return new Either<ErrorResponseViewModel, MoviesResponseViewModel>()
                    .CustomError(new ErrorResponseViewModel(ex.Message), (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<Either<ErrorResponseViewModel, MoviesResponseViewModel>> GetMoviesByNameAsync(string name)
        {
            try
            {
                var query = new QueryContainerDescriptor<IndexMovies>().Match(p => p.Field(f => f.Name).Query(name).Operator(Operator.And));

                var responseOnElastic = await _moviesRepository.SearchAsync(_ => query);
                if (responseOnElastic == null)
                    return new Either<ErrorResponseViewModel, MoviesResponseViewModel>().NotFound(new ErrorResponseViewModel("Filmes não encontrados"));

                var response = new MoviesResponseViewModel();
                foreach (var item in responseOnElastic)
                {
                    var movie = new MovieViewModel(item.Name, item.Description, item.AgeGroup, item.MovieGenre, item.ReleaseDate, item.Director);

                    response.Movies.Add(movie);
                }

                return new Either<ErrorResponseViewModel, MoviesResponseViewModel>().Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Erro ao tentar buscar filme: {ex.Message}");

                return new Either<ErrorResponseViewModel, MoviesResponseViewModel>()
                    .CustomError(new ErrorResponseViewModel(ex.Message), (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}