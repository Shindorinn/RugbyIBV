using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace INFOIBV.Filters
{
    public class HighPassFilter : BasicFilter
    {
        private int thresholdValue;

        public HighPassFilter(BasicFilter toDecorate, int thresholdValue) : base(toDecorate){
            this.thresholdValue = thresholdValue;
        }

        public override void apply(Bitmap imageToProcess)
        {
            base.apply(imageToProcess);
            for(int x = 0; x < imageToProcess.Width; x++){
                for(int y = 0; y < imageToProcess.Height; y++){
                    if (imageToProcess.GetPixel(x, y).R < this.thresholdValue)
                    {
                        imageToProcess.SetPixel(x, y, Color.Black);
                    }
                }
            }
            
        }
    }

}
