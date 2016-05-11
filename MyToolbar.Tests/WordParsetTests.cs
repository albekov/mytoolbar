using MyToolbar.Services;
using NUnit.Framework;

namespace MyToolbar.Tests
{
    [TestFixture]
    public class WordParsetTests
    {
        private readonly ICharsHandler _charsHandler;
        private readonly TestWordHandler _wordsHandler;

        public WordParsetTests()
        {
            _wordsHandler = new TestWordHandler();
            _charsHandler = new WordParser(_wordsHandler);
        }

        [Test]
        public void CreateWordFromCharTest()
        {
            _charsHandler.HandleChar('a');
            Assert.AreEqual("a", _wordsHandler.LastWord);
            _charsHandler.HandleChar('b');
            Assert.AreEqual("ab", _wordsHandler.LastWord);
            _charsHandler.HandleChar('c');
            Assert.AreEqual("abc", _wordsHandler.LastWord);
            _charsHandler.HandleChar(' ');
            Assert.AreEqual("", _wordsHandler.LastWord);
        }

        [Test]
        public void CreateAndResetWordFromCharTest()
        {
            _charsHandler.HandleChar('a');
            _charsHandler.HandleChar('b');
            _charsHandler.HandleChar('c');
            Assert.AreEqual("abc", _wordsHandler.LastWord);
            _charsHandler.HandleChar(' ');
            Assert.AreEqual("", _wordsHandler.LastWord);
        }
    }
}
