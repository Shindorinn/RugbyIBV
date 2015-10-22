using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace INFOIBV.Filters
{
    public abstract partial class BasicKernel : IApplicableKernel
    {
        protected IApplicableKernel decoratingKernel;

        protected int width;
        protected int height;
        protected float[,] weights; 

        public BasicKernel(IApplicableKernel decoratingKernel, int width, int height, float[,] weights)
        {
            this.decoratingKernel = decoratingKernel;
            this.width = width;
            this.height = height;
            this.weights = weights;
        }

        public void apply(Bitmap imageToProcess)
        {
            if (decoratingKernel != null)
            {
                decoratingKernel.apply(imageToProcess);
            }

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

        public abstract int processPixel(int xCoordinate, int yCoordinate, Bitmap imageToProcess);
    }
}
