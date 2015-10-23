using INFOIBV.Presentation;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace INFOIBV.Filters
{
    public abstract class BasicKernel : IApplicableKernel
    {
        protected IApplicableFilter decoratingKernel;

        protected int width;
        protected int height;
        protected float[,] weights;

        public BasicKernel(IApplicableFilter decoratingKernel, int width, int height, float[,] weights)
        {
            this.decoratingKernel = decoratingKernel;
            this.width = width;
            this.height = height;
            this.weights = weights;
        }

        public void apply(Bitmap imageToProcess, MainViewModel reportProgressTo)
        {
            if (decoratingKernel != null)
            {
                decoratingKernel.apply(imageToProcess, reportProgressTo);
            }

            int xOffset = (this.width - 1) / 2;
            int yOffset = (this.height - 1) / 2;

            for (int y = yOffset; y < imageToProcess.Height - yOffset; y++)
            {
                for (int x = xOffset; x < imageToProcess.Width - xOffset; x++)
                {
                    int sum = processPixel(x, y, imageToProcess, reportProgressTo);
                    imageToProcess.SetPixel(x, y, Color.FromArgb(sum, sum, sum));
                }
            }
        }

        public abstract int processPixel(int xCoordinate, int yCoordinate, Bitmap imageToProcess, MainViewModel reportProgressTo);

        public virtual double GetMaximumProgress(int imageWidth, int imageHeight) // Needs to be implemented by every class.
        {
            if (this.decoratingKernel != null)
                return decoratingKernel.GetMaximumProgress(imageWidth, imageHeight);

            return 0.0;
        }
    }
}
