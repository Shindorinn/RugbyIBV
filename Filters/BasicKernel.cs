using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace INFOIBV.Filters
{
    public abstract class BasicKernel
    {
        private int width;
        private int height;

        public BasicKernel(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public abstract void apply(Bitmap imageToProcess);

    }
}
