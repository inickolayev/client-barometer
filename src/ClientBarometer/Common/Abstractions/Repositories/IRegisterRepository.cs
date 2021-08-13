namespace ClientBarometer.Common.Abstractions.Repositories
{
    public interface IRegisterRepository<in T> where T : class
    {
        void RegisterNew(T entity);
        void RegisterNewRange(T[] entities);
        void RegisterDirty(T entity);
        void RegisterDirtyRange(T[] entities);
        void RegisterDelete(T entity);
        void RegisterDeleteRange(T[] entities);
    }
}
