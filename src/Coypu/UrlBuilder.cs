namespace Coypu
{
    public interface IUrlBuilder
    {
        string GetFullyQualifiedUrl(string virtualPath, SessionConfiguration sessionConfiguration);
    }
}