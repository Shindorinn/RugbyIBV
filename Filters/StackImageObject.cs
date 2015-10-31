using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INFOIBV.Filters
{
    public class StackImageObject
    {
        public StackImageObject(int x, int y, bool[,] canGo)
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