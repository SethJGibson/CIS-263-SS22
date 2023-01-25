/*---------------------------------------------------------------------------------------
 Author:      Seth J. Gibson
 Course:      CIS 263-01
 Program:     Assignment 2
 Description: This program tests the concepts of Big-O notation by running through 
                multiple code fragments with varying run times and displaying their 
                behavior for analysis.
---------------------------------------------------------------------------------------*/

using System;

namespace Assignment2
{
    class globals                       // Global variables stored in their own class
    {
        public static uint total = 0;
        public static int n = 200000;
    }

    class program
    {
        static void a()
        {
            for (uint i = 0; i < globals.n; i++) {
                globals.total++;
            }
        }

        static void b()
        {
            for (uint i = 0; i < globals.n; i++)
            {
                for (uint j = 0; j < globals.n; j++)
                {
                    globals.total++;
                }
            }
        }

        static void c()
        {
            for (uint i = 0; i < globals.n; i++)
            {
                for (uint j = 0; j < i; j++)
                {
                    globals.total++;
                }
            }
        }

        static void d()
        {
            for (uint i = 0; i < globals.n; i++)
            {
                for (uint j = 0; j < i; j++)
                {
                    if (j % 2 == 0) 
                        globals.total++;
                }
            }
        }

        static void Main(string[] args)                     // VS can't find Main() if its named "main()," can't follow camelCase here
        {
            Console.WriteLine("n = " + globals.n);          // Display amount of loops run for each code fragment
            var SW = new System.Diagnostics.Stopwatch();    // Initialize system stopwatch

            SW.Start();                                     // For each fragment, start the clock,
            a();                                            // run the fragment,
            SW.Stop();                                      // then stop the clock and print out result
            Console.WriteLine("A: " + SW.Elapsed);

            SW.Start();
            b();
            SW.Stop();
            Console.WriteLine("B: " + SW.Elapsed);

            SW.Start();
            c();
            SW.Stop();
            Console.WriteLine("C: " + SW.Elapsed);

            SW.Start();
            d();
            SW.Stop();
            Console.WriteLine("D: " + SW.Elapsed);
        }
    }
}
