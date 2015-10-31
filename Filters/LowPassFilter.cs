using INFOIBV.Presentation;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace INFOIBV.Filters
{
    public class LowPassFilter : BasicFilter
    {
        private int thresholdValue;

        public LowPassFilter(IApplicableFilter toDecorate, int thresholdValue)
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
                    if (imageToProcess[x, y].R > this.thresholdValue)
                    {
                        imageToProcess[x, y] = Color.White;
                    }
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