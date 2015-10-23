using INFOIBV.Presentation;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace INFOIBV.Filters
{
    public class GrayScaleKernel : BasicKernel
    {
        private readonly double weightR = 0.2162f;
        private readonly double weightG = 0.7152f;
        private readonly double weightB = 0.0722f;

        public GrayScaleKernel(IApplicableFilter decoratingKernel, int width = 1, int height = 1)
            : base(decoratingKernel, width, height, GrayScaleKernel.constructWeights())
        {
            // THE END IS COMING!
        }

        public GrayScaleKernel(int width, int height)
            : this(null, width, height)
        {
            // Monochrome
        }

        public override int processPixel(int xCoordinate, int yCoordinate, Color[,] imageToProcess, MainViewModel reportProgressTo)
        { // https://en.wikipedia.org/wiki/Grayscale
            Color toConvert = imageToProcess[xCoordinate, yCoordinate];

            // Y = 0.2162R + 0.7152G + 0.0722B
            // Kernel doorlopen?
            double y = toConvert.R * weightR + toConvert.G * weightG + toConvert.B * weightB;
            reportProgressTo.Progress++;

            return (int)Math.Floor(y);
        }

        private static float[,] constructWeights()
        {
            float[,] toReturn = new float[1, 1];
            toReturn[0, 0] = 1;
            return toReturn;
        }

        public override double GetMaximumProgress(int imageWidth, int imageHeight)
        {
            return base.GetMaximumProgress(imageWidth, imageHeight) + (imageWidth * imageHeight); // * 1 (1x1 kernel)
        }
    }
}
