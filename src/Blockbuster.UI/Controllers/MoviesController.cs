using Blockbuster.Application.Interfaces;
using Blockbuster.Application.ViewModels.Movie.Request;
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

        [HttpPost]
        public async Task<IActionResult> PostMovieAsync([FromBody] AddMovieViewModel request)
            =>  Return(await _moviesAppService.AddMovieAsync(request));

        [HttpGet]
        public async Task<IActionResult> GetMoviesAsync()
            => Return(await _moviesAppService.GetAllAsync());
    }
}
