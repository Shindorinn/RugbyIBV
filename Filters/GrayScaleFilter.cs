using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace INFOIBV.Filters
{
    public class GrayScaleFilter : BasicFilter
    {
        public GrayScaleFilter(IApplicableFilter decorator)
            : base(decorator)
        {
            // YOU MUST CONSTRUCT ADDITIONAL PYLONS!
        }

        public override void apply(Bitmap imageToProcess)
        {
            base.apply(imageToProcess);

            for (int x = 0; x < imageToProcess.Width; x++)
            {
                for (int y = 0; y < imageToProcess.Height; y++)
                {
                    imageToProcess.SetPixel(x, y, this.convertRGBtoGrayScale(imageToProcess.GetPixel(x, y)));
                }
            }
        }

        private Color convertRGBtoGrayScale(Color toConvert)
        { // https://en.wikipedia.org/wiki/Grayscale
            // Y = 0.2162R + 0.7152G + 0.0722B
            double weightR = 0.2162f;
            double weightG = 0.7152f;
            double weightB = 0.0722f;

            double y = toConvert.R * weightR + toConvert.G * weightG + toConvert.B * weightB;

            int grayValue = (int)Math.Floor(y);

            //Console.WriteLine("y = " + y + " , Floored : " + Math.Floor(y));
            //Console.Write("grayValue = " + grayValue);

            return Color.FromArgb(grayValue,grayValue,grayValue);
        }
    }
}
