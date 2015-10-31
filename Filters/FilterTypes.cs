using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INFOIBV.Filters
{
    public static class FilterTypes
    {
        private static readonly List<FilterType> Filters = new List<FilterType>()
        {
             new FilterType(FilterType.Type.GrayscaleFilter, "Grayscale"),
             new FilterType(FilterType.Type.GrayscaleKernel, "Grayscale Kernel"),
             new FilterType(FilterType.Type.InvertFilter, "Invert Filter"),
             new FilterType(FilterType.Type.DoNothingKernel, "Do-Nothing-Kernel"),
             
             new FilterType(FilterType.Type.DerivativeKernelX, "X Derivative Kernel"),
             new FilterType(FilterType.Type.DerivativeKernelY, "Y Derivative Kernel"),
             new FilterType(FilterType.Type.DerivativeKernelXY, "XY Derivative Kernel"),

             new FilterType(FilterType.Type.ApproximationCompassOperation, "Approximation Compass Operation"),
             new FilterType(FilterType.Type.IsotropicCompassOperation, "Isotropic Compass Operation"),
             new FilterType(FilterType.Type.KirschCompassOperation, "Kirsch Compass Operation"),
             new FilterType(FilterType.Type.PrewittCompassOperation, "Prewitt Compass Operation"),
             new FilterType(FilterType.Type.SobelCompassOperation, "Sobel Compass Operation"),

             new FilterType(FilterType.Type.ObjectDetection, "Object Detection"),

             //new FilterType(FilterType.Type.PrewittKernelN,  "Prewitt Kernel North"),
             //new FilterType(FilterType.Type.PrewittKernelNE, "Prewitt Kernel North East"),
             //new FilterType(FilterType.Type.PrewittKernelE,  "Prewitt Kernel East"),
             //new FilterType(FilterType.Type.PrewittKernelSE, "Prewitt Kernel South East"),
             //new FilterType(FilterType.Type.PrewittKernelS,  "Prewitt Kernel South"),
             //new FilterType(FilterType.Type.PrewittKernelSW, "Prewitt Kernel South West"),
             //new FilterType(FilterType.Type.PrewittKernelW,  "Prewitt Kernel West"),
             //new FilterType(FilterType.Type.PrewittKernelNW, "Prewitt Kernel North West"),

             //new FilterType(FilterType.Type.ApproximationKernelN,  "Approximation Kernel North"),
             //new FilterType(FilterType.Type.ApproximationKernelNE, "Approximation Kernel North East"),
             //new FilterType(FilterType.Type.ApproximationKernelE,  "Approximation Kernel East"),
             //new FilterType(FilterType.Type.ApproximationKernelSE, "Approximation Kernel South East"),
             //new FilterType(FilterType.Type.ApproximationKernelS,  "Approximation Kernel South"),
             //new FilterType(FilterType.Type.ApproximationKernelSW, "Approximation Kernel South West"),
             //new FilterType(FilterType.Type.ApproximationKernelW,  "Approximation Kernel West"),
             //new FilterType(FilterType.Type.ApproximationKernelNW, "Approximation Kernel North West"),

             //new FilterType(FilterType.Type.IsotropicKernelN,  "Isotropic Kernel North"),
             //new FilterType(FilterType.Type.IsotropicKernelNE, "Isotropic Kernel North East"),
             //new FilterType(FilterType.Type.IsotropicKernelE,  "Isotropic Kernel East"),
             //new FilterType(FilterType.Type.IsotropicKernelSE, "Isotropic Kernel South East"),
             //new FilterType(FilterType.Type.IsotropicKernelS,  "Isotropic Kernel South"),
             //new FilterType(FilterType.Type.IsotropicKernelSW, "Isotropic Kernel South West"),
             //new FilterType(FilterType.Type.IsotropicKernelW,  "Isotropic Kernel West"),
             //new FilterType(FilterType.Type.IsotropicKernelNW, "Isotropic Kernel North West"),

             //new FilterType(FilterType.Type.SobelKernelN,  "Sobel Kernel North"),
             //new FilterType(FilterType.Type.SobelKernelNE, "Sobel Kernel North East"),
             //new FilterType(FilterType.Type.SobelKernelE,  "Sobel Kernel East"),
             //new FilterType(FilterType.Type.SobelKernelSE, "Sobel Kernel South East"),
             //new FilterType(FilterType.Type.SobelKernelS,  "Sobel Kernel South"),
             //new FilterType(FilterType.Type.SobelKernelSW, "Sobel Kernel South West"),
             //new FilterType(FilterType.Type.SobelKernelW,  "Sobel Kernel West"),
             //new FilterType(FilterType.Type.SobelKernelNW, "Sobel Kernel North West"),

             new FilterType(FilterType.Type.NormalizingKernel3, "Normalizing Kernel 3x3"),
             new FilterType(FilterType.Type.NormalizingKernel5, "Normalizing Kernel 5x5"),
             new FilterType(FilterType.Type.NormalizingKernel7, "Normalizing Kernel 7x7"),
             new FilterType(FilterType.Type.NormalizingKernel1x25, "Normalizing Kernel 1x25"),
             new FilterType(FilterType.Type.NormalizingKernel25x1, "Normalizing Kernel 25x1"),
             new FilterType(FilterType.Type.NormalizingKernel25, "Normalizing Kernel 25x25"),

             new FilterType(FilterType.Type.HighPassFilter32, "High-filter pass 32"),
             new FilterType(FilterType.Type.HighPassFilter64, "High-filter pass 64"),
             new FilterType(FilterType.Type.HighPassFilter96, "High-filter pass 96"),
             new FilterType(FilterType.Type.HighPassFilter128, "High-filter pass 128"),
             new FilterType(FilterType.Type.HighPassFilter160, "High-filter pass 160"),
             new FilterType(FilterType.Type.HighPassFilter192, "High-filter pass 192"),
             new FilterType(FilterType.Type.HighPassFilter224, "High-filter pass 224"),
             new FilterType(FilterType.Type.HighPassFilter256, "High-filter pass 256"),

             new FilterType(FilterType.Type.LowPassFilter32, "Low-filter pass 32"),
             new FilterType(FilterType.Type.LowPassFilter64, "Low-filter pass 64"),
             new FilterType(FilterType.Type.LowPassFilter96, "Low-filter pass 96"),
             new FilterType(FilterType.Type.LowPassFilter128, "Low-filter pass 128"),
             new FilterType(FilterType.Type.LowPassFilter160, "Low-filter pass 160"),
             new FilterType(FilterType.Type.LowPassFilter192, "Low-filter pass 192"),
             new FilterType(FilterType.Type.LowPassFilter224, "Low-filter pass 224"),
             new FilterType(FilterType.Type.LowPassFilter256, "Low-filter pass 256"),

             new FilterType(FilterType.Type.ThresholdFilter32, "Threshold 32"),
             new FilterType(FilterType.Type.ThresholdFilter64, "Threshold 64"),
             new FilterType(FilterType.Type.ThresholdFilter96, "Threshold 96"),
             new FilterType(FilterType.Type.ThresholdFilter128, "Threshold 128"),
             new FilterType(FilterType.Type.ThresholdFilter160, "Threshold 160"),
             new FilterType(FilterType.Type.ThresholdFilter192, "Threshold 192"),
             new FilterType(FilterType.Type.ThresholdFilter224, "Threshold 224"),
             new FilterType(FilterType.Type.ThresholdFilter256, "Threshold 256")
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
            GrayscaleFilter,
            GrayscaleKernel,
            InvertFilter,
            DoNothingKernel,

            DerivativeKernelX,
            DerivativeKernelY,
            DerivativeKernelXY,

            ApproximationCompassOperation,
            IsotropicCompassOperation,
            KirschCompassOperation,
            PrewittCompassOperation,
            SobelCompassOperation,

            ObjectDetection,

            //PrewittKernelN,
            //PrewittKernelNE,
            //PrewittKernelE,
            //PrewittKernelSE,
            //PrewittKernelS,
            //PrewittKernelSW,
            //PrewittKernelW,
            //PrewittKernelNW,

            //ApproximationKernelN,
            //ApproximationKernelNE,
            //ApproximationKernelE,
            //ApproximationKernelSE,
            //ApproximationKernelS,
            //ApproximationKernelSW,
            //ApproximationKernelW,
            //ApproximationKernelNW,

            //IsotropicKernelN,
            //IsotropicKernelNE,
            //IsotropicKernelE,
            //IsotropicKernelSE,
            //IsotropicKernelS,
            //IsotropicKernelSW,
            //IsotropicKernelW,
            //IsotropicKernelNW,

            //SobelKernelN,
            //SobelKernelNE,
            //SobelKernelE,
            //SobelKernelSE,
            //SobelKernelS,
            //SobelKernelSW,
            //SobelKernelW,
            //SobelKernelNW,

            NormalizingKernel3,
            NormalizingKernel5,
            NormalizingKernel7,
            NormalizingKernel1x25,
            NormalizingKernel25x1,
            NormalizingKernel25,

            HighPassFilter32,
            HighPassFilter64,
            HighPassFilter96,
            HighPassFilter128,
            HighPassFilter160,
            HighPassFilter192,
            HighPassFilter224,
            HighPassFilter256,

            LowPassFilter32,
            LowPassFilter64,
            LowPassFilter96,
            LowPassFilter128,
            LowPassFilter160,
            LowPassFilter192,
            LowPassFilter224,
            LowPassFilter256,

            ThresholdFilter32,
            ThresholdFilter64,
            ThresholdFilter96,
            ThresholdFilter128,
            ThresholdFilter160,
            ThresholdFilter192,
            ThresholdFilter224,
            ThresholdFilter256
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