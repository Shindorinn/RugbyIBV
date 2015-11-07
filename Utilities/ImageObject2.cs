using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace INFOIBV.Utilities
{
    public class ImageObject2 : ImageObject
    {
        protected List<ListPixel> perimeterListPixels;

        public ImageObject2(List<ListPixel> pixelsToProcess)
            : base(pixelsToProcess)
        {
            // Keep empty
            perimeterListPixels = null;
        }

        private double _compactness;
        public double Compactness
        {
            get
            {
                if (_compactness == 0)
                    foreach (var pixel in pixels)
                        if (pixel == 1)
                            _compactness++; // Compute is Area

                return _compactness;
            }
            set { _compactness = value; }
        }

        private double _roundness;
        public double Roundness
        {
            get
            {
                if (_roundness == 0)
                    foreach (var pixel in pixels)
                        if (pixel == 1)
                            _roundness++; // Compute is Area

                return _roundness;
            }
            set { _roundness = value; }
        }

        private Chord _longestChord;
        public Chord LongestChord
        {
            get
            {
                if (_longestChord == null)
                {
                    if (this.perimeterListPixels == null)
                    {
                        this.perimeterListPixels = base.ConvertPerimeterPixelsToList();
                    }

                    double longestDistance = Double.NegativeInfinity;
                    double distance = 0;
                    ListPixel firstPoint = new ListPixel(0, 0, new bool[0,0]);
                    ListPixel secondPoint = new ListPixel(0, 0, new bool[0, 0]);

                    for (int i = 0; i < this.perimeterListPixels.Count; i++)
                    {
                        if (i + 1 >= this.perimeterListPixels.Count)
                        {
                            continue; // Skip the last loop
                        }
                        ListPixel toCalcFrom = this.perimeterListPixels[i];

                        for (int j = i + 1; j < this.perimeterListPixels.Count; j++)
                        {
                            ListPixel toCalcTo = this.perimeterListPixels[j];
                            distance = Math.Sqrt(Math.Pow((double)toCalcTo.X - (double)toCalcFrom.X, 2) + Math.Pow((double)toCalcTo.Y - (double)toCalcFrom.Y, 2));
                            if (distance > longestDistance)
                            {
                                longestDistance = distance;
                                firstPoint = toCalcFrom;
                                secondPoint = toCalcTo;
                            }
                        }
                    }
                    Chord toReturn = new Chord(firstPoint, secondPoint, longestDistance);
                    _longestChord = toReturn;
                }
                return _longestChord;
            }
        }

        private Chord _longestPerpendicularChord;
        public Chord LongestPerpendicularChord
        {
            get
            {
                if (_longestPerpendicularChord == null)
                {
                    Chord longestchord = this.LongestChord;
                    double angle = longestchord.orientation;
                    
                    Vector<double> unitVector1 = new DenseVector(new double[] { Math.Cos(angle), Math.Sin(angle) });
                    Vector<double> unitVector2 = new DenseVector(new double[] { Math.Cos(angle + 90), Math.Sin(angle + 90) });

                    Matrix<double> transformationMatrix = DenseMatrix.OfColumnVectors(unitVector1, unitVector2);

                    List<Vector<double>> transformedListPixels = new List<Vector<double>>();

                    foreach (ListPixel pixel in this.perimeterListPixels)
                    {
                        Vector<double> convertedPixel = new DenseVector(new double [] {pixel.X, pixel.Y});
                        transformedListPixels.Add(transformationMatrix.Multiply(convertedPixel));
                    }

                    Vector<double> point1;
                    Vector<double> point2;
                    double longestDistance = double.MinValue;
                    double distance = double.MinValue;

                    for (int i = 0; i < transformedListPixels.Count; i++)
                    {
                        if (i + 1 >= transformedListPixels.Count)
                        {
                            continue; // Skip the last loop
                        }
                        Vector<double> toCalcFrom = transformedListPixels[i];

                        for (int j = i + 1; j < transformedListPixels.Count; j++)
                        {
                            Vector<double> toCalcTo = transformedListPixels[j];
                            distance = Math.Sqrt(Math.Pow((double)toCalcTo.ToArray()[1] - (double)toCalcFrom.ToArray()[1], 2));
                            if (distance > longestDistance)
                            {
                                longestDistance = distance;
                                point1 = toCalcFrom;
                                point2 = toCalcTo;
                            }
                        }
                    }

                }
                    
                return _longestPerpendicularChord;
            }
            set { _longestPerpendicularChord = value; }
        }

        private double _eccentricity;
        public double Eccentricity
        {
            get
            {
                if (_eccentricity == 0)
                    foreach (var pixel in pixels)
                        if (pixel == 1)
                            _eccentricity++; // Compute is Area

                return _eccentricity;
            }
            set { _eccentricity = value; }
        }

        private double _boundingBoxArea;
        public double BoundingBoxArea
        {
            get
            {
                if (_boundingBoxArea == 0)
                    foreach (var pixel in pixels)
                        if (pixel == 1)
                            _boundingBoxArea++; // Compute is Area

                return _boundingBoxArea;
            }
            set { _boundingBoxArea = value; }
        }

        private double _rectangularity;
        public double Rectangularity
        {
            get
            {
                if (_rectangularity == 0)
                    foreach (var pixel in pixels)
                        if (pixel == 1)
                            _rectangularity++; // Compute is Area

                return _rectangularity;
            }
            set { _rectangularity = value; }
        }

        private double _elongation;
        public double Elongation
        {
            get
            {
                if (_elongation == 0)
                    foreach (var pixel in pixels)
                        if (pixel == 1)
                            _elongation++; // Compute is Area

                return _elongation;
            }
            set { _elongation = value; }
        }

        private double _elongation2;
        public double Elongation2
        {
            get
            {
                if (_elongation2 == 0)
                    foreach (var pixel in pixels)
                        if (pixel == 1)
                            _elongation2++; // Compute is Area

                return _elongation2;
            }
            set { _elongation2 = value; }
        }

        private double _curvature;
        public double Curvature
        {
            get
            {
                if (_curvature == 0)
                    foreach (var pixel in pixels)
                        if (pixel == 1)
                            _curvature++; // Compute is Area

                return _curvature;
            }
            set { _curvature = value; }
        }

        private double _bendingEnergy;
        public double BendingEnergy
        {
            get
            {
                if (_bendingEnergy == 0)
                    foreach (var pixel in pixels)
                        if (pixel == 1)
                            _bendingEnergy++; // Compute is Area

                return _bendingEnergy;
            }
            set { _bendingEnergy = value; }
        }
    }
}