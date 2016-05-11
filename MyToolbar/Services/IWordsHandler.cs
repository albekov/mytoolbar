namespace MyToolbar.Services
{
    /// <summary>
    /// Base interface to handle words.
    /// </summary>
    public interface IWordsHandler
    {
        /// <summary>
        /// Send new word to the handler.
        /// </summary>
        /// <param name="word"></param>
        /// <returns>Is word handled or not.</returns>
        bool HandleWord(string word);

        void Reset();
    }
}