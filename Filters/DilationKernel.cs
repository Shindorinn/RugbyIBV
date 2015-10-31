using INFOIBV.Presentation;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace INFOIBV.Filters
{
    public class DilationKernel : ErosionKernel
    {        
        public DilationKernel(IApplicableFilter decoratingFilter)
            : base(decoratingFilter)
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

                    // Is image * weights bigger than 1 every coordinate?
                    float result = imageToProcess[xCoordinate + xOffset, yCoordinate + yOffset].R * weights[x, y];
                    if (result > 1)
                    {
                        sum += 1;
                    }
                    // can be optimized by skipping the rest of the operations if one of the if-statements fails
                    // just need to calculate and move the progressbar accordingly.
                    reportProgressTo.Progress++;
                }
            }
            if (sum >= 9) // If the kernel fits in the object, the middle pixel needs to turn black
            {
                return 255;
            }
            else
            {
                return 0;
            }

        }

    }
}
