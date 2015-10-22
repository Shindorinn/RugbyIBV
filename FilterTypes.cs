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
             new FilterType(FilterType.Type.BasicKernel, "3x3 Kernel"),
             new FilterType(FilterType.Type.GrayscaleFilter, "Grayscale"),
             new FilterType(FilterType.Type.HighPassFilter, "High-filter pass")
        };

        public static List<FilterType> GetAllFilters()
        {
            return Filters;
        }
    }

    public sealed class FilterType
    {
        public enum Type
        {
            BasicKernel,
            GrayscaleFilter,
            HighPassFilter
        }

        public Type fType { get; private set; }
        public string Value { get; private set; }

        public FilterType(Type fType, string Value)
        {
            this.fType = fType;
            this.Value = Value;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}