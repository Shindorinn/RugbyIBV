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
        private readonly int MaximumSearchDepth = 2048;

        public DetectObjectsFilter(IApplicableFilter toDecorate)
            : base(toDecorate)
        {
            // Disk in flight.
            imageObjects = null;
        }

        public override Color[,] apply(Color[,] imageToProcess, MainViewModel reportProgressTo)
        {
            imageToProcess = base.apply(imageToProcess, reportProgressTo);
            Color[,] imageToReturn = (Color[,])imageToProcess.Clone();
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
                            for (int w = 0; w < 3; w++)
                                if (imageToProcess[newX + v - 1, newY + w - 1].ToArgb() == Color.Black.ToArgb())
                                    initialCanGo[v, w] = true;
                                else
                                    initialCanGo[v, w] = false;

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

                                for (int v = 0; v < 3; v++)
                                {
                                    for (int w = 0; w < 3; w++)
                                    {
                                        if (queueItem.CanGo[v, w])
                                        {
                                            queueItem.CanGo[v, w] = false;

                                            bool[,] canGo = new bool[3, 3];
                                            for (int n = 0; n < 3; n++)
                                                for (int m = 0; m < 3; m++)
                                                    if (imageToProcess[newX + v + n - 2, newY + w + m - 2].ToArgb() == Color.Black.ToArgb())
                                                        canGo[n, m] = true;
                                                    else
                                                        canGo[n, m] = false;

                                            var newItem = new StackImageObject(newX + v - 1, newY + w - 1, canGo);
                                            queue.Enqueue(newItem);
                                            imageObjects[objectCounter].Push(newItem);
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