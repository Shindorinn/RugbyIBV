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
        
        public override double GetMaximumProgress(int imageWidth, int imageHeight)
        {
            return base.GetMaximumProgress(imageWidth, imageHeight) + (((imageWidth - (4)) * (imageHeight - (4))) * (25));
        }

        private static int getHeight(DerivativeType type)
        {
            switch (type)
            {
                case DerivativeType.x:
                    return 1;

                case DerivativeType.y:
                    return 5;

                case DerivativeType.xy:
                    return 3;
            }
            return -1; // It should never come to this!
        }

        private static int getWidth(DerivativeType type){
            switch (type)
            {
                case DerivativeType.x :
                    return 5;

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
                    return new float[5, 1] { { 1f / 12f }, { -8f / 12f }, { 0f }, { 8f / 12f }, { -1f / 12f } };

                case DerivativeType.y:
                    return new float[1, 5] { { 1f / 12f, -8 / 12f, 0f, 8 / 12f, -1 / 12f } };

                case DerivativeType.xy:
                    return new float[3, 3] { {1/4, 0, -1/4} , {0,0,0} , {-1/4, 0, 1/4} } ;
            }
            return null; // It should never come to this!
        }
    }
}
