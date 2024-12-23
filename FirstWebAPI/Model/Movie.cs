namespace FirstWebAPI.Model
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int  Year { get; set; }
        public double  Rate { get; set; }
        public string  Storeline { get; set; }
        public byte[]  Poster { get; set; }
        public int genreId { get; set; }
        public Genre genre { get; set; }
    }
}
