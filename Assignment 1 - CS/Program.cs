/*---------------------------------------------------------------------------------------
 Author:      Seth J. Gibson
 Course:      CIS 263-01
 Program:     Assignment 1
 Description: This program takes in five integers, one at a time, and inserts them into
                a list, organized from smallest to largest, using the insertion sort
                method.
---------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;

namespace Assignment_1___CS
{
    class Globals
    {
        public static List<int> input = new List<int>() {0, 0, 0, 0, 0};    // Global variables stored in their own class
    }

    class Program
    {
        public static void insertionSort(int range)     // Function that sorts the list of integer inputs
        {
            int cursor = 0, placeHolder;
            
            for (int i = 1; i < range; i++)
            {
                placeHolder = Globals.input[i];
                cursor = i - 1;
                while ((cursor >= 0) && (Globals.input[cursor] > placeHolder))
                {
                    Globals.input[cursor + 1] = Globals.input[cursor];
                    cursor--;
                }
                Globals.input[cursor + 1] = placeHolder;
            }
        }

        public static void displayContents(int range)   // Function that displays the contents of the input list
        {
            Console.Write("> ");
            for (int i = 0; i < range; i++)
                Console.Write(Globals.input[i] + " ");
            Console.WriteLine("");
        }

        public static void getInput(int range)          // Function that receives input from user
        {
            Console.Write(range + ". ");
            Globals.input[range - 1] = Convert.ToInt32(Console.ReadLine());
        }

        static void Main(string[] args)
        {
            Console.Write("Please enter one integer at a time.\n");

            for (int i = 1; i <= 5; i++)
            {
                getInput(i);
                insertionSort(i);
                displayContents(i);
            }
        }
    }
}
