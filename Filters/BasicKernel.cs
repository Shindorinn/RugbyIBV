using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace INFOIBV.Filters
{
    public abstract class BasicKernel
    {
        protected int width;
        protected int height;
        protected float[,] weights; // Need a new name for this.

        public BasicKernel(int width, int height, float[,] weights)
        {
            this.weights = weights;
        }

        protected void apply(Bitmap imageToProcess)
        {
            int xOffset = (int)Math.Floor((float)this.width / 2);
            int yOffset = (int)Math.Floor((float)this.height / 2);

            for (int x = xOffset; x < imageToProcess.Width - xOffset; x++)
            {
                for (int y = yOffset; y < imageToProcess.Height - yOffset; y++)
                {
                    processPixel(x, y, imageToProcess);
                }
            }
        }

        private int processPixel(int xCoordinate, int yCoordinate, Bitmap imageToProcess)
        {
            float sum = 0;
            int midX = determineMiddleXCoordinateOfWeights(this.weights);
            int midY = determineMiddleYCoordinateOfWeights(this.weights);

            // Loop over Weights
            for (int y = 0; y < this.height; y++)
            {
                int yOffset = y - midY;
                for (int x = 0; x < this.width; x++)
                {
                    int xOffset = x - midX;
                    sum += imageToProcess.GetPixel(xCoordinate + xOffset, yCoordinate + yOffset).R * weights[x, y];
                    Console.WriteLine("xOffset = " + xOffset + " ; yOffset = " + yOffset);
                }
            }

            Console.WriteLine("Sum = " + sum);
            return (int)Math.Floor(sum);
        }

        private int processPixel2(int xCoordinate, int yCoordinate, Bitmap imageToProcess)
        {
            float sum = 0;

            // Kernel is always odd
            int radiusX = ((this.width - 1) / 2) * -1;
            int radiusY = ((this.height - 1) / 2) * -1;

            // Loop variables
            int i = 0;
            int j = 0;

            // Loop over Weights
            for (int y = radiusY; j < this.height; j++, y++)
            {
                for (int x = radiusX; i < this.width; i++, x++)
                {
                    sum += imageToProcess.GetPixel(xCoordinate + x, yCoordinate + y).R * weights[i, j];
                }
            }

            Console.WriteLine("Sum = " + sum);
            return (int)Math.Floor(sum);
        }

        private int determineMiddleXCoordinateOfWeights(float[,] map)
        {
            return (int)Math.Floor((float)width / 2);
        }

        private int determineMiddleYCoordinateOfWeights(float[,] map)
        {
            return (int)Math.Floor((float)height / 2);

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
