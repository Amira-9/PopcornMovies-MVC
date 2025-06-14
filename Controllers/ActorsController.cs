
namespace eTickets.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class ActorsController  : Controller
    {
        private readonly IActorsService _service;

        public ActorsController(IActorsService service)
        {
            _service = service;  
        }
        [AllowAnonymous]
        public async Task<IActionResult> IndexActors()
        {
            var data = await  _service.GetAllAsync();
            return View(data);
        }
        //Get : Actors/Create
        [HttpGet]
        public IActionResult Create()
        {
           return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create( [Bind("FullName,ProfilePictureURL,Bio")] Actor actor)
        {
            if(!ModelState.IsValid)
            {
               return View(actor);
            }
            await _service.AddAsync(actor);
            return RedirectToAction(nameof(IndexActors));
        }
        [AllowAnonymous]

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var actorDetails = await _service.GetByIdAsync(id);
                return View(actorDetails);
            }
            catch (InvalidOperationException)
            {
                return View("NotFound");
            }
        }


        //Get : Actors/Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id);
            if (actorDetails == null)
            {
                return View("NotFound");
            }
            return View(actorDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,ProfilePictureURL,Bio")] Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }
            await _service.UpdateAsync(id ,actor);
            return RedirectToAction(nameof(IndexActors));
        }

        //Get : Actors/Delete/1
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id);
            if (actorDetails == null)
            {
                return View("NotFound");
            }
            return View(actorDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var actorDeteails= await _service.GetByIdAsync(id);
            if(actorDeteails == null)
            {
                return View("NotFound");
            }
             await _service.DeleteAsync(id);
             return RedirectToAction(nameof(IndexActors));
        }

    }
}
