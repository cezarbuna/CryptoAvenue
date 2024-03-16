namespace CryptoAvenue.Services
{
    public class ServiceLifetime : IServiceLifetime
    {
        public Guid Guid { get; set; } = Guid.NewGuid();
    }
}
