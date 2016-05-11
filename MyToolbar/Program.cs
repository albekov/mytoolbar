using System;
using System.Windows.Forms;
using MyToolbar.Services;

namespace MyToolbar
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var form = new ToolbarForm();
            var words = new[]
            {
                "hello",
                "welcome",
                "automatic",
                "autocomplete",
                "I'm",
                "the",
                "best",
                "developer",
                "foobar"
            };

            var autoCompleteHandler = new AutoCompleteHandler(form, words);
            var wordHandler = new WordParser(autoCompleteHandler);
            using (var helper = new KeyboardHookHelper(wordHandler))
            {
                Application.Run(form);
            }
        }
    }
}