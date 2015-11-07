using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INFOIBV.Utilities
{
    public class Chord
    {
        public readonly ListPixel firstPixel;
        public readonly ListPixel secondPixel;
        public readonly double distance;

        public Chord(ListPixel firstPixel, ListPixel secondPixel, double distance)
        {
            this.firstPixel = firstPixel;
            this.secondPixel = secondPixel;
            this.distance = distance;
        }

    }
}
