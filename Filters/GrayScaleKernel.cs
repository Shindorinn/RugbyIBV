using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace INFOIBV.Filters
{
    public class GrayScaleKernel : BasicKernel
    {
        private double weightR = 0.2162f;
        private double weightG = 0.7152f;
        private double weightB = 0.0722f;

        public GrayScaleKernel(IApplicableKernel decoratingKernel, int width, int height)
            : base(decoratingKernel, width, height, GrayScaleKernel.constructWeights())
        {
            // THE END IS COMING!
        }

        public GrayScaleKernel(int width, int height)
            : this(null, width, height)
        {
            // Monochrome
        }


        public override int processPixel(int xCoordinate, int yCoordinate, Bitmap imageToProcess)
        {
            return this.convertRGBtoGrayScale(imageToProcess.GetPixel(xCoordinate, yCoordinate));
        }

        private int convertRGBtoGrayScale(Color toConvert)
        { // https://en.wikipedia.org/wiki/Grayscale
            // Y = 0.2162R + 0.7152G + 0.0722B
            double y = toConvert.R * weightR + toConvert.G * weightG + toConvert.B * weightB;

            //int grayValue = (int)Math.Floor(y);
            //grayValue
            return (int)Math.Floor(y); ;
        }

        private static float[,] constructWeights()
        {
            float[,] toReturn = new float[1, 1];
            toReturn[0, 0] = 1;
            return toReturn;
        }
    }
}
