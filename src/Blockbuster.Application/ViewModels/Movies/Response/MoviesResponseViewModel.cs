using System;

namespace Blockbuster.Application.ViewModels.Movies.Response
{
    public class MoviesResponseViewModel
    {
        public MoviesResponseViewModel(List<MovieViewModel> movies)
        {
            Movies = movies;
        }

        public List<MovieViewModel> Movies { get; set; }
    }
}