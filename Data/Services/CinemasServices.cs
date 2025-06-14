namespace eTickets.Data.Services
{
    public class CinemasServices : EntityBaseRepository<Cinema>, ICinemasService
    {
        private readonly AppDbContext _context;
        public CinemasServices(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public Task<Cinema> GetCinemaByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
        public Task<Cinema> GetCinemaByIdAsyncNoTracking(int id)
        {
            throw new NotImplementedException();
        }
        public Task<IEnumerable<Cinema>> GetAllCinemasAsyncNoTracking()
        {
            throw new NotImplementedException();
        }
        public Task<IEnumerable<Cinema>> GetAllCinemasAsyncWithMoviesNoTracking()
        {
            throw new NotImplementedException();
        }
    }
}

