//using CookBook.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using System.Collections;

//namespace CookBook.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class MovieController : ControllerBase
//    {
//        private readonly MovieContext _dbContext;

//        public MovieController(MovieContext dbContext)
//        {
//            _dbContext = dbContext;
//        }

//        // GET: api/Movies
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
//        {
//            if (_dbContext.Movies == null)
//            {
//                return NotFound();
//            }
//            return await _dbContext.Movies.ToListAsync();
//        }

//        // GET: api/Movies/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<Movie>> GetMovie(int id)
//        {
//            if (_dbContext.Movies == null) 
//            {
//                return NotFound();
//            }
//            var movie = await _dbContext.Movies.FindAsync(id);

//            if (movie == null)
//            {
//                return NotFound();
//            }
//            return movie;
//        }

//        // POST: api/Movies
//        [HttpPost]
//        public async Task<ActionResult<Movie>> PostMovie(Movie movie)
//        {
//            _dbContext.Movies.Add(movie);
//            await _dbContext.SaveChangesAsync();

//            return CreatedAtAction(nameof(GetMovie), new {id = movie.Id}, movie);
//        }
//    }
//}
