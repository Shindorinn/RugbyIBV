using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace INFOIBV.Filters
{
    public class NormalizingKernel : BasicKernel
    {
        public NormalizingKernel(IApplicableKernel decoratingKernel, int width, int height)
            : base(decoratingKernel, width, height, NormalizingKernel.constructWeights(width, height))
        {
            // Construct additional pylons?
        }

        public NormalizingKernel(int width, int height)
            : this(null, width, height)
        {
            // No additional pylons?
        }

        public override int processPixel(int xCoordinate, int yCoordinate, Bitmap imageToProcess)
        {
            float sum = 0;
            int midX = (this.width - 1) / 2;
            int midY = (this.height - 1) / 2;

            // Loop over Weights
            for (int y = 0; y < this.height; y++)
            {
                int yOffset = y - midY;
                for (int x = 0; x < this.width; x++)
                {
                    int xOffset = x - midX;
                    sum += imageToProcess.GetPixel(xCoordinate + xOffset, yCoordinate + yOffset).R * weights[x, y];
                }
            }

            return (int)Math.Floor(sum);
        }

        private static float[,] constructWeights(int width, int height)
        {
            // First initialize everything to 0,0
            // [y, x]
            float[,] toReturn = new float[height, width];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    toReturn[y, x] = 1 / (height * width);
                }
            }
            return toReturn;
        }
    }
}
