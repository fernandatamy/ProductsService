namespace OrdersAPI.Data
{
    public interface IOrderRepository
    {
        void SaveChanges();
        IEnumerable<Order> GetOrders();
        Order GetOrderById(int id);
        void CreateOrder(Order order);
    }
}
