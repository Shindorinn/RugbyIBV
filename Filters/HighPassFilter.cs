using INFOIBV.Presentation;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace INFOIBV.Filters
{
    public class HighPassFilter : BasicFilter
    {
        private int thresholdValue;

        public HighPassFilter(IApplicableFilter toDecorate, int thresholdValue)
            : base(toDecorate)
        {
            this.thresholdValue = thresholdValue;
        }

        public override void apply(Bitmap imageToProcess, MainViewModel reportProgressTo)
        {
            base.apply(imageToProcess, reportProgressTo);
            for (int x = 0; x < imageToProcess.Width; x++)
            {
                for (int y = 0; y < imageToProcess.Height; y++)
                {
                    if (imageToProcess.GetPixel(x, y).R < this.thresholdValue)
                    {
                        imageToProcess.SetPixel(x, y, Color.Black);
                        reportProgressTo.Progress++;
                    }
                }
            }

        }

        public override double GetMaximumProgress(int imageWidth, int imageHeight)
        {
            return base.GetMaximumProgress(imageWidth, imageHeight) + (imageWidth * imageHeight);
        }
    }

}
