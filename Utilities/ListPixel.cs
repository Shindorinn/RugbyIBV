using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INFOIBV.Utilities
{
    public class ListPixel
    {
        public ListPixel(int x, int y, bool[,] canGo)
        {
            X = x;
            Y = y;
            CanGo = canGo;
        }

        public int X { get; private set; }
        public int Y { get; private set; }
        public bool[,] CanGo { get; set; }
    }
}