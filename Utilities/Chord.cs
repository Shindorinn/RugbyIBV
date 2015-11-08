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
        public readonly double orientation;

        public Chord(ListPixel firstPixel, ListPixel secondPixel)
            : this(firstPixel, secondPixel, Chord.calcDistance(firstPixel, secondPixel))
        {
            // Calcs distance
        }

        public Chord(ListPixel firstPixel, ListPixel secondPixel, double distance)
        {
            this.firstPixel = firstPixel;
            this.secondPixel = secondPixel;
            this.distance = distance;

            if (firstPixel == null || secondPixel == null)
                Console.WriteLine("Null pixels detected.");
            else
                this.orientation = calcOrientation();
        }

        public static double calcDistance(ListPixel toCalcFrom, ListPixel toCalcTo)
        {
            return Math.Sqrt(Math.Pow((double)toCalcTo.X - (double)toCalcFrom.X, 2) + Math.Pow((double)toCalcTo.Y - (double)toCalcFrom.Y, 2));
        }

        private double calcOrientation()
        {
            return Math.Atan(((double)secondPixel.Y - (double)firstPixel.Y) / ((double)secondPixel.X - (double)firstPixel.X));
        }
    }
}
