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

        public virtual Color[,] apply(Color[,] imageToProcess, MainViewModel reportProgressTo)
        {
            if (decoratingKernel != null)
            {
                imageToProcess = decoratingKernel.apply(imageToProcess, reportProgressTo);
            }

            int xOffset = (this.width - 1) / 2;
            int yOffset = (this.height - 1) / 2;

            Color[,] imageToReturn = new Color[imageToProcess.GetLength(0), imageToProcess.GetLength(1)];
            Array.Copy(imageToProcess, imageToReturn, imageToProcess.GetLength(0) * imageToProcess.GetLength(1));

            for (int x = xOffset; x < imageToProcess.GetLength(0) - xOffset; x++) // GetLength(x), where x is the dimension, give you the length of the specified part of the array.
            {
                for (int y = yOffset; y < imageToProcess.GetLength(1) - yOffset; y++)
                {
                    int sum = processPixel(x, y, imageToProcess, reportProgressTo);
                    //sum = sum < 0 ? 0 : sum > 255 ? 255 : sum;
                    imageToReturn[x, y] = Color.FromArgb(sum, sum, sum);
                }
            }

            return imageToReturn;
        }

        public abstract int processPixel(int xCoordinate, int yCoordinate, Color[,] imageToProcess, MainViewModel reportProgressTo);

        public virtual double GetMaximumProgress(int imageWidth, int imageHeight)
        {
            if (this.decoratingKernel != null)
                return decoratingKernel.GetMaximumProgress(imageWidth, imageHeight);

            return 0.0;
        }

        protected int NormalizeColorSpace(float toNormalize)
        {
            float maxrange = 0;
            float minrange = 0;
            for (int i = 0; i < this.width; i++)
            {
                for (int j = 0; j < this.height; j++)
                {
                    if (weights[i, j] > 0)
                    {
                        maxrange += weights[i, j];
                    }
                    else
                    {
                        minrange += weights[i, j];
                    }
                }
            }
            maxrange *= 255;
            minrange *= 255;
            float norm = (maxrange - minrange) / 255;
            return (int)Math.Floor((toNormalize - minrange) / norm);
        }
    }
}