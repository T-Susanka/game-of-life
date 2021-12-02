using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace GameOfLife
{
    public partial class Grid : Form
    {
        private int _lineThickness = 2;

        private int _cellSize = 16;

        private int X;

        private int Y;

        private bool _playing;

        private Cell[,] _grid;

        public Grid(int x, int y)
        {
            InitializeComponent();
            this.X = x;
            this.Y = y;

            Width = x * _cellSize + (x - 1) * _lineThickness;

            Height = y * _cellSize + (y - 1) * _lineThickness;

            BackColor = Color.Black;

            _grid = new Cell[y, x];

            InitializeGrid();
        }

        private void InitializeGrid()
        {
            var step = _cellSize + _lineThickness;

            for (int y = 0; y < Height; y += step)
            {
                for (int x = 0; x < Width; x += step)
                {
                    _grid[y / step, x / step] = new Cell(y / step, x / step, _cellSize);
                    Controls.Add(_grid[y / step, x / step]);
                    _grid[y / step, x / step].Top = y;
                    _grid[y / step, x / step].Left = x;
                }
            }
        }

        private void Grid_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case (char)Keys.Enter:
                    Play();
                    break;
                case (char)Keys.Back:
                    Clear();
                    break;
                case 'g':
                    CreateGun();
                    break;
                case 'G':
                    CreateGun();
                    break;
                case (char)Keys.Space:
                    AdvanceToNextGeneration();
                    break;
                case (char)Keys.Escape:
                    Stop();
                    break;
                default:
                    break;
            }
        }

        private void Clear()
        {
            foreach (var cell in _grid)
            {
                cell.ChangeState(false);
            }
        }

        private void Play()
        {
            var t = new Thread(Loop);
            t.Start();            
        }

        private void Loop()
        {
            _playing = !_playing;

            while (_playing)
            {
                AdvanceToNextGeneration();
                Thread.Sleep(125);
            }
        }

        private void Stop()
        {
            _playing = false;
        }

        private void AdvanceToNextGeneration()
        {
            var initialState = new bool[Y, X];

            for (int y = 0; y < Y; y++)
            {
                for (int x = 0; x < X; x++)
                {
                    initialState[y, x] = _grid[y, x].Living;
                }
            }

            var newState = GetNewState(initialState);

            for (int y = 0; y < Y; y++)
            {
                for (int x = 0; x < X; x++)
                {
                    _grid[y, x].ChangeState(newState[y, x]);
                }
            }
            //Refresh();
        }

        private bool[,] GetNewState(bool[,] initialState)
        {

            var newState = new bool[Y, X];

            for (int y = 0; y < Y; y++)
            {
                for (int x = 0; x < X; x++)
                {
                    var neighbours = new bool[8];

                    neighbours[0] = y - 1 > 0 && x - 1 > 0 ? initialState[y - 1, x - 1] : false;

                    neighbours[1] = y - 1 > 0 ? initialState[y - 1, x] : false;

                    neighbours[2] = y - 1 > 0 && x + 1 < X ? initialState[y - 1, x + 1] : false;

                    neighbours[3] = x - 1 > 0 ? initialState[y, x - 1] : false;

                    neighbours[4] = x + 1 < X ? initialState[y, x + 1] : false;

                    neighbours[5] = y + 1 < Y  && x - 1 > 0 ? initialState[y + 1, x - 1] : false;

                    neighbours[6] = y + 1 < Y ? initialState[y + 1, x] : false;

                    neighbours[7] = y + 1 < Y && x + 1 < X ? initialState[y + 1, x + 1] : false;


                    var alive = neighbours.Count(n => n);

                    switch (alive)
                    {
                        case 2:
                            newState[y, x] = initialState[y, x];
                            break;
                        case 3:
                            newState[y, x] = true;
                            break;
                        default:
                            newState[y, x] = false;
                            break;
                    }
                }
            }
            return newState;
        }

        public void CreateGun()
        {
            var coordinates = new List<(int, int)>() {(5,1),(5,2),(6,1),(6,2),(5,11),(6,11), (7,11), (8,12), (4,12),
                                                       (3,13),(9,13),(3,14),(9,14),(4,16),(6,15),(8,16),(5,17),(6,17),(7,17),(6,18),
                                                       (5,21),(4,21),(3,21),(3,22),(4,22),(5,22),(2,23),(6,23),(1,25),(2,25),(6,25),(7,25),
                                                        (3,35),(3,36),(4,35),(4,36)};


            foreach (var c in coordinates)
            {
                _grid[c.Item1, c.Item2].ChangeState(true);
            }
        }




    }
}

