using INFOIBV.Presentation;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace INFOIBV.Filters
{
    public class DetectObjectsFilter : BasicFilter
    {
        private Stack<StackImageObject>[] imageObjects;

        public DetectObjectsFilter(IApplicableFilter toDecorate)
            : base(toDecorate)
        {
            // Disk in flight.
            imageObjects = null;
        }

        public override Color[,] apply(Color[,] imageToProcess, MainViewModel reportProgressTo)
        {
            imageToProcess = base.apply(imageToProcess, reportProgressTo);
            
            Color[,] imageToReturn = new Color[imageToProcess.GetLength(0), imageToProcess.GetLength(1)];
            Array.Copy(imageToProcess, imageToReturn, imageToProcess.GetLength(0) * imageToProcess.GetLength(1));

            int objectCounter = 0;

            for (int x = 0; x < imageToProcess.GetLength(0); x++)
            {
                for (int y = 0; y < imageToProcess.GetLength(1); y++)
                {
                    if (imageToProcess[x, y].ToArgb() == Color.Black.ToArgb())
                    {
                        EnlargeArray(); // makes room for another object
                        int newX = x;
                        int newY = y;
                        Queue<StackImageObject> queue = new Queue<StackImageObject>();

                        bool[,] initialCanGo = new bool[3, 3];
                        for (int v = 0; v < 3; v++)
                        {
                            for (int w = 0; w < 3; w++)
                            {
                                int testX = newX + v - 1;
                                int testY = newY + w - 1;
                                if ((v != 1 || w != 1 ) && testX > 0 && testX < imageToProcess.GetLength(0) && testY > 0 && testY < imageToProcess.GetLength(1))
                                {
                                    if (imageToProcess[newX + v - 1, newY + w - 1].ToArgb() == Color.Black.ToArgb())
                                        initialCanGo[v, w] = true;
                                    else
                                        initialCanGo[v, w] = false;
                                }
                                else
                                    initialCanGo[v, w] = false;
                            }
                        }

                        queue.Enqueue(new StackImageObject(newX, newY, initialCanGo));
                        imageObjects[objectCounter].Push(queue.Peek());

                        while (queue.Count > 0)
                        {
                            StackImageObject queueItem = queue.Dequeue();
                            newX = queueItem.X;
                            newY = queueItem.Y;

                            if (imageToProcess[newX, newY].ToArgb() == Color.Black.ToArgb())
                            {
                                imageToProcess[newX, newY] = Color.White;
                                imageObjects[objectCounter].Push(queueItem);

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
                                                    int testX = newX + v + n - 2;
                                                    int testY = newY + w + m - 2;
                                                    if ((n != 1 || m != 1) && testX > 0 && testX < imageToProcess.GetLength(0) && testY > 0 && testY < imageToProcess.GetLength(1))
                                                    {
                                                        if (imageToProcess[newX + v + n - 2, newY + w + m - 2].ToArgb() == Color.Black.ToArgb())
                                                            canGo[n, m] = true; // Need to check if there is already a path to it
                                                        else
                                                            canGo[n, m] = false;
                                                    }
                                                    else
                                                        canGo[n, m] = false;
                                                }
                                            }

                                            queue.Enqueue(new StackImageObject(newX + v - 1, newY + w - 1, canGo));
                                        }
                                    }
                                }
                            }
                        }

                        objectCounter++;
                    }
                    reportProgressTo.Progress++;
                }
            }

            // 512 x 512
            // Football-blue-white_small.jpg
            // 50966 - num black pixels
            // 51742 - num of black pixels in image

            int sumStack = 0;
            foreach (var stack in imageObjects)
                sumStack += stack.Count;

            int sumPixels = 0;
            foreach (var pixel in imageToReturn)
                sumPixels += pixel.ToArgb() == Color.Black.ToArgb() ? 1 : 0;

            Console.WriteLine("Sum of stack sizes: {0}", sumStack);
            Console.WriteLine("Sum of black pixels: {0}", sumPixels);

            return imageToReturn;
        }

        private void EnlargeArray()
        {
            if (imageObjects == null)
                imageObjects = new Stack<StackImageObject>[1];
            else
            {
                Stack<StackImageObject>[] tempStackArray = (Stack<StackImageObject>[])imageObjects.Clone();
                imageObjects = new Stack<StackImageObject>[tempStackArray.Length + 1];

                for (int i = 0; i < tempStackArray.Length; i++)
                    imageObjects[i] = tempStackArray[i];
            }

            imageObjects[imageObjects.Length - 1] = new Stack<StackImageObject>();
        }

        public override double GetMaximumProgress(int imageWidth, int imageHeight)
        {
            return base.GetMaximumProgress(imageWidth, imageHeight) + (imageWidth * imageHeight); // Progress might seem stuck for detecting objects. It's not reporting that progress.
        }
    }
}