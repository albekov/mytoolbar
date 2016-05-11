using System;
using System.Drawing;
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
            var rectangle = new Rectangle(area.X, area.Bottom - 40, area.Width, 40);
            Left = area.Left;
            Top = area.Bottom - 40;
            Width = area.Width;
            Height = 40;
        }
    }
}