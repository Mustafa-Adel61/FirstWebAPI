using FirstWebAPI.Dtos;
using FirstWebAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace FirstWebAPI.servies
{
    public class GenresServies : IGenresServies
    {
        private readonly ApplicationDbContext _context;

        public GenresServies(ApplicationDbContext context)
        {
            _context = context;
        }

        public Genre DeleteGenre(Genre genre)
        {
            _context.Genres.Remove(genre);
            _context.SaveChanges();
            return genre;
        }

        public async Task<Genre>  GetById(int id)
        {
            var sasa =await _context.Genres.FindAsync(id);
            return sasa;
        }

        public async Task<IEnumerable<Genre>> GetGenres()
        {
            var sasa = await _context.Genres.ToListAsync();
            return sasa;

        }

        public async Task<Genre> postgenres(Genre genre)
        {
            await _context.Genres.AddAsync(genre);
            _context.SaveChanges();
            return genre;
        }

        public  Genre UpdateGenre(Genre genre)
        {
            _context.Update(genre);
            _context.SaveChanges();

            return genre;

        }

        
    }
}
