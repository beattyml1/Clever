namespace Clever.Service
{
    public interface IContextService<TSession>
    {
        TSession SessionData { get; }
    }
}
