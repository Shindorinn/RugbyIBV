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

        public BasicKernel(int width, int height)
        {
            // We should really check if the height and width are of odd value and above zero here.
            
            this.width = width;
            this.height = height;
            this.weights = this.initializeWeights();
        }

        public abstract void apply(Bitmap imageToProcess);

        private float[,] initializeWeights()
        {
            return this.initializeWeights(this.width, this.height);
        }

        private abstract float[,] initializeWeights(int width, int height);

        private float[,] initializeDoNothingWeights(int width, int height) 
        {
            // This is an example do nothing kernel, all zeroes except the middle.

            // First initialize everything to 0,0
            // [y, x] apparently. see -> https://msdn.microsoft.com/en-us/library/2yd9wwz4.aspx
            float[,] toReturn = new float[height, width];
            for (int x = 0; x < width; x++){
                for (int y = 0; y < height; y++){
                    toReturn[y, x] = 0;
                }
            }

            // Then determine middle coordinate
            int midY = (int) Math.Floor((float)height/2);
            int midX = (int) Math.Floor((float)width/2);

            toReturn[midY, midX] = 1;

            return toReturn;
        }

    }
}
