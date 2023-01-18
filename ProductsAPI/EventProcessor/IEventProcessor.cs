namespace ProductsAPI.EventProcessor
{
    public interface IEventProcessor
    {
        void Process(string message);
    }
}