using System;

namespace Blockbuster.Application.ViewModels.Movies.Response
{
    public class MoviesResponseViewModel
    {
        public MoviesResponseViewModel()
        {
            Movies = new List<MovieViewModel>();
        }

        public List<MovieViewModel> Movies { get; set; }
    }
}