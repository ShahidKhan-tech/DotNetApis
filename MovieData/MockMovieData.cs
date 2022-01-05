using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectAssnmt.Controllers;
using ProjectAssnmt.Model;
using ProjectAssnmt.Model.ProjectDbContext;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectAssnmt.MovieData
{
    public class MockMovieData : IMovieData
    {
       
        private ProjectDbContext pd;
        private object _env;

        public MockMovieData()
        {
            pd = new ProjectDbContext();
        }


        public Movie AddMovie(HttpRequest request)
        {
            Movie movie = new Movie();
            movie.MovieId = Convert.ToInt32(request.Form["MovieId"]);
            movie.Title = request.Form["Title"];
            movie.Price = Convert.ToInt32(request.Form["Price"]);

            movie.Quantity = Convert.ToInt32(request.Form["Quantity"]);
            movie.Photo = request.Form.Files[0].FileName;
            
            movie.CategoryId = Convert.ToInt32(request.Form["CategoryId"]);


            pd.Movies.Add(movie);
            saveImage(request.Form.Files[0]);

            pd.SaveChanges();
            return movie;
        }

        public void DeleteMovie(Movie movie)
        {
            pd.Movies.Remove(movie);
            pd.SaveChanges();
        }

        public Movie EditMovie(Movie movie)
        {
            throw new NotImplementedException();
        }

        public Movie GetMovie(int id)
        {
            return pd.Movies.SingleOrDefault(x => x.MovieId==id);
        }
        public List<Movie> GetMovieByCategory(int id)
        {
           var movies= pd.Movies.Where(x => x.CategoryId == id);
            return movies.ToList();
        }

        public List<Movie> GetMovies()
        {
            return pd.Movies.ToList();
        }
        // List<Category> GetCategories()
        //{
        //    return pd.Categories.ToList();
        //}

        List<Category> IMovieData.GetCategories()
        {
            return pd.Categories.ToList();
        }

        public void saveImage(IFormFile imageFile)
        {
            //string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
            string path = $"{Environment.CurrentDirectory}\\Photos\\{imageFile.FileName}";
            //imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);
            //var imagePath = Path.Combine(_env.ContentRootPath, "Photos", imageName);
            using (var fileStream = new FileStream(path, FileMode.Create,FileAccess.Write))
            {
                imageFile.CopyTo(fileStream);
                fileStream.Close();
            }
        
        }

    }
}
