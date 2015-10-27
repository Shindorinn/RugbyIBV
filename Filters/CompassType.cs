using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INFOIBV.Filters
{
    /// <summary>
    /// List of kernels used in the compass-operator
    /// </summary>
    public enum CompassType
    {
        Approximation,
        Prewitt,
        Isotropic,
        Sobel,
        Kirsch
    }
}