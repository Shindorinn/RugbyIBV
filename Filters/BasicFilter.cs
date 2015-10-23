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

        public virtual void apply(Bitmap imageToProcess, MainViewModel reportProgressTo)
        {
            if (this.decoratingFilter != null)
            {
                decoratingFilter.apply(imageToProcess, reportProgressTo);
            }
        }

        public virtual double GetMaximumProgress(int imageWidth, int imageHeight) // Needs to be implemented by every class.
        {
            if (this.decoratingFilter != null)
                return decoratingFilter.GetMaximumProgress(imageWidth, imageHeight);

            return 0.0;
        }
    }
}
