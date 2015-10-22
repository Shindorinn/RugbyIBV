using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace INFOIBV.Filters
{
    public class NormalizingKernel : BasicKernel
    {
        public void apply(Bitmap imageToProcess)
        {
            if(! (imageToProcess.Height >= this.height) &&
               ! (imageToProcess.Width >= this.width ) )
            {
                // The filter cannot be applied
            }


        }

        private float[,] initializeWeights(int width, int height)
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
