namespace AdaroConnect.Core.Abstract
{
    public interface IRfcConnectionPoolServiceFactory
    {
        IRfcConnectionPool GetService(string serverAlias);
    }
}
