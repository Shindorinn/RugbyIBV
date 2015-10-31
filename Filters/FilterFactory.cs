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
                    case FilterType.Type.ErosionKernel:
                        toBeDecoratedFilter = new ErosionKernel(toBeDecoratedFilter);
                        break;

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
                        toBeDecoratedFilter = new DerivativeKernel(toBeDecoratedFilter, DerivativeType.x);
                        break;
                    case FilterType.Type.DerivativeKernelY:
                        toBeDecoratedFilter = new DerivativeKernel(toBeDecoratedFilter, DerivativeType.y);
                        break;
                    case FilterType.Type.DerivativeKernelXY:
                        toBeDecoratedFilter = new DerivativeKernel(toBeDecoratedFilter, DerivativeType.xy);
                        break;

                    case FilterType.Type.ApproximationCompassOperation:
                        toBeDecoratedFilter = new CompassOperation(toBeDecoratedFilter, CompassType.Approximation);
                        break;
                    case FilterType.Type.IsotropicCompassOperation:
                        toBeDecoratedFilter = new CompassOperation(toBeDecoratedFilter, CompassType.Isotropic);
                        break;
                    case FilterType.Type.KirschCompassOperation:
                        toBeDecoratedFilter = new CompassOperation(toBeDecoratedFilter, CompassType.Kirsch);
                        break;
                    case FilterType.Type.PrewittCompassOperation:
                        toBeDecoratedFilter = new CompassOperation(toBeDecoratedFilter, CompassType.Prewitt);
                        break;
                    case FilterType.Type.SobelCompassOperation:
                        toBeDecoratedFilter = new CompassOperation(toBeDecoratedFilter, CompassType.Sobel);
                        break;

                    //case FilterType.Type.PrewittKernelN:
                    //    toBeDecoratedFilter = new PrewittKernel(toBeDecoratedFilter, Direction.North);
                    //    break;
                    //case FilterType.Type.PrewittKernelNE:
                    //    toBeDecoratedFilter = new PrewittKernel(toBeDecoratedFilter, Direction.NorthEast);
                    //    break;
                    //case FilterType.Type.PrewittKernelE:
                    //    toBeDecoratedFilter = new PrewittKernel(toBeDecoratedFilter, Direction.East);
                    //    break;
                    //case FilterType.Type.PrewittKernelSE:
                    //    toBeDecoratedFilter = new PrewittKernel(toBeDecoratedFilter, Direction.SouthEast);
                    //    break;
                    //case FilterType.Type.PrewittKernelS:
                    //    toBeDecoratedFilter = new PrewittKernel(toBeDecoratedFilter, Direction.South);
                    //    break;
                    //case FilterType.Type.PrewittKernelSW:
                    //    toBeDecoratedFilter = new PrewittKernel(toBeDecoratedFilter, Direction.SouthWest);
                    //    break;
                    //case FilterType.Type.PrewittKernelW:
                    //    toBeDecoratedFilter = new PrewittKernel(toBeDecoratedFilter, Direction.West);
                    //    break;
                    //case FilterType.Type.PrewittKernelNW:
                    //    toBeDecoratedFilter = new PrewittKernel(toBeDecoratedFilter, Direction.NorthWest);
                    //    break;

                    //case FilterType.Type.ApproximationKernelN:
                    //    toBeDecoratedFilter = new ApproximationKernel(toBeDecoratedFilter, Direction.North);
                    //    break;
                    //case FilterType.Type.ApproximationKernelNE:
                    //    toBeDecoratedFilter = new ApproximationKernel(toBeDecoratedFilter, Direction.NorthEast);
                    //    break;
                    //case FilterType.Type.ApproximationKernelE:
                    //    toBeDecoratedFilter = new ApproximationKernel(toBeDecoratedFilter, Direction.East);
                    //    break;
                    //case FilterType.Type.ApproximationKernelSE:
                    //    toBeDecoratedFilter = new ApproximationKernel(toBeDecoratedFilter, Direction.SouthEast);
                    //    break;
                    //case FilterType.Type.ApproximationKernelS:
                    //    toBeDecoratedFilter = new ApproximationKernel(toBeDecoratedFilter, Direction.South);
                    //    break;
                    //case FilterType.Type.ApproximationKernelSW:
                    //    toBeDecoratedFilter = new ApproximationKernel(toBeDecoratedFilter, Direction.SouthWest);
                    //    break;
                    //case FilterType.Type.ApproximationKernelW:
                    //    toBeDecoratedFilter = new ApproximationKernel(toBeDecoratedFilter, Direction.West);
                    //    break;
                    //case FilterType.Type.ApproximationKernelNW:
                    //    toBeDecoratedFilter = new ApproximationKernel(toBeDecoratedFilter, Direction.NorthWest);
                    //    break;

                    //case FilterType.Type.IsotropicKernelN:
                    //    toBeDecoratedFilter = new IsotropicKernel(toBeDecoratedFilter, Direction.North);
                    //    break;
                    //case FilterType.Type.IsotropicKernelNE:
                    //    toBeDecoratedFilter = new IsotropicKernel(toBeDecoratedFilter, Direction.NorthEast);
                    //    break;
                    //case FilterType.Type.IsotropicKernelE:
                    //    toBeDecoratedFilter = new IsotropicKernel(toBeDecoratedFilter, Direction.East);
                    //    break;
                    //case FilterType.Type.IsotropicKernelSE:
                    //    toBeDecoratedFilter = new IsotropicKernel(toBeDecoratedFilter, Direction.SouthEast);
                    //    break;
                    //case FilterType.Type.IsotropicKernelS:
                    //    toBeDecoratedFilter = new IsotropicKernel(toBeDecoratedFilter, Direction.South);
                    //    break;
                    //case FilterType.Type.IsotropicKernelSW:
                    //    toBeDecoratedFilter = new IsotropicKernel(toBeDecoratedFilter, Direction.SouthWest);
                    //    break;
                    //case FilterType.Type.IsotropicKernelW:
                    //    toBeDecoratedFilter = new IsotropicKernel(toBeDecoratedFilter, Direction.West);
                    //    break;
                    //case FilterType.Type.IsotropicKernelNW:
                    //    toBeDecoratedFilter = new IsotropicKernel(toBeDecoratedFilter, Direction.NorthWest);
                    //    break;

                    //case FilterType.Type.SobelKernelN:
                    //    toBeDecoratedFilter = new SobelKernel(toBeDecoratedFilter, Direction.North);
                    //    break;
                    //case FilterType.Type.SobelKernelNE:
                    //    toBeDecoratedFilter = new SobelKernel(toBeDecoratedFilter, Direction.NorthEast);
                    //    break;
                    //case FilterType.Type.SobelKernelE:
                    //    toBeDecoratedFilter = new SobelKernel(toBeDecoratedFilter, Direction.East);
                    //    break;
                    //case FilterType.Type.SobelKernelSE:
                    //    toBeDecoratedFilter = new SobelKernel(toBeDecoratedFilter, Direction.SouthEast);
                    //    break;
                    //case FilterType.Type.SobelKernelS:
                    //    toBeDecoratedFilter = new SobelKernel(toBeDecoratedFilter, Direction.South);
                    //    break;
                    //case FilterType.Type.SobelKernelSW:
                    //    toBeDecoratedFilter = new SobelKernel(toBeDecoratedFilter, Direction.SouthWest);
                    //    break;
                    //case FilterType.Type.SobelKernelW:
                    //    toBeDecoratedFilter = new SobelKernel(toBeDecoratedFilter, Direction.West);
                    //    break;
                    //case FilterType.Type.SobelKernelNW:
                    //    toBeDecoratedFilter = new SobelKernel(toBeDecoratedFilter, Direction.NorthWest);
                    //    break;

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