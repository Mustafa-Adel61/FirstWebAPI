using FirstWebAPI.Dtos;
using FirstWebAPI.Model;
using FirstWebAPI.servies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirstWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        

        //private  readonly ApplicationDbContext _context ;
        //public GenresController(ApplicationDbContext context)
        //{
        //    _context = context;
        //}
        private readonly IGenresServies _genresServies;

        public GenresController(IGenresServies genresServies)
        {
            _genresServies = genresServies;
        }

        [HttpGet]
        public async Task<IActionResult> GetGenres()
        {
            var sasa =await _genresServies.GetGenres();

            return Ok(sasa);
        }
        [HttpPost]
        public async Task<IActionResult> postgenres(createGenresDto dto)
        {
            var genre=new Genre {Name=dto.Name};
            await _genresServies.postgenres(genre);
            return Ok(genre);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGenre(int id,createGenresDto dto)
        {
            // var genre=await _context.Genres.SingleOrDefaultAsync(c=>c.Id==id);
            var genre =await _genresServies.GetById(id);
            if (genre == null)
            {
                return NotFound($"the Genre with id : {id} is Not Found in Database");
            }
            else
            {
                genre.Name = dto.Name;
                var ss=_genresServies.UpdateGenre(genre);
                //await _context.SaveChangesAsync();
                return Ok(ss);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult>  DeleteGenre(int id) {

            // var genre = await _context.Genres.SingleOrDefaultAsync(c => c.Id == id);
            var genre =await _genresServies.GetById(id);
            if (genre == null)
                return NotFound($"The Genre with ID : {id} Not Found ");
          //  _context.Genres.Remove(genre);
          _genresServies.DeleteGenre(genre);
            //await _context.SaveChangesAsync();
            return Ok($"the Genre with ID : {id} is Deleted ");
        
        }
       

    }
}
