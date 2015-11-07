using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INFOIBV.Utilities
{
    public class ImageObject2 : ImageObject
    {
        protected List<ListPixel> perimeterListPixels;

        public ImageObject2(List<ListPixel> pixelsToProcess)
            : base(pixelsToProcess)
        {
            // Keep empty

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
                            distance = Math.Sqrt(Math.Pow(toCalcTo.X - toCalcFrom.X, 2) + Math.Pow(toCalcTo.Y - toCalcFrom.Y, 2));
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

        private double _longestChordOrientation;
        public double LongestChordOrientation
        {
            get
            {
                if (_longestChordOrientation == 0)
                    foreach (var pixel in pixels)
                        if (pixel == 1)
                            _longestChordOrientation++; // Compute is Area

                return _longestChordOrientation;
            }
            set { _longestChordOrientation = value; }
        }

        private double _longestPerpendicularChord;
        public double LongestPerpendicularChord
        {
            get
            {
                if (_longestPerpendicularChord == 0)
                    foreach (var pixel in pixels)
                        if (pixel == 1)
                            _longestPerpendicularChord++; // Compute is Area

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