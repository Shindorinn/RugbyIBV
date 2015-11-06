using INFOIBV.Presentation;
using INFOIBV.Utilities.Enums;

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
            int i = 0;
            kernels = new BasicKernel[8];
            switch (type)
            {
                case CompassType.Approximation:
                    foreach (Direction compass in Enum.GetValues(typeof(Direction)))
                    {
                        kernels[i] = new ApproximationKernel(null, compass);
                        i++;
                    }
                    break;
                case CompassType.Isotropic:
                    foreach (Direction compass in Enum.GetValues(typeof(Direction)))
                    {
                        kernels[i] = new IsotropicKernel(null, compass);
                        i++;
                    }
                    break;
                case CompassType.Kirsch:
                    foreach (Direction compass in Enum.GetValues(typeof(Direction)))
                    {
                        kernels[i] = new KirschKernel(null, compass);
                        i++;
                    }
                    break;
                case CompassType.Prewitt:
                    foreach (Direction compass in Enum.GetValues(typeof(Direction)))
                    {
                        kernels[i] = new PrewittKernel(null, compass);
                        i++;
                    }
                    break;
                case CompassType.Sobel:
                    foreach (Direction compass in Enum.GetValues(typeof(Direction)))
                    {
                        kernels[i] = new SobelKernel(null, compass);
                        i++;
                    }
                    break;
                default:
                    throw new InvalidOperationException("You cannot perform a CompassOperation on the specified CompassType.");
            }
        }

        public override Color[,] apply(Color[,] imageToProcess, MainViewModel reportProgressTo)
        {
            imageToProcess = base.apply(imageToProcess, reportProgressTo);
            Color[,] imageToReturn = new Color[imageToProcess.GetLength(0), imageToProcess.GetLength(1)];
            Array.Copy(imageToProcess, imageToReturn, imageToProcess.GetLength(0) * imageToProcess.GetLength(1));
            int xOffset = 1;
            int yOffset = 1;

            for (int x = xOffset; x < imageToProcess.GetLength(0) - xOffset; x++)
            {
                for (int y = yOffset; y < imageToProcess.GetLength(1) - yOffset; y++)
                {
                    int highestValue = 0;
                    foreach (BasicKernel kernel in kernels)
                    {
                        int kernelValue = kernel.processPixel(x, y, imageToProcess, reportProgressTo);
                        kernelValue = kernelValue < 0 ? 0 : kernelValue > 255 ? 255 : kernelValue;
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