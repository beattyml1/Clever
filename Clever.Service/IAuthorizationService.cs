namespace Clever.Service
{
    public interface IEntityAuthorizationService<T>
    {
        bool CanRead(T entity);
        bool CanUpdate(T entity);
        bool CanPost(T entity);
        bool CanDelete(T entity);
    }
}
