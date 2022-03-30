namespace Checkin.Repositories
{
    public interface ICacheRepository<T>
    {
        void Set(string cacheKey, T value);

        T Get(string cacheKey);
    }
}