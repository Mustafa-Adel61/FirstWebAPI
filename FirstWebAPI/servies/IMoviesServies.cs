using FirstWebAPI.Model;

namespace FirstWebAPI.servies
{
    public interface IMoviesServies
    {
        Task<IEnumerable<Movie>> GetMovies(int id=0);
        //Task<Movie> GetMoveFromId(int id);
        Task<IEnumerable<Movie>> GetMoviesFromGenre(int id);
        Task<Movie> PostMovies(Movie movie);
        Movie UpdateMovies(Movie movie);
        Movie DeleteMovies(Movie movie);
        Task< bool> AnyGenres(int GenresId);

    }
}
