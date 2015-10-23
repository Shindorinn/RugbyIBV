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
        Color[,] apply(Color[,] imageToProcess, MainViewModel reportProgressTo);
        double GetMaximumProgress(int imageWidth, int imageHeight);
    }
}
