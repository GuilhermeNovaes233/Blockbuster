using AutoMapper;
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
        private readonly IMapper _mapper;

        public MoviesAppService(
            ILogger<MoviesAppService> logger,
            IMapper mapper,
            IMoviesRepository moviesRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _moviesRepository = moviesRepository;
        }

        public async Task<Either<ErrorResponseViewModel, SuccessResponseViewModel>> AddMovieAsync(AddMovieViewModel requestModel)
        {
            try
            {
                if (requestModel.Movies == null || !requestModel.Movies.Any())
                    return new Either<ErrorResponseViewModel, SuccessResponseViewModel>().NotFound(new ErrorResponseViewModel("Filmes não encontrados"));

                var vm = _mapper.Map<List<IndexMovies>>(requestModel.Movies);

                await _moviesRepository.InsertManyAsync(vm);

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

                var vm = _mapper.Map<List<MovieViewModel>>(responseOnElastic);

                return new Either<ErrorResponseViewModel, MoviesResponseViewModel>().Ok(new MoviesResponseViewModel(vm));
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

                var vm = _mapper.Map<List<MovieViewModel>>(responseOnElastic);

                return new Either<ErrorResponseViewModel, MoviesResponseViewModel>().Ok(new MoviesResponseViewModel(vm));
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Erro ao tentar buscar filme: {ex.Message}");

                return new Either<ErrorResponseViewModel, MoviesResponseViewModel>()
                    .CustomError(new ErrorResponseViewModel(ex.Message), (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<Either<ErrorResponseViewModel, MoviesResponseViewModel>> GetByNameWithWildcardAsync(string name)
        {
            try
            {
                var query = new QueryContainerDescriptor<IndexMovies>().Wildcard(w => w.Field(f => f.Name).Value($"*{name}*").CaseInsensitive());

                var responseOnElastic = await _moviesRepository.SearchAsync(_ => query);
                if (responseOnElastic == null)
                    return new Either<ErrorResponseViewModel, MoviesResponseViewModel>().NotFound(new ErrorResponseViewModel("Filmes não encontrados"));

                var vm = _mapper.Map<List<MovieViewModel>>(responseOnElastic);

                return new Either<ErrorResponseViewModel, MoviesResponseViewModel>().Ok(new MoviesResponseViewModel(vm));
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Erro ao tentar buscar filme: {ex.Message}");

                return new Either<ErrorResponseViewModel, MoviesResponseViewModel>()
                    .CustomError(new ErrorResponseViewModel(ex.Message), (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<Either<ErrorResponseViewModel, MoviesResponseViewModel>> GetByMovieGenreAsync(string movieGenre)
        {
            try
            {
                var query = new QueryContainerDescriptor<IndexMovies>().Match(p => p.Field(f => f.MovieGenre).Query(movieGenre).Operator(Operator.And));

                var responseOnElastic = await _moviesRepository.SearchAsync(_ => query);
                if (responseOnElastic == null)
                    return new Either<ErrorResponseViewModel, MoviesResponseViewModel>().NotFound(new ErrorResponseViewModel("Filmes não encontrados"));

                var vm = _mapper.Map<List<MovieViewModel>>(responseOnElastic);

                return new Either<ErrorResponseViewModel, MoviesResponseViewModel>().Ok(new MoviesResponseViewModel(vm));
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