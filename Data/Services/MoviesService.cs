
namespace eTickets.Data.Services
{
    public class MoviesService : EntityBaseRepository<Movie>, IMoviesService
    {
        private readonly AppDbContext _context;
        public MoviesService(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            var movieDetails = await _context.Movies
                .Include(m => m.Cinema)
                .Include(m => m.Producer)
                .Include(am => am.Actors_Movies).ThenInclude(a => a.Actor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movieDetails == null)
            {
                throw new InvalidOperationException($"Movie with ID {id} does not exist.");
            }
            return   movieDetails; 


        }
        public Task<Movie> GetMovieByIdAsyncNoTracking(int id)
        {
            throw new NotImplementedException();
        }
        public Task<IEnumerable<Movie>> GetAllMoviesAsyncNoTracking()
        {
            throw new NotImplementedException();
        }
        public Task<IEnumerable<Movie>> GetAllMoviesAsyncWithCinemasAndProducersNoTracking()
        {
            throw new NotImplementedException();
        }

        public async Task<NewMovieDropdownsVM> GetNewMovieDropdownsValues()
        {
            var response = new NewMovieDropdownsVM()
            {
                Actors = await _context.Actors.OrderBy(n => n.FullName).ToListAsync(),
                Cinemas = await _context.Cinemas.OrderBy(n => n.Name).ToListAsync(),
                Producers = await _context.Producers.OrderBy(n => n.FullName).ToListAsync()
            };

            return response;
        }

        public async Task AddNewMovieAsync(NewMovieVM data)
        {
            var newMovie = new Movie() { 
             Name = data.Name,
             Describtion = data.Describtion,
             Price = data.Price,
             ImageURL = data.ImageURL,
             StartDate = data.StartDate,
             EndDate = data.EndDate,
             MovieCategory = data.MovieCategory,
             CinemaId = data.CinemaId,
             ProducerId = data.ProducerId 
            //newMovie.Actors_Movies = new List<Actor_Movie>();
            };
            await _context.Movies.AddAsync(newMovie);
            await _context.SaveChangesAsync();
            //Add Movie_Actor
            foreach (var actorId in data.ActorIds)
            {
                var newActorMovie = new Actor_Movie()
                {
                    MovieId = newMovie.Id,
                    ActorId = actorId
                };
                await _context.Actors_Movies.AddAsync(newActorMovie);
            }
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMovieAsync(NewMovieVM data)
        {
            var dbMovie = await _context.Movies.FirstOrDefaultAsync(n => n.Id == data.Id);
            if(dbMovie != null)
            {
                dbMovie.Name = data.Name;
                dbMovie.Describtion = data.Describtion;
                dbMovie.Price = data.Price;
                dbMovie.ImageURL = data.ImageURL;
                dbMovie.StartDate = data.StartDate;
                dbMovie.EndDate = data.EndDate;
                dbMovie.MovieCategory = data.MovieCategory;
                dbMovie.CinemaId = data.CinemaId;
                dbMovie.ProducerId = data.ProducerId;
            }
            
            await _context.SaveChangesAsync();
            //Remove existing actors
            var existingActorsDb = _context.Actors_Movies.Where(n => n.MovieId == data.Id).ToList();
            _context.Actors_Movies.RemoveRange(existingActorsDb);
            await _context.SaveChangesAsync();
            //Add Movie_Actor
            foreach (var actorId in data.ActorIds)
            {
                var newActorMovie = new Actor_Movie()
                {
                    MovieId = data.Id,
                    ActorId = actorId
                };
                await _context.Actors_Movies.AddAsync(newActorMovie);
            }
            await _context.SaveChangesAsync();
        }

        //public async Task<IEnumerable<Movie>> GetAllMoviesAsyncWithCinemasAndProducers()
        //{
        //    throw new NotImplementedException();
        //}

    }


}
       

   


