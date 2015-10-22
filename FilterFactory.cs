using INFOIBV.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INFOIBV
{
    public static class FilterFactory
    {
        public static BasicFilter Construct(List<FilterType> filters)
        {
            BasicFilter toBeDecoratedFilter = null;

            foreach (FilterType filter in filters)
            {
                switch (filter.fType)
                {
                    case FilterType.Type.BasicKernel:
                        //toBeDecoratedFilter = new BasicKernel(3, 3, toBeDecoratedFilter);
                        break;
                    case FilterType.Type.GrayscaleFilter:
                        toBeDecoratedFilter = new GrayScaleFilter(toBeDecoratedFilter);
                        break;
                    case FilterType.Type.HighPassFilter:
                        toBeDecoratedFilter = new HighPassFilter(toBeDecoratedFilter, 128);
                        break;
                    default:
                        Console.WriteLine("Error \"{0}\" filter not found.", filter.fType);
                        break;
                }
            }


            return null;
        }

    }
}