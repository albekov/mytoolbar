using System;
using System.Windows.Forms;
using MyToolbar.Services;

namespace MyToolbar
{
    public partial class ToolbarForm : Form, IWordsHandler
    {
        public ToolbarForm()
        {
            InitializeComponent();
        }

        public bool HandleWord(string word)
        {
            _text.Text = word;
            return true;
        }

        public void Reset()
        {
            _text.Text = string.Empty;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var area = Screen.PrimaryScreen.WorkingArea;
            Left = area.Left;
            Top = area.Bottom - 40;
            Width = area.Width;
            Height = 40;
        }
    }
}