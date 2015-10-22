using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace INFOIBV.Filters
{
    public abstract class BasicFilter : IApplicableFilter
    {
        protected BasicFilter decoratingFilter;

        public BasicFilter(BasicFilter toDecorate)
        {
            this.decoratingFilter = toDecorate;
        }

        public void apply(Bitmap imageToProcess)
        {
            if (this.decoratingFilter != null)
            {
                decoratingFilter.apply(imageToProcess);
            }
        }
    }
}
