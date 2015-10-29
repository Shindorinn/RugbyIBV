using INFOIBV.Presentation;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace INFOIBV.Filters
{
    public class Hamiltonian : BasicFilter
    {
        public Hamiltonian(IApplicableFilter toDecorate)
            : base(toDecorate)
        {

        }

        public override Color[,] apply(Color[,] imageToProcess, MainViewModel reportProgressTo)
        {
            DerivativeKernel xDerivative = new DerivativeKernel(null, DerivativeType.x);
            DerivativeKernel yDerivative = new DerivativeKernel(null, DerivativeType.y);



            return imageToProcess;
        }

        public override double GetMaximumProgress(int imageWidth, int imageHeight){
            return base.GetMaximumProgress(imageWidth, imageHeight) + (((imageWidth - (4)) * (imageHeight - (4))) * (25)) * 2;
        }
    }
}