namespace eTickets.Data.Services
{
    public interface ICinemasService :IEntityBaseRepository<Cinema>
    {
        Task<Cinema> GetCinemaByIdAsync(int id);
        Task<Cinema> GetCinemaByIdAsyncNoTracking(int id);
        Task<IEnumerable<Cinema>> GetAllCinemasAsyncNoTracking();
        Task<IEnumerable<Cinema>> GetAllCinemasAsyncWithMoviesNoTracking();
    }
    
}

