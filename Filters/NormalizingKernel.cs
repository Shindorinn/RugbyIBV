using INFOIBV.Presentation;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace INFOIBV.Filters
{
    public class NormalizingKernel : BasicKernel
    {
        public NormalizingKernel(IApplicableFilter decoratingKernel, int width, int height)
            : base(decoratingKernel, width, height, NormalizingKernel.constructWeights(width, height))
        {
            // Construct additional pylons?
        }

        public NormalizingKernel(int width, int height)
            : this(null, width, height)
        {
            // No additional pylons?
        }

        public override int processPixel(int xCoordinate, int yCoordinate, Color[,] imageToProcess, MainViewModel reportProgressTo)
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
                    sum += imageToProcess[xCoordinate + xOffset, yCoordinate + yOffset].R * weights[y, x];
                    reportProgressTo.Progress++;
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
                    toReturn[y, x] = 1.0f / ((float)(height * width));
                }
            }
            return toReturn;
        }

        public override double GetMaximumProgress(int imageWidth, int imageHeight) // Needs to be implemented by every class.
        {
            return base.GetMaximumProgress(imageWidth, imageHeight) + ((imageWidth * imageHeight) * (this.width * this.height));
        }
    }
}
