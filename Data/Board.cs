using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class Board
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public Board(int width, int height) 
        { 
            Width = width;
            Height = height;
        }
    }
}
