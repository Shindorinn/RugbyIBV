using INFOIBV.Utilities.Enums;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INFOIBV.Utilities
{
    public class ImageObject
    {
        protected int[,] pixels;
        protected int[,] perimeterPixels;
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

            Console.WriteLine("Succesfully constructed an image object");
            Console.WriteLine("The image object has the following properties:");
            Console.WriteLine("OffsetX: {0}, OffsetY: {1}", OffsetX, OffsetY);
            Console.WriteLine("SizeX: {0}, SizeY: {1}", sizeX, sizeY);
            Console.WriteLine("Size of array: {0}", pixels.Length);
            Console.WriteLine("Area: ", Area);
            Console.WriteLine("Perimeter: ", Perimeter);

            Console.WriteLine("");
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("");
        }

        private void EstablishPerimeterPixels(bool countPerimeter)
        {
            int fX = -1; // First X
            int fY = -1; // First Y
            for (int i = 0; i < pixels.GetLength(0); i++) // Would only loop once, if things were done right
            {
                for (int j = 0; j < pixels.GetLength(1); j++)
                {
                    if (pixels[i, j] == 1)
                    {
                        fX = i;
                        fY = j;
                        break;
                    }
                }

                if (fX != -1 && fY != -1)
                    break;
            }

            if (fX == -1 && fY == -1)
                return; // Error, should not come to this. Could not find a pixel of the object.

            int nX = fX; // Next X
            int nY = fY; // Next Y
            Direction lookingDirection = Direction.North; // Begin looking North

            perimeterPixels = new int[pixels.GetLength(0), pixels.GetLength(1)];
            for (int i = 0; i < perimeterPixels.GetLength(0); i++)
                for (int j = 0; j < perimeterPixels.GetLength(1); j++)
                    perimeterPixels[i, j] = 0;

            double perimeterCounter = 0.0;

            do
            {
                Direction newDirection = lookingDirection;
                int antiLoopCounter = 8;
                bool hasADirection = false;
                // 1 loop to rule them all
                for (int i = -2; i < 3; i++)
                {
                    if (CanTurn(nX, nY, newDirection, i))
                    { // Found a direction to go to
                        hasADirection = true;
                        newDirection = Turn(nX, nY, newDirection, i);
                        break;
                    }
                }

                if(!hasADirection) // If you have no direction
                { // GO BACK (to the choppa)!
                    newDirection = Turn(nX, nY, newDirection, 4);
                }

                if (countPerimeter)
                {
                    switch (newDirection)
                    {
                        case Direction.NorthEast:
                        case Direction.SouthEast:
                        case Direction.SouthWest:
                        case Direction.NorthWest:
                            perimeterCounter += Math.Sqrt(2);
                            break;
                        case Direction.North:
                        case Direction.East:
                        case Direction.South:
                        case Direction.West:
                            perimeterCounter += 1.0;
                            break;
                        default:
                            break;
                    }
                }

                if (countPerimeter)
                    Perimeter = perimeterCounter;

                Traverse(ref nX, ref nY, newDirection); // Sets new nX and nY
                lookingDirection = newDirection;
                perimeterPixels[nX, nY] = 1;

            } while (nX != fX && nY != fY);
        }

        private bool CanTurn(int x, int y, Direction direction, int turnDirection)
        {
            int numDirection = -1;
            switch (direction) // Look in new direction
            {
                case Direction.North:
                    numDirection = 0;
                    break;
                case Direction.NorthEast:
                    numDirection = 1;
                    break;
                case Direction.East:
                    numDirection = 2;
                    break;
                case Direction.SouthEast:
                    numDirection = 3;
                    break;
                case Direction.South:
                    numDirection = 4;
                    break;
                case Direction.SouthWest:
                    numDirection = 5;
                    break;
                case Direction.West:
                    numDirection = 6;
                    break;
                case Direction.NorthWest:
                    numDirection = 7;
                    break;
                default:
                    return false;
            }

            int newNumDirection = -1;
            if (numDirection + turnDirection < 0)
                newNumDirection = 8 - numDirection + turnDirection;
            else
                newNumDirection = (numDirection + turnDirection) % 8;

            switch (newNumDirection)
            {
                case 0: // North
                    if (y - 1 > 0)
                        return pixels[x, y - 1] == 1;
                    break;
                case 1: // NorthEast
                    if (x + 1 < pixels.GetLength(0) && y - 1 > 0)
                        return pixels[x + 1, y - 1] == 1;
                    break;
                case 2: // East
                    if (x + 1 < pixels.GetLength(0))
                        return pixels[x + 1, y] == 1;
                    break;
                case 3: // SouthEast
                    if (x + 1 < pixels.GetLength(0) && y + 1 < pixels.GetLength(1))
                        return pixels[x + 1, y + 1] == 1;
                    break;
                case 4: // South
                    if (y + 1 < pixels.GetLength(1))
                        return pixels[x, y + 1] == 1;
                    break;
                case 5: // SouthWest
                    if (x - 1 > 0 && y + 1 < pixels.GetLength(1))
                        return pixels[x - 1, y + 1] == 1;
                    break;
                case 6: // West
                    if (x - 1 > 0)
                        return pixels[x - 1, y] == 1;
                    break;
                case 7: // NorthWest
                    if (x - 1 > 0 && y - 1 > 0)
                        return pixels[x - 1, y - 1] == 1;
                    break;
                default:
                    return false;
            }

            return false;
        }

        private Direction Turn(int x, int y, Direction direction, int turnDirection)
        {
            int numDirection = -1;
            switch (direction) // Look in new direction
            {
                case Direction.North:
                    numDirection = 0;
                    break;
                case Direction.NorthEast:
                    numDirection = 1;
                    break;
                case Direction.East:
                    numDirection = 2;
                    break;
                case Direction.SouthEast:
                    numDirection = 3;
                    break;
                case Direction.South:
                    numDirection = 4;
                    break;
                case Direction.SouthWest:
                    numDirection = 5;
                    break;
                case Direction.West:
                    numDirection = 6;
                    break;
                case Direction.NorthWest:
                    numDirection = 7;
                    break;
                default:
                    return direction;
            }

            int newNumDirection = -1;
            if (numDirection + turnDirection < 0)
                newNumDirection = 8 - numDirection + turnDirection;
            else
                newNumDirection = (numDirection + turnDirection) % 8;

            switch (newNumDirection)
            {
                case 0: // North
                    if (y - 1 > 0)
                        return pixels[x, y - 1] == 1 ? Direction.North : direction;
                    break;
                case 1: // NorthEast
                    if (x + 1 < pixels.GetLength(0) && y - 1 > 0)
                        return pixels[x + 1, y - 1] == 1 ? Direction.NorthEast : direction;
                    break;
                case 2: // East
                    if (x + 1 < pixels.GetLength(0))
                        return pixels[x + 1, y] == 1 ? Direction.East : direction;
                    break;
                case 3: // SouthEast
                    if (x + 1 < pixels.GetLength(0) && y + 1 < pixels.GetLength(1))
                        return pixels[x + 1, y + 1] == 1 ? Direction.SouthEast : direction;
                    break;
                case 4: // South
                    if (y + 1 < pixels.GetLength(1))
                        return pixels[x, y + 1] == 1 ? Direction.South : direction;
                    break;
                case 5: // SouthWest
                    if (x - 1 > 0 && y + 1 < pixels.GetLength(1))
                        return pixels[x - 1, y + 1] == 1 ? Direction.SouthWest : direction;
                    break;
                case 6: // West
                    if (x - 1 > 0)
                        return pixels[x - 1, y] == 1 ? Direction.West : direction;
                    break;
                case 7: // NorthWest
                    if (x - 1 > 0 && y - 1 > 0)
                        return pixels[x - 1, y - 1] == 1 ? Direction.NorthWest : direction;
                    break;
                default:
                    return direction;
            }

            return direction;
        }

        private void Traverse(ref int x, ref int y, Direction direction)
        {
            switch (direction)
            {
                case Direction.North:
                    y--;
                    break;
                case Direction.NorthEast:
                    x++;
                    y--;
                    break;
                case Direction.East:
                    x++;
                    break;
                case Direction.SouthEast:
                    x++;
                    y++;
                    break;
                case Direction.South:
                    y++;
                    break;
                case Direction.SouthWest:
                    x--;
                    y++;
                    break;
                case Direction.West:
                    x--;
                    break;
                case Direction.NorthWest:
                    x--;
                    y++;
                    break;
                default: // Shouldn't come to this. Unauthorized Direction used.
                    x = -1;
                    y = -1;
                    break;
            }
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

        private double _perimeter;
        public double Perimeter
        {
            get
            {
                if (_perimeter == 0)
                    EstablishPerimeterPixels(true); // If needed, redo the perimeterPixels, but count this time

                return _perimeter;
            }
            set { _perimeter = value; } // Can be 1's but also Sqrt(2)'s
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