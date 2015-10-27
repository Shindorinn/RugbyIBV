using INFOIBV.Presentation;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace INFOIBV.Filters
{
    public class PrewittKernel : BasicKernel
    {
        public PrewittKernel(Direction compass)
            : this(null, compass)
        {
            // A compass that doesn't point north?
        }

        public PrewittKernel(IApplicableFilter decoratingKernel, Direction compass)
            : base(decoratingKernel, 3, 3, PrewittKernel.constructWeights(3, 3, compass))
        {
            // What's the heading capt'n?
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

        private static float[,] constructWeights(int width, int height, Direction compass)
        {
            // [y, x]
            float[,] toReturn = new float[height, width];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    float value = 0.0f;
                    switch (compass)
                    {
                        case Direction.North:
                            if (y == 0)
                                value = 1.0f;
                            else if (y == height - 1)
                                value = -1.0f;
                            break;
                        case Direction.NorthEast:
                            if (x < y)
                                value = -1.0f;
                            else if (x > y)
                                value = 1.0f;
                            break;
                        case Direction.East:
                            if (x == 0)
                                value = -1.0f;
                            else if (x == width - 1)
                                value = 1.0f;
                            break;
                        case Direction.SouthEast:
                            if (2 - x < y)
                                value = 1.0f;
                            else if (2 - x > y)
                                value = -1.0f;
                            break;
                        case Direction.South:
                            if (y == 0)
                                value = -1.0f;
                            else if (y == height - 1)
                                value = 1.0f;
                            break;
                        case Direction.SouthWest:
                            if (x < y)
                                value = 1.0f;
                            else if (x > y)
                                value = -1.0f;
                            break;
                        case Direction.West:
                            if (x == 0)
                                value = 1.0f;
                            else if (x == width - 1)
                                value = -1.0f;
                            break;
                        case Direction.NorthWest:
                            if (2 - x < y)
                                value = -1.0f;
                            else if (2 - x > y)
                                value = 1.0f;
                            break;
                    }

                    toReturn[y, x] = value;
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