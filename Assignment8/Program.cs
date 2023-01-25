/*---------------------------------------------------------------------------------------
 Author:      Seth J. Gibson
 Course:      CIS 263-01
 Program:     Assignment 8
 Description: This program utilizes and tests the functionality of hash probing. The 
                program inserts 500 random integers into three hash arrays of size 1001,
                each using either linear probing, quadratic probing or double hashing. 
                The program logs every occurence of a probing collision and prints the
                results for each probing type, displaying the efficiency of each type.
---------------------------------------------------------------------------------------*/

using System;

namespace Assignment8
{
    public class hash
    {
        public int n;
        public int[] array;
        public int collisionCount = 0;

        public void logCollision(int index)
        {
            collisionCount++;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("COLLISION! at " + index + ". Attempting next index...");
            Console.ResetColor();
        }

        public void logSuccess(int input, int index)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("SUCCESS! -> " + input + " placed at index " + index);
            Console.ResetColor();
        }

        public void linearProbing(int[] data)
        {
            Console.WriteLine("<START OF LINEAR PROBE>");
            for (int i = 0; i < data.Length; i++)
            {
                Console.WriteLine("Placing " + data[i] + "...");
                int index = data[i] % n;

                while (array[index] != 0)
                {
                    logCollision(index);
                    index += 1;
                    if (index >= n)
                        index = 0;
                }
                array[index] = data[i];
                logSuccess(array[index], index);
            }
            Console.WriteLine("<END OF LINEAR PROBE>");
        }

        public void quadraticProbing(int[] data)
        {
            Console.WriteLine("<START OF QUADRATIC PROBE>");
            for (int i = 0; i < data.Length; i++)
            {
                Console.WriteLine("Placing " + data[i] + "...");
                int index = data[i] % n;
                int indexPrime = index;
                int j = 1;

                while (array[index] != 0)
                {
                    logCollision(index);
                    index = (indexPrime + (2 * j) + (3 * j * j)) % n;
                    j++;
                }
                array[index] = data[i];
                logSuccess(array[index], index);
            }
            Console.WriteLine("<END OF QUADRATIC PROBE>");
        }

        public void doubleProbing(int[] data)
        {
            Console.WriteLine("<START OF DOUBLE PROBE>");
            for (int i = 0; i < data.Length; i++)
            {
                Console.WriteLine("Placing " + data[i] + "...");
                int index = data[i] % n;
                int indexPrime = index;
                int j = 1;

                while (array[index] != 0)
                {
                    logCollision(index);
                    index = (indexPrime + j * (7 - (data[i] % 7))) % n;
                    j++;
                }
                array[index] = data[i];
                logSuccess(array[index], index);
            }
            Console.WriteLine("<END OF DOUBLE PROBE>");
        }

        public hash(int n) { array = new int[n]; this.n = n; }
    }

    class Program
    {
        static void test()
        {
            int[] data = new int[] { 4371, 1323, 6173, 4199, 4344, 9679, 1989 };

            hash testHash = new hash(10);
            testHash.doubleProbing(data);

            Console.WriteLine("~TOTAL COLLISION COUNT~");
            Console.WriteLine("Double:   " + testHash.collisionCount + " Collisions");
        }

        static void Main(string[] args)
        {
            //test();

            Random rng = new Random();
            int[] data = new int[500];

            for (int i = 0; i < 500; i++)
                data[i] = rng.Next();

            hash lin = new hash(1001);
            lin.linearProbing(data);

            Console.WriteLine();

            hash quad = new hash(1001);
            quad.quadraticProbing(data);

            Console.WriteLine();

            hash dou = new hash(1001);
            dou.doubleProbing(data);

            Console.WriteLine();

            Console.WriteLine("~TOTAL COLLISION COUNT~");
            Console.WriteLine("Linear:      " + lin.collisionCount + " Collisions");
            Console.WriteLine("Quadratic:   " + quad.collisionCount + " Collisions");
            Console.WriteLine("Double:      " + dou.collisionCount + " Collisions");
        }
    }
}
