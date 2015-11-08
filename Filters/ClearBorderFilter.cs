using INFOIBV.Presentation;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace INFOIBV.Filters
{
    public class ClearBorderFilter : BasicFilter
    {
        public ClearBorderFilter(IApplicableFilter decorator) : base(decorator) { }

        public override Color[,] apply(Color[,] imageToProcess, MainViewModel reportProgressTo)
        {
            imageToProcess = base.apply(imageToProcess, reportProgressTo);

            for (int i = 0; i < imageToProcess.GetLength(0); i++)
            {
                imageToProcess[i, 0] = Color.White;
                imageToProcess[i, imageToProcess.GetLength(1) - 1] = Color.White;

                reportProgressTo.Progress++;
            }

            for (int i = 0; i < imageToProcess.GetLength(1); i++)
            {
                imageToProcess[0, i] = Color.White;
                imageToProcess[imageToProcess.GetLength(0) - 1, i] = Color.White;

                reportProgressTo.Progress++;
            }
            return imageToProcess;
        }

        public override double GetMaximumProgress(int imageWidth, int imageHeight)
        {
            return base.GetMaximumProgress(imageWidth, imageHeight) + (imageWidth + imageHeight);
        }
    }
}
