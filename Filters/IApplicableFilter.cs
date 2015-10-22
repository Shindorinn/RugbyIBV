using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace INFOIBV.Filters
{
    public interface IApplicableFilter
    {
        void apply(Bitmap imageToProcess);
    }
}
