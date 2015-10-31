using INFOIBV.Presentation;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace INFOIBV.Filters
{
    public class ErosionKernel : BasicKernel
    {
        // : base(decoratingKernel, 3, 3, ErosionOperation.constructWeights()) 
        public ErosionKernel(IApplicableFilter decoratingFilter)
            : base(decoratingFilter, 3, 3, ErosionKernel.constructWeights())
        {
        }

        public override Color[,] apply(Color[,] imageToProcess, MainViewModel reportProgressTo)
        {
            if (decoratingKernel != null)
            {
                imageToProcess = decoratingKernel.apply(imageToProcess, reportProgressTo);
            }

            Color[,] endResultImage = new Color[imageToProcess.GetLength(0), imageToProcess.GetLength(1)];
            Array.Copy(imageToProcess, endResultImage, imageToProcess.GetLength(0) * imageToProcess.GetLength(1));

            int xOffset = (this.width - 1) / 2;
            int yOffset = (this.height - 1) / 2;

            for (int x = xOffset; x < imageToProcess.GetLength(0) - xOffset; x++) // GetLength(x), where x is the dimension, give you the length of the specified part of the array.
            {
                for (int y = yOffset; y < imageToProcess.GetLength(1) - yOffset; y++)
                {
                    int sum = processPixel(x, y, imageToProcess, reportProgressTo);
                    //sum = sum < 0 ? 0 : sum > 255 ? 255 : sum;
                    endResultImage[x, y] = Color.FromArgb(sum, sum, sum);
                }
            }

            return endResultImage;
        }

        public override int processPixel(int xCoordinate, int yCoordinate, Color[,] imageToProcess, MainViewModel reportProgressTo)
        {
            float sum = 0;
            int midX = (this.width - 1) / 2;
            int midY = (this.height - 1) / 2;

            // Loop over Weights
            for (int x = 0; x < this.width; x++)
            {
                int xOffset = x - midX;
                for (int y = 0; y < this.height; y++)
                {
                    int yOffset = y - midY;
                    // Need to double-check if the objects are black or white

                    // Is image * weights bigger than 1 every coordinate?
                    float result = imageToProcess[xCoordinate + xOffset, yCoordinate + yOffset].R * weights[x, y];
                    if(result >= 1){
                        sum += 1;
                    } 
                    // can be optimized by skipping the rest of the operations if one of the if-statements fails
                    // just need to calculate and move the progressbar accordingly.
                    reportProgressTo.Progress++;
                }
            }
            if (sum >= 9) // If the kernel fits in the object, the middle pixel needs to turn black
            {
                return 255;
            }
            else
            {
                return 0;
            }

        }


        public override double GetMaximumProgress(int imageWidth, int imageHeight)
        {
            return base.GetMaximumProgress(imageWidth, imageHeight) + (((imageWidth - (this.width - 1)) * (imageHeight - (this.height - 1))) * (this.width * this.height));
        }

        private static float[,] constructWeights()
        {
            float[,] toReturn = new float[3, 3];
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    toReturn[x, y] = 1.0f;
                }
            }
            return toReturn;
        }

    }
}
