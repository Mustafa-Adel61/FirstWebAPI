using FirstWebAPI.Dtos;
using FirstWebAPI.Model;
using FirstWebAPI.servies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FirstWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        List<string>allowExtention=new List<string> {".jpg",".png" };
        long allowposterSize = 1048576;
        //ApplicationDbContext _context;

        //public MoviesController(ApplicationDbContext context)
        //{
        //    this._context = context;
        //}
        private readonly IMoviesServies _moviesServies;

        public MoviesController(IMoviesServies moviesServies)
        {
            _moviesServies = moviesServies;
        }

        [HttpGet]
        public async Task<IActionResult> GatData()
        {
            var sasa =await _moviesServies.GetMovies();
            return Ok(sasa);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GatDataAsync(int id )
        {
            var movie = await _moviesServies.GetMovies(id);
            if (movie == null)
                return NotFound("this Movies is not found ");
            return Ok(movie);
        }
        [HttpGet("getmovies/{id}")]
        public async Task<IActionResult>GetMoviesFromGenre(int id)
        {
            var movies = await _moviesServies.GetMoviesFromGenre(id);
            return Ok(movies);
        }

        [HttpPost]
        public async Task<IActionResult> AddMovie([FromForm]MoviesDto dto)
        {
            if (dto.Poster == null)
                return BadRequest("Must insert POster");
            if (!allowExtention.Contains(Path.GetExtension(dto.Poster.FileName)))
                return BadRequest("Allow only .jpg and .png Image Extention");
            if(dto.Poster.Length>allowposterSize)
                return BadRequest("Allow only 1MB Poster sizez");

            if (! await _moviesServies.AnyGenres(dto.genreId))
                return BadRequest("The Genre Is not Found in DATABASE");

             var datastream=new MemoryStream();
            await dto.Poster.CopyToAsync(datastream);
            Movie movie = new Movie {

                Title = dto.Title,
                Year = dto.Year,
                genreId = dto.genreId,
                Rate = dto.Rate,
                Storeline = dto.Storeline,
                Poster = datastream.ToArray()

            };
           await _moviesServies.PostMovies(movie);
            return Ok(movie);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult>updateMovies(int id,[FromForm]MoviesDto dto)
        {
            //عملت كدا عشان احل مشكله ان مستخدم Ienumerable في ال InterFace فعمات toList عشان لو عنصر واحد اعمل [0]
            var sasa1 = (await _moviesServies.GetMovies(id)).ToList();
           var sasa2 = sasa1[0];
            if (sasa2 == null)
                return NotFound();
            sasa2.Title = dto.Title;    
            sasa2.Year = dto.Year;
            sasa2.Storeline = dto.Storeline;
            sasa2.Rate = dto.Rate;
            sasa2.genreId = dto.genreId;
            if (dto.Poster != null)
            {
                if (!allowExtention.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                    return BadRequest("Allow only .jpg and .png Image Extention");
                if(dto.Poster.Length>allowposterSize)
                    return BadRequest("Allow only 1MB Poster sizez");
                var datastream= new MemoryStream();
                dto.Poster.CopyTo(datastream);
                sasa2.Poster = datastream.ToArray();
            }

            _moviesServies.UpdateMovies(sasa2);
            return Ok(sasa2);

        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletMovies(int id)
        {
            var sasa1 =(await _moviesServies.GetMovies(id)).ToList();
            var sasa = sasa1[0];
            if (sasa == null)
                return NotFound();
            _moviesServies.DeleteMovies(sasa);         
            return Ok(sasa);    

        }
       
    }
}
