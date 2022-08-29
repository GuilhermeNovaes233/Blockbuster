using System;

namespace Blockbuster.Domain.Indexes
{
    public class IndexMovies : ElasticBaseIndex
    {
        public IndexMovies(string name, string description, string ageGroup, string movieGenre, string releaseDate, string director)
        {
            Name = name;
            Description = description;
            AgeGroup = ageGroup;
            MovieGenre = movieGenre;
            ReleaseDate = releaseDate;
            Director = director;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string AgeGroup { get; set; }
        public string MovieGenre { get; set; }
        public string ReleaseDate { get; set; }
        public string Director { get; set; }
    }
}