using FirstWebAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace FirstWebAPI.servies
{
    public class MoviesServies : IMoviesServies
    {
        private readonly ApplicationDbContext _context;

        public MoviesServies(ApplicationDbContext context)
        {
            _context = context;
        }

        public Movie DeleteMovies(Movie movie)
        {
            _context.Remove(movie);
            _context.SaveChanges();
            return movie;
        }

        public async Task<IEnumerable<Movie>> GetMoviesFromGenre(int id)
        {
            var movie = await _context.Movies.Where(c=>c.genreId==id).ToListAsync();
            return movie;
        } 
        public async Task<bool> AnyGenres(int GenresId)
        {
            var movie = await _context.Genres.AnyAsync(c=>c.Id==GenresId);
            if (movie)
                return true;
            return false;
        }

        public async Task<IEnumerable<Movie>> GetMovies(int id = 0  )
        {
            var movies =await _context.Movies.Where(c=>c.Id==id||id==0).Include(c => c.genre).ToListAsync();
            return movies;

        }

        //مجرد ماعملت (int id =0)وعملت الكود بتاعه في ال where كدا مش محتاج الكود ده
        //public async Task<Movie> GetMoveFromId(int id)
        //{
        //    var movie = await _context.Movies.FindAsync(id);
        //    return movie;
        //}
        public async Task<Movie> PostMovies(Movie movie)
        {
           await _context.Movies.AddAsync(movie);
           await _context.SaveChangesAsync();
            return movie;
        }

        public Movie UpdateMovies(Movie movie)
        {
            _context.Movies.Update(movie);
           _context.SaveChanges();
            return movie;
        }
    }
}
