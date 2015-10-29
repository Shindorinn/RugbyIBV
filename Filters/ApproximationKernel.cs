using INFOIBV.Presentation;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace INFOIBV.Filters
{
    public class ApproximationKernel : BasicKernel
    {
        public ApproximationKernel(IApplicableFilter decoratingKernel, Direction compass)
            : base(decoratingKernel, 3, 3, ApproximationKernel.constructWeights(3, 3, compass))
        {
            // A snail can sleep for 3 years.
        }

        public override int processPixel(int xCoordinate, int yCoordinate, Color[,] imageToProcess, MainViewModel reportProgressTo)
        {
            float sum = 0;

            // Loop over Weights
            for (int x = 0; x < this.width; x++)
            {
                int xOffset = x - 1;
                for (int y = 0; y < this.height; y++)
                {
                    int yOffset = y - 1;
                    sum += imageToProcess[xCoordinate + xOffset, yCoordinate + yOffset].R * weights[x, y];
                    reportProgressTo.Progress++;
                }
            }

            return (int)Math.Floor(sum) + 128;
        }

        private static float[,] constructWeights(int width, int height, Direction compass)
        {
            float[,] toReturn = new float[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    toReturn[x, y] = 0.0f;
                }
            }

            switch (compass)
            {
                case Direction.North:
                    toReturn[0, 1] = 0.5f;
                    toReturn[2, 1] = -0.5f;
                    break;
                case Direction.NorthEast:
                    toReturn[0, 2] = 0.5f;
                    toReturn[2, 0] = -0.5f;
                    break;
                case Direction.East:
                    toReturn[1, 2] = 0.5f;
                    toReturn[1, 0] = -0.5f;
                    break;
                case Direction.SouthEast:
                    toReturn[2, 2] = 0.5f;
                    toReturn[0, 0] = -0.5f;
                    break;
                case Direction.South:
                    toReturn[2, 1] = 0.5f;
                    toReturn[0, 1] = -0.5f;
                    break;
                case Direction.SouthWest:
                    toReturn[2, 0] = 0.5f;
                    toReturn[0, 2] = -0.5f;
                    break;
                case Direction.West:
                    toReturn[1, 0] = 0.5f;
                    toReturn[1, 2] = -0.5f;
                    break;
                case Direction.NorthWest:
                    toReturn[0, 0] = 0.5f;
                    toReturn[2, 2] = -0.5f;
                    break;
            }

            return toReturn;
        }

        public override double GetMaximumProgress(int imageWidth, int imageHeight) // Needs to be implemented by every class.
        {
            return base.GetMaximumProgress(imageWidth, imageHeight) + (((imageWidth - 2) * (imageHeight - 2)) * 9);
        }
    }
}