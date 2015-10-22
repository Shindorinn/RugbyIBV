using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace INFOIBV.Filters
{
    public abstract class BasicFilter
    {
        protected BasicFilter toDecorate;

        public BasicFilter(BasicFilter toDecorate)
        {
            this.toDecorate = toDecorate;
        }

        public abstract void apply(Bitmap imageToProcess);
    }
}
