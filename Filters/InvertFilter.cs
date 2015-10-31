using INFOIBV.Presentation;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace INFOIBV.Filters
{
    public class InvertFilter : BasicFilter
    {
        public InvertFilter(IApplicableFilter decorator)
            : base(decorator)
        {
            // You need more overlords!
        }

        public override Color[,] apply(Color[,] imageToProcess, MainViewModel reportProgressTo)
        {
            imageToProcess = base.apply(imageToProcess, reportProgressTo);

            for (int x = 0; x < imageToProcess.GetLength(0); x++)
            {
                for (int y = 0; y < imageToProcess.GetLength(1); y++)
                {
                    imageToProcess[x,y] = InvertColor(imageToProcess[x, y]);
                    reportProgressTo.Progress++;
                }
            }

            return imageToProcess;
        }

        private Color InvertColor(Color toConvert)
        {
            return Color.FromArgb(255 - toConvert.R, 255 - toConvert.G, 255 - toConvert.B);
        }

        public override double GetMaximumProgress(int imageWidth, int imageHeight)
        {
            return base.GetMaximumProgress(imageWidth, imageHeight) + (imageWidth * imageHeight);
        }
    }
}