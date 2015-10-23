using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace INFOIBV.Filters
{
   public class ThresholdFilter : BasicFilter
    {
       private int thresholdValue;

       public ThresholdFilter(IApplicableFilter toDecorate, int thresholdValue)
           : base(toDecorate)
       {
           this.thresholdValue = thresholdValue;
       }

       public override void apply(Bitmap imageToProcess)
       {
           base.apply(imageToProcess);
           for (int x = 0; x < imageToProcess.Width; x++)
           {
               for (int y = 0; y < imageToProcess.Height; y++)
               {
                   if (imageToProcess.GetPixel(x, y).R < this.thresholdValue)
                   {
                       imageToProcess.SetPixel(x, y, Color.Black);
                   }
                   else
                   {
                       imageToProcess.SetPixel(x, y, Color.White);
                   }
               }
           }

       }
    }
}
