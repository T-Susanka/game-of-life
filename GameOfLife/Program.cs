using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            var grid = new Grid(80,50);

            grid.ShowDialog();
        
            


        }
    }
}
