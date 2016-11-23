namespace Coypu.Finders
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDisambiguationStrategy
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="elementFinder"></param>
        /// <returns></returns>
        Element ResolveQuery(ElementFinder elementFinder);
    }
}