using AutoMapper;
using Blockbuster.Application.ViewModels.Movies;
using Blockbuster.Domain.Indexes;

namespace Blockbuster.Application.AutoMapper
{
    public class MovieMappingProfile : Profile
    {
        public MovieMappingProfile()
        {
            CreateMap<IndexMovies, MovieViewModel>();

            CreateMap<MovieViewModel, IndexMovies>();
        }
    }
}