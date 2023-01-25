/*---------------------------------------------------------------------------------------
 Author:      Seth J. Gibson
 Course:      CIS 263-01
 Program:     Assignment 7
 Description: This program utilizes and tests the functionality of a heap data structure. 
                The heap is tested by using three int arrays of N size, one filled with 
                max-to-min sorted integers, one filled with min-to-max sorted integers 
                and one filled with randomly-sorted inetegers. One heap is filled and 
                sorted with these arrays by inserting elements one at a time, while 
                another heap has all elements placed inside the heap all at once and then 
                sorts it. Run-times are logged and output for comparison and analysis.
---------------------------------------------------------------------------------------*/

using System;
using System.Linq;

namespace Assignment7
{
    public class heap
    {
        public int[] data;
        public int length;
        public int heapSize;
        public int largest;

        public int? parent(int i)
        {
            decimal result = ((decimal)i - 1) / 2;
            if (result < 0)
                return null;

            if ((result % 1) == 0)
                return (int)result;
            else
                return (int)((decimal)i - 2) / 2;
        }

        public int? left(int i)
        {
            if (((2 * i) + 1) > (length - 1))
                return null;
            return (2 * i) + 1;
        }

        public int? right(int i)
        {
            if (((2 * i) + 2) >= (length - 1))
                return null;
            return (2 * i) + 2;
        }

        public void maxHeapify(int i)
        {
            if (left(i) != null || right(i) != null)
            {
                int? l = left(i), r = right(i);

                if (l != null)
                {
                    if (l <= heapSize && data[(int)l] > data[i])
                        largest = (int)l;
                    else
                        largest = i;
                }

                if (r != null)
                {
                    if (r <= heapSize && data[(int)r] > data[largest])
                        largest = (int)r;
                }

                if (largest != i)
                {
                    int ph = data[i];
                    data[i] = data[largest];
                    data[largest] = ph;

                    maxHeapify(largest);
                }
            }
        }

        public void buildMaxHeap()
        {
            heapSize = length;
            for (int i = length / 2; i >= 0; i--)
                maxHeapify(i);
        }

        public void increaseKey(int i, int key)
        {
            if (key < data[i])
                throw new ArgumentException("HEAP UNDERFLOW");
            data[i] = key;
            while (i > 0 && data[(int)parent(i)] < data[i])
            {
                int ph = data[i];
                data[i] = data[(int)parent(i)];
                data[(int)parent(i)] = ph;

                i = (int)parent(i);
            }
        }

        public void maxHeapInsert(int key)
        {
            if (heapSize >= length)
                return;
            data[heapSize] = int.MinValue;        // int.MinValue! The poor man's -INF!
            increaseKey(heapSize, key);
            heapSize += 1;
        }

        public string maxHeapTest()
        {
            for (int i = length - 1; i >= 0; i--)
            {
                if (parent(i) != null)
                {
                    if (data[(int)parent(i)] < data[i])
                    {
                        return "IS NOT MAXHEAP";
                    }
                }
            }
            return "IS MAXHEAP";
        }

        public heap(int n) { data = new int[n]; length = n; heapSize = 0; }
    }

    class Program
    {
        public static void randomize(int[] array)   // https://stackoverflow.com/questions/108819/best-way-to-randomize-an-array-with-net
        {
            Random rng = new Random();
            int n = array.Length;

            while (n > 1)
            {
                int i = rng.Next(n--);
                int ph = array[n];
                array[n] = array[i];
                array[i] = ph;
            }
        }

        static void Main(string[] args)
        {
            var SW = new System.Diagnostics.Stopwatch();

            int n = 1000000;

            heap heapAS = new heap(n);
            heap heapASR = new heap(n);
            heap heapAR = new heap(n);
            heap heapBS = new heap(n);
            heap heapBSR = new heap(n);
            heap heapBR = new heap(n);

            Console.WriteLine("N - " + n);

            int[] sorted = Enumerable.Range(0, n).ToArray();            // Already max to min, doesn't need to be sorted     
            Array.Reverse(sorted);
            SW.Start();
            for (int i = 0; i < sorted.Length; i++)
                heapAS.maxHeapInsert(sorted[i]);
            SW.Stop();
            Console.WriteLine("heapAS " + heapAS.maxHeapTest() + " - " + SW.Elapsed);
            SW.Reset();

            int[] sortedReverse = Enumerable.Range(0, n).ToArray();     // Needs to sort every single one
            SW.Start();
            for (int i = 0; i < sorted.Length; i++)
                heapASR.maxHeapInsert(sortedReverse[i]);
            SW.Stop();
            Console.WriteLine("heapASR " + heapASR.maxHeapTest() + " - " + SW.Elapsed);
            SW.Reset();

            int[] randomized = Enumerable.Range(0, n).ToArray();
            randomize(randomized);
            SW.Start();
            for (int i = 0; i < sorted.Length; i++)
                heapAR.maxHeapInsert(randomized[i]);
            SW.Stop();
            Console.WriteLine("heapAR " + heapAR.maxHeapTest() + " - " + SW.Elapsed);
            SW.Reset();

            Console.WriteLine();

            sorted.CopyTo(heapBS.data, 0);
            SW.Start();
            heapBS.buildMaxHeap();
            SW.Stop();
            Console.WriteLine("heapBS " + heapBS.maxHeapTest() + " - " + SW.Elapsed);
            SW.Reset();

            sortedReverse.CopyTo(heapBSR.data, 0);
            SW.Start();
            heapBSR.buildMaxHeap();
            SW.Stop();
            Console.WriteLine("heapBSR " + heapBSR.maxHeapTest() + " - " + SW.Elapsed);
            SW.Reset();

            randomized.CopyTo(heapBR.data, 0);
            SW.Start();
            heapBR.buildMaxHeap();
            SW.Stop();
            Console.WriteLine("heapBR " + heapBR.maxHeapTest() + " - " + SW.Elapsed);
            SW.Reset();

            Console.WriteLine();
        }
    }
}
