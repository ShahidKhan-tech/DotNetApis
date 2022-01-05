using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectAssnmt.Model.ProjectDbContext;
using ProjectAssnmt.MovieData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectAssnmt.Controllers
{
    
    [ApiController]
    [EnableCors("angularProject")]
    public class MovieController : ControllerBase
    {
        IWebHostEnvironment _env;
        private IMovieData moviesdata;
        public MovieController(IMovieData moviedata,IWebHostEnvironment env)
        {
            moviesdata = moviedata;
            _env = env;
        }
        [HttpGet]
        [Route("api/[controller]")]
        public IActionResult getMovies()
        {
            return Ok(moviesdata.GetMovies());
        }
        [HttpGet]
        [Route("api/Category")]
        public IActionResult getCategories()
        {
            return Ok(moviesdata.GetCategories());
        }
        [HttpGet]
        [Route("api/[controller]/{id}")]
        public IActionResult getMovie(int id)
        {
            var movie = moviesdata.GetMovie(id);
            if (movie != null)
            {
                return Ok(movie);
            }
            return NotFound($"Movie with {id} does not exist");
        }

        [HttpGet]
        [Route("api/Category/{id}")]
        public IActionResult getCategory(int id)
        {
            var category = moviesdata.GetMovieByCategory(id);
            if (category != null)
            {
                return Ok(category);
            }
            return NotFound($"Category with {id} does not exist");
        }

        [HttpPost]
        [Route("api/[controller]")]
        public IActionResult AddMovie()
        {
            
            moviesdata.AddMovie(Request);
            return Ok("movie created");
           // return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + movie.MovieId, movie);
        }
        [HttpDelete]
        [Route("api/[controller]/{id}")]
        public IActionResult DeleteMovie(int id)
        {
            var movie = moviesdata.GetMovie(id);
            if (movie != null)
            {
                moviesdata.DeleteMovie(movie);

                return Ok("movie deleted");
            }
            return NotFound($"movie with id {id} does not exist");
        }

        [HttpPost]
        [Route("api/PhotoUpload")]
        public IActionResult Save()
        {
            var httpRequest = Request.Form;
            var postedFile = httpRequest.Files[0];
            string filename = postedFile.FileName;
            var physicalPath = _env.ContentRootPath + "/Photos/" + filename;
            using (var stream=new FileStream(physicalPath, FileMode.Create))
            {
                postedFile.CopyTo(stream);
            }
            return Ok(filename);
        }

      
       
    }
}
