using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INFOIBV.Filters
{
    public static class FilterFactory
    {
        public static IApplicableFilter Construct(List<FilterType> filters)
        {
            IApplicableFilter toBeDecoratedFilter = null;

            foreach (FilterType filter in filters)
            {
                switch (filter.fType)
                {
                    case FilterType.Type.GrayscaleFilter:
                        toBeDecoratedFilter = new GrayScaleFilter(toBeDecoratedFilter);
                        break;
                    case FilterType.Type.GrayscaleKernel:
                        toBeDecoratedFilter = new GrayScaleKernel(toBeDecoratedFilter);
                        break;
                    case FilterType.Type.DoNothingKernel:
                        toBeDecoratedFilter = new DoNothingKernel(toBeDecoratedFilter, 3, 3);
                        break;

                    case FilterType.Type.DerivativeKernelX:
                        toBeDecoratedFilter = new DerivativeKernel(toBeDecoratedFilter, AxisDirection.x);
                        break;
                    case FilterType.Type.DerivativeKernelY:
                        toBeDecoratedFilter = new DerivativeKernel(toBeDecoratedFilter, AxisDirection.y);
                        break;

                    case FilterType.Type.NormalizingKernel3:
                        toBeDecoratedFilter = new NormalizingKernel(toBeDecoratedFilter, 3, 3);
                        break;
                    case FilterType.Type.NormalizingKernel5:
                        toBeDecoratedFilter = new NormalizingKernel(toBeDecoratedFilter, 5, 5);
                        break;
                    case FilterType.Type.NormalizingKernel7:
                        toBeDecoratedFilter = new NormalizingKernel(toBeDecoratedFilter, 7, 7);
                        break;
                    case FilterType.Type.NormalizingKernel1x25:
                        toBeDecoratedFilter = new NormalizingKernel(toBeDecoratedFilter, 1, 25);
                        break;
                    case FilterType.Type.NormalizingKernel25x1:
                        toBeDecoratedFilter = new NormalizingKernel(toBeDecoratedFilter, 25, 1);
                        break;
                    case FilterType.Type.NormalizingKernel25:
                        toBeDecoratedFilter = new NormalizingKernel(toBeDecoratedFilter, 25, 25);
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