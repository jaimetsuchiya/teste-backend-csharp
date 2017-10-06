namespace Infrastructure.TorreHanoi.Cache
{
    public interface ICacheManager
    {
        object DataSource { get; set; }

        object Get(string key); 
        void Set(string key); 
    }
}