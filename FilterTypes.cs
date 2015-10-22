using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INFOIBV
{
    public static class FilterTypes
    {
        private static readonly List<FilterType> Filters = new List<FilterType>()
        {
            // Some Dummy filtertypes
             new FilterType("3x3 Kernel"),
             new FilterType("5x5 Kernel"),
             new FilterType("Blurry"),
             new FilterType("High-filter pass")

             /* if Name is needed
             new FilterType("SmallKernel", "3x3 Kernel"),
             new FilterType("MiddleKernel", "5x5 Kernel"),
             new FilterType("Blurry", "Blurry"),
             new FilterType("HighPass", "High-filter pass") 
             */
        };

        public static List<FilterType> GetAllFilters()
        {
            return Filters;
        }
    }

    public sealed class FilterType
    {
        private enum Type
        {
            SmallKernel,
            MediumKernel,
            BlurryFilter,
            HighPassFilter
        }

        // Don't know if name is needed
        //public string Name { get; private set; }
        public string Value { get; private set; }

        public FilterType(/*string Name, */string Value)
        {
            //this.Name = Name;
            this.Value = Value;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}