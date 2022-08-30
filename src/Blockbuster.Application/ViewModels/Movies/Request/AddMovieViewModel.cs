using Blockbuster.Application.ViewModels.Movies;
using System;
using System.ComponentModel.DataAnnotations;

namespace Blockbuster.Application.ViewModels.Movie.Request
{
    public class AddMovieViewModel
    {
        public AddMovieViewModel()
        {
            Movies = new List<MovieViewModel>();
        }

        [Required]
        public List<MovieViewModel> Movies { get; set; }
    }
}