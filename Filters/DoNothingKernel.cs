using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace INFOIBV.Filters
{
    public class DoNothingKernel : BasicKernel
    {
        public DoNothingKernel(IApplicableKernel decoratingKernel, int width, int height)
            : base(decoratingKernel, width
                , height, DoNothingKernel.initializeDoNothingWeights(width, height))
        {
            // Zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz....
        }

        public DoNothingKernel(int width, int height)
            : this(null, width, height)
        {
            // *snore* Zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz..
        }

        public override int processPixel(int xCoordinate, int yCoordinate, Bitmap imageToProcess)
        {
            return imageToProcess.GetPixel(xCoordinate, yCoordinate).R;
        }

        private static float[,] initializeDoNothingWeights(int width, int height)
        {
            // This is an example do nothing kernel, all zeroes except the middle.

            // First initialize everything to 0,0
            // [y, x] apparently. see -> https://msdn.microsoft.com/en-us/library/2yd9wwz4.aspx
            float[,] toReturn = new float[height, width];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    toReturn[y, x] = 0;
                }
            }

            // Then determine middle coordinate
            int midY = (int)Math.Floor((float)height / 2);
            int midX = (int)Math.Floor((float)width / 2);

            toReturn[midY, midX] = 1;

            return toReturn;
        }
    }
}
