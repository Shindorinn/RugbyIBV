using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace INFOIBV.Filters
{
    public class BasicKernel
    {
        protected int width;
        protected int height;
        protected float[,] weights; // Need a new name for this.

        public BasicKernel(int width, int height, float[,] weights)
        {
            this.width = width;
            this.height = height;
            this.weights = weights;
        }

        public void apply(Bitmap imageToProcess)
        {
            int xOffset = (this.width - 1) / 2;
            int yOffset = (this.height - 1) / 2;

            for (int x = xOffset; x < imageToProcess.Width - xOffset; x++)
            {
                for (int y = yOffset; y < imageToProcess.Height - yOffset; y++)
                {
                    int sum = processPixel(x, y, imageToProcess);
                    imageToProcess.SetPixel(x, y, Color.FromArgb(sum, sum, sum));
                }
            }
        }

        private int processPixel(int xCoordinate, int yCoordinate, Bitmap imageToProcess)
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

        public static float[,] initializeDoNothingWeights(int width, int height)
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
