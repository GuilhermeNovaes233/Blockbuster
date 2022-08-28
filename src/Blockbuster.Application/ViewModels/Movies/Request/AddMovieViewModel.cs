using System;

namespace Blockbuster.Application.ViewModels.Movie.Request
{
    public class AddMovieViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string AgeGroup { get; set; }
        public string MovieGenre { get; set; }
        public string ReleaseDate { get; set; }
        public string Director { get; set; }
    }
}