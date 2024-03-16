namespace CryptoAvenue.Services
{
    public class SingletonService : ISingletonService
    {
        public Guid Guid { get; set; } = Guid.NewGuid();
    }
}
