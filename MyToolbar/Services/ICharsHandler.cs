namespace MyToolbar.Services
{
    /// <summary>
    /// Base interface to handle flow of chars.
    /// </summary>
    public interface ICharsHandler
    {
        void HandleChar(char key);
    }
}