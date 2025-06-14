
namespace eTickets.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class CinemasController : Controller
    {
        private readonly ICinemasService _service;
        public CinemasController(ICinemasService service)
        {
            _service = service;
        }
        [AllowAnonymous]
        public async Task<IActionResult>  Index()
        {
            var allCinemas = await _service.GetAllAsync();

            return View(allCinemas);
        }

        //Get: Cinemas/Create
        [HttpGet]
        public async  Task<IActionResult> Create()
        {
           
             return View();
        }
        //Create New Cinema
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Logo,Name,Describtion")] Cinema cinema)
        {
            if (!ModelState.IsValid)
            {
                return View(cinema);
            }
            await _service.AddAsync(cinema);

            return RedirectToAction(nameof(Index));
        }
        [AllowAnonymous]
        //Get: Cinemas/Details/1
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var cinemaDetails = await _service.GetByIdAsync(id);
            if (cinemaDetails == null) return View("NotFound");
            return View(cinemaDetails);
        }

        //Get: Cinemas/Edit/1
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var cinemaDetails = await _service.GetByIdAsync(id);
            if (cinemaDetails == null) return View("NotFound");
            return View(cinemaDetails);
        }

        //Edit Cinema
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Logo,Name,Describtion")] Cinema cinema)
        {
            if (id != cinema.Id) return View("NotFound");
            if (!ModelState.IsValid)
            {
                return View(cinema);
            }
            await _service.UpdateAsync(id, cinema);
            return RedirectToAction(nameof(Index));
        }

        //Get: Cinemas/Delete/1
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var cinemaDetails = await _service.GetByIdAsync(id);
            if (cinemaDetails == null) return View("NotFound");
            return View(cinemaDetails);
        }
        //Post : Cinema/Delete
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var CinemaDetails = await _service.GetByIdAsync(id);
            if (CinemaDetails == null) return View("NotFound");
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));

        }
    }
}
