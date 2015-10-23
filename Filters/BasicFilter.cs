using INFOIBV.Presentation;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace INFOIBV.Filters
{
    public abstract class BasicFilter : IApplicableFilter
    {
        protected IApplicableFilter decoratingFilter;

        public BasicFilter(IApplicableFilter toDecorate)
        {
            this.decoratingFilter = toDecorate;
        }

        public virtual Color[,] apply(Color[,] imageToProcess, MainViewModel reportProgressTo)
        {
            if (this.decoratingFilter != null)
            {
                return decoratingFilter.apply(imageToProcess, reportProgressTo);
            }

            return imageToProcess;
        }

        public virtual double GetMaximumProgress(int imageWidth, int imageHeight)
        {
            if (this.decoratingFilter != null)
                return decoratingFilter.GetMaximumProgress(imageWidth, imageHeight);

            return 0.0;
        }
    }
}