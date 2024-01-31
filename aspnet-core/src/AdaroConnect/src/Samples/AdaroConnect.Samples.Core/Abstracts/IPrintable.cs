namespace AdaroConnect.Application.Core.Abstracts
{
    public interface IPrintable<in T> where T: class, new()
    {
        void Print(T model);
    }
}
