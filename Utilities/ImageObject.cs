using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INFOIBV.Utilities
{
    public class ImageObject
    {
        private int[,] pixels;
        public int OffsetX { get; private set; }
        public int OffsetY { get; private set; }

        public ImageObject(List<ListPixel> pixelsToProcess)
        {
            OffsetX = int.MaxValue;
            OffsetY = int.MinValue; // To make sure it changes on the first time
            int offsetXMax = -1;
            int offsetYMax = -1;

            for (int i = 0; i < pixelsToProcess.Count; i++)
            {
                if (pixelsToProcess[i].X < OffsetX)
                    OffsetX = pixelsToProcess[i].X;
                if (pixelsToProcess[i].Y < OffsetY)
                    OffsetY = pixelsToProcess[i].Y;
                if (pixelsToProcess[i].X > offsetXMax)
                    offsetXMax = pixelsToProcess[i].X;
                if (pixelsToProcess[i].Y > offsetYMax)
                    offsetYMax = pixelsToProcess[i].Y;
            }


            //pixels = new int[X,Y];
            // Loop through pixelsToProcess to fill pixels and the offset
        }

        public int GetNumberOfPixels()
        {
            return pixels.Length;
        }
    }
}