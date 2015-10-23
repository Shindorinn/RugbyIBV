using INFOIBV.Presentation;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace INFOIBV.Filters
{
    public class GrayScaleFilter : BasicFilter
    {
        public GrayScaleFilter(IApplicableFilter decorator)
            : base(decorator)
        {
            // YOU MUST CONSTRUCT ADDITIONAL PYLONS!
        }

        public override Color[,] apply(Color[,] imageToProcess, MainViewModel reportProgressTo)
        {
            imageToProcess = base.apply(imageToProcess, reportProgressTo);

            for (int x = 0; x < imageToProcess.GetLength(0); x++)
            {
                for (int y = 0; y < imageToProcess.GetLength(1); y++)
                {
                    imageToProcess[x,y] = this.convertRGBtoGrayScale(imageToProcess[x, y]);
                    reportProgressTo.Progress++;
                }
            }

            return imageToProcess;
        }

        private Color convertRGBtoGrayScale(Color toConvert)
        { // https://en.wikipedia.org/wiki/Grayscale
            // Y = 0.2162R + 0.7152G + 0.0722B
            double weightR = 0.2162f;
            double weightG = 0.7152f;
            double weightB = 0.0722f;

            double y = toConvert.R * weightR + toConvert.G * weightG + toConvert.B * weightB;
            int grayValue = (int)Math.Floor(y);

            return Color.FromArgb(grayValue,grayValue,grayValue);
        }

        public override double GetMaximumProgress(int imageWidth, int imageHeight)
        {
            return base.GetMaximumProgress(imageWidth, imageHeight) + (imageWidth * imageHeight);
        }
    }
}