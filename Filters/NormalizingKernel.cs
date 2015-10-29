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
            for (int x = 0; x < this.width; x++)
            {
                int xOffset = x - midX;
                for (int y = 0; y < this.height; y++)
                {
                    int yOffset = y - midY;
                    sum += imageToProcess[xCoordinate + xOffset, yCoordinate + yOffset].R * weights[x, y];
                    reportProgressTo.Progress++;
                }
            }

            return (int)Math.Floor(sum);
        }

        private static float[,] constructWeights(int width, int height)
        {
            float[,] toReturn = new float[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    toReturn[x, y] = 1.0f / ((float)(height * width));
                }
            }
            return toReturn;
        }

        public override double GetMaximumProgress(int imageWidth, int imageHeight) // Needs to be implemented by every class.
        {
            return base.GetMaximumProgress(imageWidth, imageHeight) + (((imageWidth - (this.width - 1)) * (imageHeight - (this.height - 1))) * (this.width * this.height));
        }
    }
}
