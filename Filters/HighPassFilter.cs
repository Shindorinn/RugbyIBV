using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace INFOIBV.Filters
{
    public class HighPassFilter : BasicKernel
    {
        private int thresholdValue;

        public HighPassFilter(int thresholdValue) : base(1, 1){
            this.thresholdValue = thresholdValue;
        }

        public override void apply(Bitmap imageToProcess)
        {
            if (!(imageToProcess.Height >= this.height) &&
                !(imageToProcess.Width >= this.width))
            {
                // The filter cannot be applied
            }

            for(int x = 0; x < imageToProcess.Width; x++){
                for(int y = 0; y < imageToProcess.Height; y++){
                    // We need to see how to compare colour of a pixel to the thresholdvalue
                    //if (imageToProcess.GetPixel(x, y) < this.thresholdValue)
                    //{
                    //    imageToProcess.SetPixel(x, y, Color.Black);
                    //}
                }
            }
            
        }

        protected override float[,] initializeWeights(int width, int height)
        {
            throw new NotImplementedException();
        }
    }

}
