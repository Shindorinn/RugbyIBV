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

        public virtual void apply(Bitmap imageToProcess)
        {
            if (this.decoratingFilter != null)
            {
                decoratingFilter.apply(imageToProcess);
            }
        }
    }
}
