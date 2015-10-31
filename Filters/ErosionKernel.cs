using INFOIBV.Presentation;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace INFOIBV.Filters
{
    public class ErosionKernel : BasicKernel
    {
        // : base(decoratingKernel, 3, 3, ErosionOperation.constructWeights()) 
        public ErosionKernel(IApplicableFilter decoratingFilter)
            : base(decoratingFilter, 3, 3, ErosionKernel.constructWeights())
        {
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
                    // Need to double-check if the objects are black or white
                    // If so, this short-hand if-statement needs to be turned around to work.
                    sum += imageToProcess[xCoordinate + xOffset, yCoordinate + yOffset].R * weights[x, y] > 1 ? 1 : 0; // Currently, reacting to white
                    reportProgressTo.Progress++;
                }
            }
            if (sum >= 9) // If the kernel fits in the object, the middle pixel needs to turn black
            {
                return 0;
            }
            else
            {
                return 255;
            }

        }


        public override double GetMaximumProgress(int imageWidth, int imageHeight)
        {
            return base.GetMaximumProgress(imageWidth, imageHeight) + (((imageWidth - (this.width - 1)) * (imageHeight - (this.height - 1))) * (this.width * this.height));
        }

        private static float[,] constructWeights()
        {
            float[,] toReturn = new float[3, 3];
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    toReturn[x, y] = 1.0f;
                }
            }
            return toReturn;
        }

    }
}
