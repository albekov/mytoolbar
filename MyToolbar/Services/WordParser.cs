using System.Diagnostics;

namespace MyToolbar.Services
{
    /// <summary>
    /// Creates separate words from chars flow.
    /// </summary>
    public class WordParser : ICharsHandler
    {
        private readonly IWordsHandler _wordsHandler;

        private string _currentWord = string.Empty;

        public WordParser(IWordsHandler wordsHandler)
        {
            _wordsHandler = wordsHandler;
        }

        public void HandleChar(char key)
        {
            if (char.IsLetterOrDigit(key) || char.IsPunctuation(key))
            {
                Debug.WriteLine(key);
                AppendToWord(key);
            }
            else if (IsBreakKey(key))
            {
                ClearWord();
            }
            else
            {
                return;
            }

            if (!_wordsHandler.HandleWord(_currentWord))
                ClearWord();
        }

        private void AppendToWord(char key)
        {
            _currentWord = _currentWord + key;
        }

        private void ClearWord()
        {
            _currentWord = string.Empty;
        }

        private static bool IsBreakKey(char key) => key == ' ' || key == '\t' || key == '\r' || key == '\n';
    }
}