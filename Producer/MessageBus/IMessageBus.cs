namespace Producer.MessageBus
{
    public interface IMessageBus
    {
        Task Publish(object entity);
    }
}