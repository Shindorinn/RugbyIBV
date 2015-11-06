using INFOIBV.Presentation;
using INFOIBV.Utilities.Enums;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace INFOIBV.Filters
{
    public class DerivativeKernel : BasicKernel
    {
        private DerivativeType type;

        public DerivativeKernel(IApplicableFilter decoratingFilter, DerivativeType type)
            : base(decoratingFilter, DerivativeKernel.getWidth(type), DerivativeKernel.getHeight(type), constructWeights(type))
        {
            this.type = type;
        }

        public override int processPixel(int xCoordinate, int yCoordinate, Color[,] imageToProcess, MainViewModel reportProgressTo)
        {
            float sum = 0;
            int midX = (this.width - 1) / 2;
            int midY = (this.height - 1) / 2;

            // Loop over Weights to calculate the convolution
            for (int x = 0; x < this.width; x++)
            {
                int xOffset = x - midX;
                for (int y = 0; y < this.height; y++)
                {
                    int yOffset = y - midY;
                    sum += imageToProcess[xCoordinate + xOffset, yCoordinate + yOffset].R * weights[x, y];
                    reportProgressTo.Progress++;
                }
            }

            return base.NormalizeColorSpace(sum);

        }

        public override double GetMaximumProgress(int imageWidth, int imageHeight)
        {
            if (this.type == DerivativeType.xy)
            { // 3x3
                return base.GetMaximumProgress(imageWidth, imageHeight) + (((imageWidth - (2)) * (imageHeight - (2))) * (9));
            }
            else
            { // 1x5 or 5x1
                return base.GetMaximumProgress(imageWidth, imageHeight) + (((imageWidth - (4)) * (imageHeight - (4))) * (5));
            }

        }

        private static int getHeight(DerivativeType type)
        {
            switch (type)
            {
                case DerivativeType.x:
                    return 1;

                case DerivativeType.y:
                    //return 5;
                    return 3;

                case DerivativeType.xy:
                    return 3;
            }
            return -1; // It should never come to this!
        }

        private static int getWidth(DerivativeType type)
        {
            switch (type)
            {
                case DerivativeType.x:
                    //return 5;
                    return 3;

                case DerivativeType.y:
                    return 1;

                case DerivativeType.xy:
                    return 3;
            }
            return -1; // It should never come to this!
        }

        private static float[,] constructWeights(DerivativeType type)
        {
            switch (type)
            {
                case DerivativeType.x:
                    //return new float[5, 1] { { 1f / 12f }, { -8f / 12f }, { 0f }, { 8f / 12f }, { -1f / 12f } };
                    return new float[3, 1] { { 1f },{ 0f },{ -1f } };

                case DerivativeType.y:
                    //return new float[1, 5] { { 1f / 12f, -8f / 12f, 0f, 8f / 12f, -1f / 12f } };
                    return new float[1, 3] { { 1f, 0f, -1f } };

                case DerivativeType.xy:
                    return new float[3, 3] { { 1f / 4f, 0f, -1f / 4f }, { 0f, 0f, 0f }, { -1f / 4f, 0f, 1f / 4f } };
            }
            return null; // It should never come to this!
        }
    }
}
