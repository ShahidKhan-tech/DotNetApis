using Microsoft.AspNetCore.Http;
using ProjectAssnmt.Model.ProjectDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectAssnmt.MovieData
{
    public interface IMovieData
    {
        List<Movie> GetMovies();

        List<Category> GetCategories();
        Movie GetMovie(int id);

        public Movie AddMovie(HttpRequest request);

        void DeleteMovie(Movie movie);

        Movie EditMovie(Movie movie);

        List<Movie> GetMovieByCategory(int id);

        void saveImage(IFormFile imageFile);
    }
}
