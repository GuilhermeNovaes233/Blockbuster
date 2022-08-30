using Blockbuster.Application.Interfaces;
using Blockbuster.Application.ViewModels;
using Blockbuster.Application.ViewModels.Movie.Request;
using Blockbuster.Application.ViewModels.Movies.Response;
using Microsoft.AspNetCore.Mvc;

namespace Blockbuster.UI.Controllers
{
    [Route("api/[controller]")]
    public class MoviesController : BaseController
    {
        private readonly IMoviesAppService _moviesAppService;
        public MoviesController(IMoviesAppService moviesAppService)
        {
            _moviesAppService = moviesAppService;
        }

        // <summary>
        /// Api para cadastrar os filmes
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(SuccessResponseViewModel), 200)]
        [ProducesResponseType(typeof(ErrorResponseViewModel), 404)]
        [ProducesResponseType(typeof(ErrorResponseViewModel), 500)]
        [ProducesResponseType(typeof(ErrorResponseViewModel), 401)]
        [HttpPost]
        public async Task<IActionResult> PostMovieAsync([FromBody] AddMovieViewModel request)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            return Return(await _moviesAppService.AddMovieAsync(request));
        }

        /// <summary>
        ///  Api que retorna os filmes cadastrados
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(MoviesResponseViewModel), 200)]
        [ProducesResponseType(typeof(ErrorResponseViewModel), 404)]
        [ProducesResponseType(typeof(ErrorResponseViewModel), 500)]
        [ProducesResponseType(typeof(ErrorResponseViewModel), 401)]
        [HttpGet("list")]
        public async Task<IActionResult> GetMoviesAsync()
            => Return(await _moviesAppService.GetAllAsync());

        /// <summary>
        ///  Api que retorna os filmes cadastrados pelo nome
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(MoviesResponseViewModel), 200)]
        [ProducesResponseType(typeof(ErrorResponseViewModel), 404)]
        [ProducesResponseType(typeof(ErrorResponseViewModel), 500)]
        [ProducesResponseType(typeof(ErrorResponseViewModel), 401)]
        [HttpGet("name-match")]
        public async Task<IActionResult> GetMoviesByNameAsync([FromQuery] string name)
            => Return(await _moviesAppService.GetMoviesByNameAsync(name));


        /// <summary>
        ///  Api que retorna os filmes cadastrados pelo texto digitado
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(MoviesResponseViewModel), 200)]
        [ProducesResponseType(typeof(ErrorResponseViewModel), 404)]
        [ProducesResponseType(typeof(ErrorResponseViewModel), 500)]
        [ProducesResponseType(typeof(ErrorResponseViewModel), 401)]
        [HttpGet("name-wildcard")]
        public async Task<IActionResult> GetByNameWithWildcard([FromQuery] string name)
            => Return(await _moviesAppService.GetByNameWithWildcard(name));
    }
}
