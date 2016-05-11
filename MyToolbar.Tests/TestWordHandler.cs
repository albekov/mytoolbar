using MyToolbar.Services;

namespace MyToolbar.Tests
{
    public class TestWordHandler : IWordsHandler
    {
        public string LastWord { get; private set; } = string.Empty;

        public bool HandleWord(string word)
        {
            LastWord = word;
            return true;
        }

        public void Reset()
        {
            LastWord = string.Empty;
        }
    }
}