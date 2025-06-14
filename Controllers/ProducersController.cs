namespace eTickets.Controllers
{
    [Authorize(Roles =UserRoles.Admin)]
    public class ProducersController : Controller
    {
        private readonly IProducerService _Service;

        public ProducersController(IProducerService Service)
        {
            _Service = Service;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allProducers = await _Service.GetAllAsync();
            
            return View(allProducers);
        }
        [AllowAnonymous]
        //Get : Producers/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var producerDetails = await _Service.GetByIdAsync(id);
            if (producerDetails == null) return View("NotFound");
            return View(producerDetails);
        }


        //Get : Producers/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //Post : Producers/Create
        [HttpPost]
        public async Task<IActionResult> Create([Bind("ProfilePictureURL,FullName,Bio")] Producer producer)
        {
            if (!ModelState.IsValid)
            {
                return View(producer);
            }
            await _Service.AddAsync(producer);
            return RedirectToAction(nameof(Index));
        }

        //Get : Producers/Edit/1 
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var producerDetails = await _Service.GetByIdAsync(id);
            if (producerDetails == null) return View("NotFound");

            return View(producerDetails);
        }

        //Post : Producers/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(int id ,[Bind("Id,ProfilePictureURL,FullName,Bio")] Producer producer)
        {
            if (!ModelState.IsValid)
            {
                return View(producer);
            }
            if(id == producer.Id)
            {
                await _Service.UpdateAsync(id, producer);
                return RedirectToAction(nameof(Index));

            }
            return View(producer);
        }

        //Get : Producers/Delete/1 
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var producerDetails = await _Service.GetByIdAsync(id);
            if (producerDetails == null) return View("NotFound");

            return View(producerDetails);
        }

        //Post : Producers/Delete
        [HttpPost , ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producerDetails = await _Service.GetByIdAsync(id);
            if (producerDetails == null) return View("NotFound");
            await _Service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));

        }

    }
}
