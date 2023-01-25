/*---------------------------------------------------------------------------------------
 Author:      Seth J. Gibson
 Course:      CIS 263-01
 Program:     Assignment 3
 Description: This program utilizes and tests the functionality of linked lists, stacks 
                and queues. The linked list is tested by filling it with odd numbers, 
                then switching the order of '9' and '11' by adjusting the links between
                nodes, not the values within nodes. The stack is tested by filling it
                with values, printing contents to verify its functionality, removing some 
                of the values, printing again, then completely emptying it before 
                printing one last time. The same is done to test the functionality of the 
                queue.
---------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;

namespace Assignment3
{
    public class singleNode
    {
        public singleNode nextNode { get; set; }
        public int data { get; set; }
    }

    public struct singleList
    {
        private singleNode singleHead;
        private singleNode singleTail;
        public singleNode head { get { return singleHead; } set { singleHead = value; } }

        public singleNode tail
        {
            get
            {
                singleNode cursor = head;
                if (cursor == null)
                    return null;
                while (cursor.nextNode != null)
                    cursor = cursor.nextNode;
                return cursor;
            }
        }

        public void addNode(int newData)
        {
            singleNode newEntry = new singleNode { data = newData };
            if (head == null)
                head = newEntry;
            else
                tail.nextNode = newEntry;   
        }

        public void printList()
        {
            singleNode cursor = head;
            while (cursor != null)
            {
                Console.Write(cursor.data + " ");
                cursor = cursor.nextNode;
            }
            Console.WriteLine("");
        }

        public void swapNodes(int firstPlace, int secondPlace)
        {
            if (firstPlace == secondPlace || firstPlace < 0 || secondPlace < 0)
                return;

            singleNode c1 = head, c2 = head;
            singleNode c1T = new singleNode();
            singleNode c2T = new singleNode();
            for (int i = 0; i < firstPlace; i++)
            {
                c1T = c1;
                c1 = c1.nextNode;
            }

            for (int j = 0; j < secondPlace; j++)
            {
                c2 = c2.nextNode;
            }
            c2T.nextNode = c2.nextNode;

            c1T.nextNode = c2;
            c2.nextNode = c1;
            c1.nextNode = c2T.nextNode;
        }
    }

    public class doubleNode
    {
        public doubleNode nextNode { get; set; }
        public doubleNode lastNode { get; set; }
        public int data { get; set; }
    }

    public struct doubleList
    {
        private doubleNode doubleHead;
        private doubleNode doubleTail;
        public doubleNode head { get { return doubleHead; } set { doubleHead = value; } }

        public doubleNode tail
        {
            get
            {
                doubleNode cursor = head;
                if (cursor == null)
                    return null;
                while (cursor.nextNode != null)
                    cursor = cursor.nextNode;
                return cursor;
            }
        }

        public void addNode(int newData)
        {
            doubleNode newEntry = new doubleNode { data = newData };
            if (head == null)
                head = newEntry;
            else
            {
                newEntry.lastNode = tail;
                tail.nextNode = newEntry;
            }
        }

        public void printList()
        {
            doubleNode cursor = head;
            while (cursor != null)
            {
                Console.Write(cursor.data + " ");
                cursor = cursor.nextNode;
            }
            Console.WriteLine("");
        }

        public void swapNodes(int firstPlace, int secondPlace)
        {
            if (firstPlace == secondPlace || firstPlace < 0 || secondPlace < 0)
                return;

            doubleNode c1 = head, c2 = head;
            doubleNode c1T = new doubleNode();
            doubleNode c2T = new doubleNode();
            doubleNode[] temp = new doubleNode[4];
            for (int i = 0; i < firstPlace; i++)
            {
                c1T = c1;
                c1 = c1.nextNode;
            }

            for (int j = 0; j < secondPlace; j++)
            {
                c2 = c2.nextNode;
            }
            c2T = c2.nextNode;

            c1T.nextNode = c2;
            c2.nextNode = c1;
            c1.nextNode = c2T;

            c2T.lastNode = c1;
            c1.lastNode = c2;
            c2.lastNode = c1T;
        }
    }

    public class stack
    {
        private int[] dataArray = new int[5];
        private int top = -1;

        public bool isEmpty()
        {
            if (top == -1)
                return true;
            else
                return false;
        }

        public void push(int input)
        {
            if (!(top <= 4))
                return;
            top++;
            dataArray[top] = input;
        }

        public int? pop()
        {
            if ((isEmpty()))
                return null;
            top--;
            return dataArray[top + 1];
        }

        public void printStack()
        {
            for (int i = 0; i <= top; i++)
                Console.Write(dataArray[i] + " ");
            Console.WriteLine("");
        }
    }

    public class queue
    {
        private int[] dataArray = new int[5];
        private int size = 0, tail = 0, head = 0;

        private static void shift<T>(ref T[] array)
        {
            T[] newArray = new T[array.Length];

            for (int i = 1; i < array.Length; i++)
                newArray[(i + array.Length + -1 % array.Length) % array.Length] = array[i];

            array = newArray;
        }

        public bool isEmpty()
        {
            if (size == 0)
                return true;
            else
                return false;
        }

        public void enqueue(int input)
        {
            if (!(size <= 4))
                return;
            dataArray[tail] = input;
            size++;
            if (tail == dataArray.Length)
                tail = 0;
            else
                tail++;
        }

        public int? dequeue()
        {
            if (isEmpty())
                return null;
            int x = dataArray[head];
            size--;
            tail--;
            shift(ref dataArray);

            return x;
        }

        public void printQueue()
        {
            for (int i = head; i < tail; i++)
                Console.Write(dataArray[i] + " ");
            Console.WriteLine("");
        }
    }

    class Program
    {
        public static void partOne()
        {
            Console.WriteLine("Single Linked List:");
            singleList singleLinkedList = default(singleList);
            singleLinkedList.addNode(3);
            singleLinkedList.addNode(5);
            singleLinkedList.addNode(7);
            singleLinkedList.addNode(9);
            singleLinkedList.addNode(11);
            singleLinkedList.addNode(13);
            singleLinkedList.printList();

            singleLinkedList.swapNodes(3, 4);
            singleLinkedList.printList();

            Console.WriteLine("Double Linked List:");
            doubleList doubleLinkedList = default(doubleList);
            doubleLinkedList.addNode(3);
            doubleLinkedList.addNode(5);
            doubleLinkedList.addNode(7);
            doubleLinkedList.addNode(9);
            doubleLinkedList.addNode(11);
            doubleLinkedList.addNode(13);
            doubleLinkedList.printList();

            doubleLinkedList.swapNodes(3, 4);
            doubleLinkedList.printList();
            Console.WriteLine();
        }

        public static void partTwoStack()
        {
            Console.WriteLine("Stack Test:");
            stack testStack = new stack();

            testStack.push(1);
            testStack.push(2);
            testStack.push(3);
            testStack.printStack();

            Console.WriteLine("Popping " + testStack.pop());
            Console.WriteLine("Popping " + testStack.pop());
            testStack.push(4);
            testStack.printStack();

            Console.WriteLine("Popping " + testStack.pop());
            Console.WriteLine("Popping " + testStack.pop());
            testStack.printStack();
        }

        public static void partTwoQueue()
        {
            Console.WriteLine("Queue Test:");
            queue testQueue = new queue();

            testQueue.enqueue(1);
            testQueue.enqueue(2);
            testQueue.enqueue(3);
            testQueue.printQueue();

            Console.WriteLine("Dequeueing " + testQueue.dequeue());
            Console.WriteLine("Dequeueing " + testQueue.dequeue());
            testQueue.enqueue(4);
            testQueue.printQueue();

            Console.WriteLine("Dequeueing " + testQueue.dequeue());
            Console.WriteLine("Dequeueing " + testQueue.dequeue());
            testQueue.printQueue();
        }

        static void Main(string[] args)
        {
            partOne();
            partTwoStack();
            partTwoQueue();
        }
    }
}
