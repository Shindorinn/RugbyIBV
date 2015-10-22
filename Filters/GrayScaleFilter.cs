using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace INFOIBV.Filters
{
    public class GrayScaleFilter : BasicFilter
    {
        public override void apply(Bitmap imageToProcess)
        {
            base.apply(imageToProcess);

            for (int x = 0; x < imageToProcess.Width; x++)
            {
                for (int y = 0; y < imageToProcess.Height; y++)
                {
                    imageToProcess.SetPixel(x, y, this.convertRGBtoGrayScale(imageToProcess.GetPixel(x, y));
                }
            }
        }

        private Color convertRGBtoGrayScale(Color toConvert)
        {
            throw new NotImplementedException();
        }
    }
}
