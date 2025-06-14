
namespace eTickets.Data.Services
{
    public interface IMoviesService : IEntityBaseRepository<Movie>
    {
        Task<Movie> GetMovieByIdAsync(int id);
        Task<Movie> GetMovieByIdAsyncNoTracking(int id);
        Task<IEnumerable<Movie>> GetAllMoviesAsyncNoTracking();
        Task<IEnumerable<Movie>> GetAllMoviesAsyncWithCinemasAndProducersNoTracking();
        //Task<IEnumerable<Movie>> GetAllMoviesAsyncWithCinemasAndProducers();
        Task<NewMovieDropdownsVM> GetNewMovieDropdownsValues();

        Task AddNewMovieAsync(NewMovieVM data);
        Task UpdateMovieAsync(NewMovieVM data);
    }
   
}
