using eTickets.Data.Cart;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace eTickets.Controllers
{
    [Authorize] 

    public class OrdersController : Controller
    {
        private readonly IMoviesService _moviesService;
        private readonly ShoppingCart _shoppingCart;
        private readonly IOrdersService _orderService;
        public OrdersController(IMoviesService moviesService , ShoppingCart shoppingCart, IOrdersService orderService)
        {
            _moviesService = moviesService;
            _shoppingCart = shoppingCart;
            _orderService = orderService;
        }

        // GET: Orders/ShoppingCart
        public async Task<IActionResult> Index()
        {
            string userId =User.FindFirstValue(ClaimTypes.NameIdentifier); // This should be fetched from the user context or identity
            string userRole = User.FindFirstValue(ClaimTypes.Role); // This should be fetched from the user context or identity
            var orders = await _orderService.GetOrdersByUserIdAndRoleAsync(userId , userRole);
            return View(orders);

        }

        public IActionResult ShoppingCart()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;
            var response = new ShoppingCartVM()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };
            return View(response);
        }

        public async Task<IActionResult> AddToShoppingCart(int id)
        {
            var item = await _moviesService.GetMovieByIdAsync(id);
            if (item != null)
            {
                _shoppingCart.AddItemToCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> RemoveItemFromShoppingCart(int id)
        {
            var item = await _moviesService.GetMovieByIdAsync(id);
            if (item != null)
            {
                _shoppingCart.RemoveItemFromCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }

        public async  Task<IActionResult> CompleteOrder()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier); ;
            string userEmailAddress = User.FindFirstValue(ClaimTypes.Email);

            await _orderService.StoredOrderAsync(items, userId, userEmailAddress);
            await _shoppingCart.ClearShoppingCartAsync();
            return View("OrderCompleted");

        }

    }
}
