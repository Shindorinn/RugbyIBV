using INFOIBV.Presentation;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace INFOIBV.Filters
{
    public interface IApplicableFilter
    {
        void apply(Bitmap imageToProcess, MainViewModel reportProgressTo);
        double GetMaximumProgress(int imageWidth, int imageHeight);
    }
}
