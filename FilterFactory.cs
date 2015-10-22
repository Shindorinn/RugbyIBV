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
                    case FilterType.Type.HighPassFilter32:
                        toBeDecoratedFilter = new HighPassFilter(toBeDecoratedFilter, 32);
                        break;
                    case FilterType.Type.HighPassFilter64:
                        toBeDecoratedFilter = new HighPassFilter(toBeDecoratedFilter, 64);
                        break;
                    case FilterType.Type.HighPassFilter96:
                        toBeDecoratedFilter = new HighPassFilter(toBeDecoratedFilter, 96);
                        break;
                    case FilterType.Type.HighPassFilter128:
                        toBeDecoratedFilter = new HighPassFilter(toBeDecoratedFilter, 128);
                        break;
                    case FilterType.Type.HighPassFilter160:
                        toBeDecoratedFilter = new HighPassFilter(toBeDecoratedFilter, 160);
                        break;
                    case FilterType.Type.HighPassFilter192:
                        toBeDecoratedFilter = new HighPassFilter(toBeDecoratedFilter, 192);
                        break;
                    case FilterType.Type.HighPassFilter224:
                        toBeDecoratedFilter = new HighPassFilter(toBeDecoratedFilter, 224);
                        break;
                    case FilterType.Type.HighPassFilter256:
                        toBeDecoratedFilter = new HighPassFilter(toBeDecoratedFilter, 254);
                        break;
                    case FilterType.Type.LowPassFilter32:
                        toBeDecoratedFilter = new LowPassFilter(toBeDecoratedFilter, 32);
                        break;
                    case FilterType.Type.LowPassFilter64:
                        toBeDecoratedFilter = new LowPassFilter(toBeDecoratedFilter, 64);
                        break;
                    case FilterType.Type.LowPassFilter96:
                        toBeDecoratedFilter = new LowPassFilter(toBeDecoratedFilter, 96);
                        break;
                    case FilterType.Type.LowPassFilter128:
                        toBeDecoratedFilter = new LowPassFilter(toBeDecoratedFilter, 128);
                        break;
                    case FilterType.Type.LowPassFilter160:
                        toBeDecoratedFilter = new LowPassFilter(toBeDecoratedFilter, 160);
                        break;
                    case FilterType.Type.LowPassFilter192:
                        toBeDecoratedFilter = new LowPassFilter(toBeDecoratedFilter, 192);
                        break;
                    case FilterType.Type.LowPassFilter224:
                        toBeDecoratedFilter = new LowPassFilter(toBeDecoratedFilter, 224);
                        break;
                    case FilterType.Type.LowPassFilter256:
                        toBeDecoratedFilter = new LowPassFilter(toBeDecoratedFilter, 255);
                        break;
                    case FilterType.Type.ThresholdFilter32:
                        toBeDecoratedFilter = new ThresholdFilter(toBeDecoratedFilter, 32);
                        break;
                    case FilterType.Type.ThresholdFilter64:
                        toBeDecoratedFilter = new ThresholdFilter(toBeDecoratedFilter, 64);
                        break;
                    case FilterType.Type.ThresholdFilter96:
                        toBeDecoratedFilter = new ThresholdFilter(toBeDecoratedFilter, 96);
                        break;
                    case FilterType.Type.ThresholdFilter128:
                        toBeDecoratedFilter = new ThresholdFilter(toBeDecoratedFilter, 128);
                        break;
                    case FilterType.Type.ThresholdFilter160:
                        toBeDecoratedFilter = new ThresholdFilter(toBeDecoratedFilter, 160);
                        break;
                    case FilterType.Type.ThresholdFilter192:
                        toBeDecoratedFilter = new ThresholdFilter(toBeDecoratedFilter, 192);
                        break;
                    case FilterType.Type.ThresholdFilter224:
                        toBeDecoratedFilter = new ThresholdFilter(toBeDecoratedFilter, 224);
                        break;
                    case FilterType.Type.ThresholdFilter256:
                        toBeDecoratedFilter = new ThresholdFilter(toBeDecoratedFilter, 254);
                        break;
                    default:
                        break;
                }
            }

            return toBeDecoratedFilter;
        }

    }
}

/*
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
*/