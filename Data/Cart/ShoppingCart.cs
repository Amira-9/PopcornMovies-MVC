namespace eTickets.Data.Cart
{
    public class ShoppingCart
    {
        private readonly AppDbContext _context;

        public string ShoppingCartId { get; set; }

        public List<ShoppingCartItem> ShoppingCartItems { get; set; } = new List<ShoppingCartItem>();

        public ShoppingCart(AppDbContext context)
        {
            _context = context;
        }

        // Static method to get or create a ShoppingCart with session ID
        public static ShoppingCart GetShoppingCart(IServiceProvider services)
        {
            var session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetRequiredService<AppDbContext>();

            if (session == null)
            {
                throw new Exception("Session is not available.");
            }

            string cartId = session.GetString("ShoppingCartId") ?? Guid.NewGuid().ToString();
            session.SetString("ShoppingCartId", cartId);

            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }

        // Add item to cart
        public void AddItemToCart(Movie movie)
        {
            if (movie == null) return;

            var shoppingCartItem = _context.ShoppingCartItems
                .FirstOrDefault(n => n.Movie.Id == movie.Id && n.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    Movie = movie,
                    Amount = 1,
                    ShoppingCartId = ShoppingCartId
                };
                _context.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }

            _context.SaveChanges();
        }

        // Remove item from cart
        public void RemoveItemFromCart(Movie movie)
        {
            if (movie == null) return;

            var shoppingCartItem = _context.ShoppingCartItems
                .FirstOrDefault(n => n.Movie.Id == movie.Id && n.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                }
                else
                {
                    _context.ShoppingCartItems.Remove(shoppingCartItem);
                }

                _context.SaveChanges();
            }
        }

        // Get all items in the cart
        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems = _context.ShoppingCartItems
                .Where(n => n.ShoppingCartId == ShoppingCartId)
                .Include(n => n.Movie)
                .ToList();
        }

        // Get total price
        public double GetShoppingCartTotal()
        {
            return _context.ShoppingCartItems
                .Where(n => n.ShoppingCartId == ShoppingCartId)
                .Select(n => n.Movie.Price * n.Amount)
                .Sum();
        }

        // Clear the shopping cart
        public async Task ClearShoppingCartAsync()
        {
            var items = await _context.ShoppingCartItems
                .Where(n => n.ShoppingCartId == ShoppingCartId)
                .ToListAsync();
            _context.ShoppingCartItems.RemoveRange(items);
            await _context.SaveChangesAsync();
            var session = _context.GetService<IHttpContextAccessor>()?.HttpContext.Session;
            if (session != null)
            {
                session.Remove("ShoppingCartId");
            }

        }
    }
}