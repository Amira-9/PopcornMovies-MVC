
namespace eTickets.Data.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly AppDbContext _context;
        public OrdersService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string userId , string userRole)
        {
            var Orders = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Movie)
                .Include(o => o.User)
                .ToListAsync();
            if(userRole != "Admin")
            {
                Orders=Orders.Where(n => n.UserId == userId).ToList();
            }
                return Orders;
        }

        public async Task StoredOrderAsync(List<ShoppingCartItem> item, string userId, string userEmailAddress)
        {
            var order = new Order()
            {
                UserId = userId,
                Email = userEmailAddress,
                
            };
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            foreach (var shoppingCartItem in item)
            {
                var orderItem = new OrderItem()
                {
                    Amount = shoppingCartItem.Amount,
                    MovieId = shoppingCartItem.Movie.Id,
                    OrderId = order.Id,
                    Price = shoppingCartItem.Movie.Price 
                };
                await _context.OrderItems.AddAsync(orderItem);
                await _context.SaveChangesAsync();
            }
        }
    }
}
