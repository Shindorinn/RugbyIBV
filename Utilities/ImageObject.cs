﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INFOIBV.Utilities
{
    public class ImageObject
    {
        protected int[,] pixels;
        public int OffsetX { get; private set; }
        public int OffsetY { get; private set; }

        public ImageObject(List<ListPixel> pixelsToProcess)
        {
            Area = 0;

            OffsetX = int.MaxValue;
            OffsetY = int.MaxValue; // To make sure it changes on the first time
            int offsetXMax = -1;
            int offsetYMax = -1;

            for (int i = 0; i < pixelsToProcess.Count; i++)
            {
                if (pixelsToProcess[i].X < OffsetX)
                    OffsetX = pixelsToProcess[i].X;
                if (pixelsToProcess[i].Y < OffsetY)
                    OffsetY = pixelsToProcess[i].Y;
                if (pixelsToProcess[i].X > offsetXMax)
                    offsetXMax = pixelsToProcess[i].X;
                if (pixelsToProcess[i].Y > offsetYMax)
                    offsetYMax = pixelsToProcess[i].Y;
            }

            int sizeX = offsetXMax - (OffsetX - 1);
            int sizeY = offsetYMax - (OffsetY - 1);

            pixels = new int[sizeX, sizeY];
            foreach (var pixel in pixelsToProcess)
                pixels[pixel.X - OffsetX, pixel.Y - OffsetY] = 1;

            for (int i = 0; i < pixels.GetLength(0); i++)
                for (int j = 0; j < pixels.GetLength(1); j++)
                    if (pixels[i, j] == 1)
                        continue;
                    else
                        pixels[i, j] = 0;

            Console.WriteLine("Succesfully constructed an imageobject");
            Console.WriteLine("OffsetX: {0}, SizeX: {1}", OffsetX, sizeX);
            Console.WriteLine("OffsetY: {0}, SizeY: {1}", OffsetY, sizeY);
            Console.WriteLine("Number of pixels: {0}, Number of object-pixels: {1}", pixels.Length, Area);
        }

        private int _area;
        public int Area
        {
            get
            {
                if (_area == 0)
                    foreach (var pixel in pixels)
                        if (pixel == 1)
                            _area++;

                return _area;
            }
            set { _area = value; }
        }

        private int _perimeter;
        public int Perimeter
        {
            get
            {
                if (_perimeter == 0)
                    foreach (var pixel in pixels)
                        if (pixel == 1)
                            _perimeter++; // Compute is Area

                return _perimeter;
            }
            set { _perimeter = value; }
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

        private double _longestChord;
        public double LongestChord
        {
            get
            {
                if (_longestChord == 0)
                    foreach (var pixel in pixels)
                        if (pixel == 1)
                            _longestChord++; // Compute is Area

                return _longestChord;
            }
            set { _longestChord = value; }
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