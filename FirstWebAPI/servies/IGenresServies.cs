using FirstWebAPI.Dtos;
using FirstWebAPI.Model;

namespace FirstWebAPI.servies
{
    public interface IGenresServies
    {
        Task<IEnumerable<Genre>> GetGenres();
        Task<Genre> GetById(int id);
        Task<Genre> postgenres(Genre genre);
        Genre UpdateGenre(Genre genre);
        Genre DeleteGenre(Genre genre);
    }
}
