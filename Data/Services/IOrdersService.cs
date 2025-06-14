namespace eTickets.Data.Services
{
    public interface IOrdersService
    {
        Task StoredOrderAsync(List<ShoppingCartItem> item, string userId, string userEmailAddress);
        Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string userId , string userRole);


    }
}
