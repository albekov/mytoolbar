using MyToolbar.Services;
using NUnit.Framework;

namespace MyToolbar.Tests
{
    [TestFixture]
    public class AutocompleteTests
    {
        private readonly IWordsHandler _handler;
        private readonly TestWordHandler _wordsHandler;

        public AutocompleteTests()
        {
            _wordsHandler = new TestWordHandler();
            _handler = new AutoCompleteHandler(_wordsHandler, new[] {"foo", "bar"});
        }

        [Test]
        public void TestAutocompletion()
        {
            _handler.HandleWord("f");
            Assert.AreEqual("foo", _wordsHandler.LastWord);

            _handler.HandleWord("fo");
            Assert.AreEqual("foo", _wordsHandler.LastWord);

            _handler.HandleWord("foo");
            Assert.AreEqual("foo", _wordsHandler.LastWord);

            _handler.HandleWord("fooo");
            Assert.AreEqual("", _wordsHandler.LastWord);

            _handler.HandleWord("bar");
            Assert.AreEqual("bar", _wordsHandler.LastWord);

            _handler.HandleWord("hello");
            Assert.AreEqual("", _wordsHandler.LastWord);

            _handler.HandleWord("");
            Assert.AreEqual("", _wordsHandler.LastWord);
        }
    }
}