/*---------------------------------------------------------------------------------------
 Author:      Seth J. Gibson
 Course:      CIS 263-01
 Program:     Assignment 11
 Description: This program utilizes and tests the functionality of knapsack algorithm.
                The program takes in a set of items, each with a weight and value. The
                items are input into a knapsack object with a maximum capacity. A 
                knapsack function determines the maximum weight of the knapsack if
                a specific combination of items are placed inside, ensuring that the most
                amount of weight and value are in the knapsack if all items are 
                considered.
---------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;

namespace Assignment11
{
    public class item
    {
        public int weight;
        public int value;

        public item( int weight, int value ) { this.weight = weight; this.value = value; }
    }

    public class knapsack
    {
        int size;
        int[,] maxCap;
        List<item> items;

        public void printMaxCap()   // https://stackoverflow.com/questions/24094093/how-to-print-2d-array-to-console-in-c-sharp
        {
            for (int i = 0; i < maxCap.GetLength(0); i++)
            {
                for (int j = 0; j < maxCap.GetLength(1); j++)
                {
                    Console.Write(maxCap[i, j] + "\t");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public int getMaxCapacity()  // Modified version of https://dotnetcoretutorials.com/2020/04/22/knapsack-algorithm-in-c/
        {
            int itemIndex = 0;
            foreach (item inHand in items)
            {
                for (int capIndex = 0; capIndex <= size; capIndex++)
                {
                    if (itemIndex == 0)
                    {
                        if (inHand.weight <= capIndex)
                            maxCap[itemIndex, capIndex] = inHand.value;
                        else
                            maxCap[itemIndex, capIndex] = 0;
                    }
                    else
                    {
                        if (inHand.weight <= capIndex)
                            maxCap[itemIndex, capIndex] = Math.Max(maxCap[itemIndex - 1, capIndex], (maxCap[itemIndex - 1, capIndex - inHand.weight] + inHand.value));
                        else
                            maxCap[itemIndex, capIndex] = maxCap[itemIndex - 1, capIndex];
                    }
                }
                itemIndex++;
            }
            return maxCap[items.Count - 1, size];
        }

        public knapsack(int size, List<item> items) { this.size = size; maxCap = new int[items.Count, size + 1]; this.items = items; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<item> firstItems = new List<item>
            {
                //new item(2, 1),
                //new item(4, 5),
                //new item(3, 6),
                //new item(1, 4)    // 10

                //new item(1, 1),
                //new item(3, 5),
                //new item(2, 6),
                //new item(2, 4)    // 11

                new item(1, 1),
                new item(2, 6),
                new item(5, 18),
                new item(6, 22),
                new item(7, 28)     // 40
            };

            knapsack firstKnapsack = new knapsack(11, firstItems);
            Console.WriteLine("1st Max Capacity: " + firstKnapsack.getMaxCapacity());
            firstKnapsack.printMaxCap();

            List < item > secondItems = new List<item>
            {
                new item(250, 16808),
                new item(659, 50074),
                new item(273, 8931),
                new item(879, 27545),
                new item(710, 77924),
                new item(166, 64441),
                new item(43, 84493),
                new item(504, 7988),
                new item(730, 82328),
                new item(613, 78841),
                new item(170, 44304),
                new item(158, 17710),
                new item(934, 29561),
                new item(279, 93100),
                new item(336, 51817),
                new item(827, 99098),
                new item(268, 13513),
                new item(634, 23811),
                new item(150, 80980),
                new item(822, 36580),
                new item(673, 11968),
                new item(337, 1394),
                new item(746, 25486),
                new item(92, 25229),
                new item(358, 40195),
                new item(154, 35002),
                new item(945, 16709),
                new item(491, 15669),
                new item(197, 88125),
                new item(904, 9531),
                new item(667, 27723),
                new item(25, 28550)     // 1215693
            };

            knapsack secondKnapsack = new knapsack(10000, secondItems);
            Console.WriteLine("2nd Max Capacity: " + secondKnapsack.getMaxCapacity());
            Console.WriteLine("Not gonna write the array for this one, for obvious reasons.");
        }
    }
}
