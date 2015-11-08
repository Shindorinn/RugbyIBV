using INFOIBV.Presentation;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace INFOIBV.Filters
{
    public class FillObjectFilter : BasicFilter
    {
        public FillObjectFilter(IApplicableFilter decorator) : base(decorator) { }

        public override Color[,] apply(Color[,] imageToProcess, MainViewModel reportProgressTo)
        {
            imageToProcess = base.apply(imageToProcess, reportProgressTo);

            for (int i = 0; i < imageToProcess.GetLength(0); i++)
            {
                int firstPixelY = -1;
                for (int j = 0; j < imageToProcess.GetLength(1); j++)
                {
                    if (imageToProcess[i, j].ToArgb() == Color.Black.ToArgb())
                        if (firstPixelY <= 0 || firstPixelY + 1 == j)
                            firstPixelY = j;
                        else
                        {
                            for (int k = firstPixelY + 1; k < j; k++)
                                imageToProcess[i, k] = Color.Black;

                            firstPixelY = -1;
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