using INFOIBV.Presentation;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace INFOIBV.Filters
{
    public class ThresholdFilter : BasicFilter
    {
        private int thresholdValue;

        public ThresholdFilter(IApplicableFilter toDecorate, int thresholdValue)
            : base(toDecorate)
        {
            this.thresholdValue = thresholdValue;
        }

        public override Color[,] apply(Color[,] imageToProcess, MainViewModel reportProgressTo)
        {
            imageToProcess = base.apply(imageToProcess, reportProgressTo);
            for (int x = 0; x < imageToProcess.GetLength(0); x++)
            {
                for (int y = 0; y < imageToProcess.GetLength(1); y++)
                {
                    imageToProcess[x, y] = imageToProcess[x, y].R < this.thresholdValue ? Color.Black : Color.White;
                    reportProgressTo.Progress++;
                }
            }

            return imageToProcess;
        }

        public override double GetMaximumProgress(int imageWidth, int imageHeight)
        {
            return base.GetMaximumProgress(imageWidth, imageHeight) + (imageWidth * imageHeight);
        }
    }
}