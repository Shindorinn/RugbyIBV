using INFOIBV.Presentation;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace INFOIBV.Filters
{
    /// <summary>
    /// This filter uses 8 kernels to process the image.
    /// </summary>
    public class CompassOperation : BasicFilter
    {
        private BasicKernel[] kernels;

        public CompassOperation(IApplicableFilter decorator, CompassType type)
            : base(decorator)
        {
            ConstructKernels(type);
            // A mole can dig a tunnel 300 feet long in just one night
        }

        private void ConstructKernels(CompassType type)
        {
            kernels = new BasicKernel[8];
            switch (type)
            {
                case CompassType.Approximation:
                    for (int i = 0; i < kernels.Length; i++)
                        foreach (Direction compass in Enum.GetValues(typeof(Direction)))
                            kernels[i] = new ApproximationKernel(null, compass);
                    break;
                case CompassType.Isotropic:
                    for (int i = 0; i < kernels.Length; i++)
                        foreach (Direction compass in Enum.GetValues(typeof(Direction)))
                            kernels[i] = new ApproximationKernel(null, compass);
                    break;
                case CompassType.Kirsch:
                    for (int i = 0; i < kernels.Length; i++)
                        foreach (Direction compass in Enum.GetValues(typeof(Direction)))
                            kernels[i] = new ApproximationKernel(null, compass);
                    break;
                case CompassType.Prewitt:
                    for (int i = 0; i < kernels.Length; i++)
                        foreach (Direction compass in Enum.GetValues(typeof(Direction)))
                            kernels[i] = new ApproximationKernel(null, compass);
                    break;
                case CompassType.Sobel:
                    for (int i = 0; i < kernels.Length; i++)
                        foreach (Direction compass in Enum.GetValues(typeof(Direction)))
                            kernels[i] = new ApproximationKernel(null, compass);
                    break;
                default:
                    throw new InvalidOperationException("You cannot perform a CompassOperation on the specified CompassType.");
            }
        }

        public override Color[,] apply(Color[,] imageToProcess, MainViewModel reportProgressTo)
        {
            imageToProcess = base.apply(imageToProcess, reportProgressTo);
            Color[,] imageToReturn = imageToProcess;
            int xOffset = 1;
            int yOffset = 1;

            for (int y = yOffset; y < imageToProcess.GetLength(0) - yOffset; y++)
            {
                for (int x = xOffset; x < imageToProcess.GetLength(1) - xOffset; x++)
                {
                    int highestValue = -1;
                    foreach (BasicKernel kernel in kernels)
                    {
                        int kernelValue = kernel.processPixel(x, y, imageToProcess, reportProgressTo);
                        if (highestValue < kernelValue)
                            highestValue = kernelValue;
                    }
                    imageToReturn[x, y] = Color.FromArgb(highestValue, highestValue, highestValue);
                }
            }

            return imageToReturn;
        }

        public override double GetMaximumProgress(int imageWidth, int imageHeight)
        {
            return base.GetMaximumProgress(imageWidth, imageHeight) + (((imageWidth - 2) * (imageHeight - 2)) * 72); // 9 * 8 (9 size of the kernels and 8 for # of kernels)
        }
    }
}