using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbContext _context;

        public AccountController(UserManager<ApplicationUser> userManager,
                                 SignInManager<ApplicationUser> signInManager,
                                 AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public async Task<IActionResult> Users()
        {
            var users = await _context.Users.ToListAsync();
            return View(users);
        }

        [HttpGet]
        public IActionResult Login()
        {
            Console.WriteLine("Login GET method called");
            return View(new LoginVM());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            //Console.WriteLine("Login POST method called with Email: " + loginVM.EmailAddress);
            if (!ModelState.IsValid)
            {
                //Console.WriteLine("Model state is invalid");
                return View(loginVM);
            }
            var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress);
            if (user != null)
            {
                var passwordCkeck = await _userManager.CheckPasswordAsync(user, loginVM.Password);
                if (passwordCkeck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                    if (result.Succeeded)
                    {

                        return RedirectToAction("Index", "Movies");
                    }

                }
                TempData["Error"] = "Wrong credentials. please, try again";
                return View(loginVM);

            }
            TempData["Error"] = "Wrong credentials. please, try again";
            return View(loginVM);


        }

        [HttpGet]
        public IActionResult Register()
        {

            return View(new RegisterVM());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByEmailAsync(model.EmailAddress);
            if (user != null)
            {
                ModelState.AddModelError("EmailAddress", "This email is already registered.");
                return View(model);
            }

            var newUser = new ApplicationUser()
            {
                FullName = model.FullName,
                Email = model.EmailAddress,
                UserName = model.EmailAddress
            };

            var result = await _userManager.CreateAsync(newUser, model.Password);

            if (result.Succeeded)
            {
                // optionally sign in the user
                return RedirectToAction("Index", "Movies");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View("RegisterCompleted");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Movies");

        }

        [HttpGet]
        public IActionResult AccessDenied(string ReturnUrl)
        {
            return View();
        }
    }
}
