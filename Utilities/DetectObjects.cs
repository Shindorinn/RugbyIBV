using INFOIBV.Presentation;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace INFOIBV.Utilities
{
    public class DetectObjects
    {
        private List<ImageObject> detectedObjects;
        private List<ImageObject> detectedBalls;

        private readonly double minEccentricity = 1.50;
        private readonly double maxEccentricity = 1.85;
        private readonly double minRoundness = 0.69;
        private readonly double maxRoundness = 0.85;
        private readonly double minRectangularity = 0.70;
        private readonly double maxRectangularity = 0.79;

        public DetectObjects()
        {
            // Disk in flight.
            detectedObjects = new List<ImageObject>();
            detectedBalls = new List<ImageObject>();
        }

        public void detectObjects(Color[,] imageToReturn)
        {
            Color[,] imageToProcess = (Color[,])imageToReturn.Clone();

            for (int x = 0; x < imageToProcess.GetLength(0); x++)
            {
                for (int y = 0; y < imageToProcess.GetLength(1); y++)
                {
                    if (imageToProcess[x, y].ToArgb() == Color.Black.ToArgb())
                    {
                        Queue<ListPixel> queue = new Queue<ListPixel>();
                        List<ListPixel> pixelList = new List<ListPixel>();

                        bool[,] initialCanGo = new bool[3, 3];
                        for (int v = 0; v < 3; v++)
                        {
                            for (int w = 0; w < 3; w++)
                            {
                                int testX = x + v - 1;
                                int testY = y + w - 1;
                                if ((v != 1 || w != 1) && testX > 0 && testX < imageToProcess.GetLength(0) && testY > 0 && testY < imageToProcess.GetLength(1))
                                {
                                    if (imageToProcess[testX, testY].ToArgb() == Color.Black.ToArgb())
                                        initialCanGo[v, w] = true;
                                    else
                                        initialCanGo[v, w] = false;
                                }
                                else
                                    initialCanGo[v, w] = false;
                            }
                        }

                        queue.Enqueue(new ListPixel(x, y, initialCanGo));

                        while (queue.Count > 0)
                        {
                            ListPixel queueItem = queue.Dequeue();
                            int newX = queueItem.X;
                            int newY = queueItem.Y;

                            if (imageToProcess[newX, newY].ToArgb() == Color.Black.ToArgb())
                            {
                                imageToProcess[newX, newY] = Color.White;
                                pixelList.Add(queueItem);

                                for (int v = 0; v < 3; v++)
                                {
                                    for (int w = 0; w < 3; w++)
                                    {
                                        if (queueItem.CanGo[v, w])
                                        {
                                            queueItem.CanGo[v, w] = false;

                                            bool[,] canGo = new bool[3, 3];
                                            for (int n = 0; n < 3; n++)
                                            {
                                                for (int m = 0; m < 3; m++)
                                                {
                                                    int nextX = newX + v + n - 2;
                                                    int nextY = newY + w + m - 2;
                                                    if ((n != 1 || m != 1) && nextX > 0 && nextX < imageToProcess.GetLength(0) && nextY > 0 && nextY < imageToProcess.GetLength(1))
                                                    {
                                                        if (imageToProcess[nextX, nextY].ToArgb() == Color.Black.ToArgb())
                                                            canGo[n, m] = true; // Need to check if there is already a path queued to it
                                                        else
                                                            canGo[n, m] = false;
                                                    }
                                                    else
                                                        canGo[n, m] = false;
                                                }
                                            }

                                            queue.Enqueue(new ListPixel(newX + v - 1, newY + w - 1, canGo));
                                        }
                                    }
                                }
                            }
                        }

                        detectedObjects.Add(new ImageObject(pixelList));
                    }
                }
            }

            // Filter 'empty' objects

            int sumStack = 0;
            foreach (var imageObject in detectedObjects)
                sumStack += imageObject.Area;

            int sumPixels = 0;
            foreach (var pixel in imageToReturn)
                sumPixels += pixel.ToArgb() == Color.Black.ToArgb() ? 1 : 0;

            Console.WriteLine("Sum of stack sizes: {0}", sumStack);
            Console.WriteLine("Sum of black pixels: {0}", sumPixels);
        }

        public void detectRugbyBall(Color[,] imageToReturn)
        {
            if (this.detectedObjects == null)
            {
                detectObjects(imageToReturn);
            }

            foreach (var imgObj in detectedObjects)
            {
                if (isABall(imgObj))
                    detectedBalls.Add(imgObj);
            }

            foreach (var ball in detectedBalls)
            {
                ball.ColorPerimeter(imageToReturn, Color.SkyBlue);
            }

        }

        public List<ImageObject> getDetectedObjects()
        {
            if (detectedObjects == null)
                Console.WriteLine("DetectedObjects is empty. Should call detectObjects() first.");

            return detectedObjects;
        }

        private bool isABall(ImageObject obj)
        {
            return (
                this.minEccentricity < obj.Eccentricity && obj.Eccentricity < this.maxEccentricity          &&
                this.minRectangularity < obj.Rectangularity && obj.Rectangularity < this.maxRectangularity  &&
                this.minRoundness < obj.Roundness && obj.Roundness < this.maxRoundness
                );

        }
    }
}