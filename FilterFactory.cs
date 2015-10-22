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
            foreach (FilterType filter in filters)
            {
                switch (filter.fType)
                {
                    case FilterType.Type.SmallKernel:
                        break;
                    case FilterType.Type.MediumKernel:
                        break;
                    case FilterType.Type.BlurryFilter:
                        break;
                    case FilterType.Type.HighPassFilter:
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