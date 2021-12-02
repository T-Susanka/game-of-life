using System.Drawing;
using System.Windows.Forms;

namespace GameOfLife
{
    public partial class Cell : PictureBox
    {
        public bool Living { get; set; }

        public int X { get; }

        public int Y { get; }

        public Cell(int y, int x, int size)
        {
            InitializeComponent();
            X = x;
            Y = y;
            Height = size;
            Width = size;
            BackColor = Color.DarkSlateGray;
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {  
            ChangeState(!Living);
        }

        public void ChangeState(bool state)
        {
            Living = state;
            switch (state)
            {
                case true:
                    BackColor = Color.ForestGreen;
                    break;
                case false:
                    BackColor = Color.DarkSlateGray;
                    break;
            }
        }
    }
}
