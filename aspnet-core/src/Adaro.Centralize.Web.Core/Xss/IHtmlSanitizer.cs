using Abp.Dependency;

namespace Adaro.Centralize.Web.Xss
{
    public interface IHtmlSanitizer: ITransientDependency
    {
        string Sanitize(string html);
    }
}