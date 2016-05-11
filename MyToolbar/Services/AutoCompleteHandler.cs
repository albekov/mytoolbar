using System;
using System.Linq;

namespace MyToolbar.Services
{
    /// <summary>
    /// Class which implements IWordsHandler and filters incoming words by predefined dictionary.
    /// </summary>
    public sealed class AutoCompleteHandler : IWordsHandler
    {
        private readonly string[] _words;
        private readonly IWordsHandler _wordsHandler;

        public AutoCompleteHandler(IWordsHandler wordsHandler, string[] words)
        {
            _wordsHandler = wordsHandler;
            _words = words;
        }

        public bool HandleWord(string word)
        {
            if (!string.IsNullOrEmpty(word))
            {
                var match = _words.FirstOrDefault(w => w.StartsWith(word, StringComparison.OrdinalIgnoreCase));
                if (match != null)
                {
                    _wordsHandler.HandleWord(match);
                    return true;
                }
            }
            _wordsHandler.Reset();
            return false;
        }

        public void Reset()
        {
        }
    }
}