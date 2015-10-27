using INFOIBV.Presentation;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace INFOIBV.Filters
{
    public class DerivativeKernel : BasicKernel
    {
        private AxisDirection direction;

        public DerivativeKernel(IApplicableFilter decoratingFilter, AxisDirection direction)
            : base(decoratingFilter, direction == AxisDirection.x ? 5 : 1, direction == AxisDirection.y ? 5 : 1, constructWeights(direction))
        {
            this.direction = direction;
        }

        public override int processPixel(int xCoordinate, int yCoordinate, Color[,] imageToProcess, MainViewModel reportProgressTo)
        {
            float sum = 0;
            int midX = (this.width - 1) / 2;
            int midY = (this.height - 1) / 2;

            // Loop over Weights
            for (int y = 0; y < this.height; y++)
            {
                int yOffset = y - midY;
                for (int x = 0; x < this.width; x++)
                {
                    int xOffset = x - midX;
                    sum += imageToProcess[xCoordinate + xOffset, yCoordinate + yOffset].R * weights[y, x];
                    reportProgressTo.Progress++;
                }
            }

            return (int)Math.Floor(sum);
        }

        private static float[,] constructWeights(AxisDirection direction)
        {
            if (direction == AxisDirection.x)
            {
                return new float[5, 1] { { 1f / 12f }, { -8f / 12f }, { 0f }, { 8f / 12f }, { -1f / 12f } };
            }
            else
            {
                return new float[1, 5] { { 1f / 12f ,  -8 / 12f ,  0f ,  8 / 12f ,  -1 / 12f } };
            }
        }
    }
}
