using System;
using static System.Console;

//init Tail, add to Tail / remove from Head | Head = Tail = 0 first | Then when add: Tail++
//Do shift after removing
//IsEmpty()? Tail = Head
//DeleteAll(): Remove till Tail = Head
//Don't print 0's
//If input = 0, return

namespace LabASD6
{
    public struct Queue
    {
        private int tail, size;
        private int[] arr;
        public Queue(int size)
        {
            this.size = size;
            tail = 0;
            arr = new int[size];
        }
        public void Add(int elem)
        {
            if (tail != size)
            {
                arr[tail] = elem; tail++;
                Print();
            }
            else
                IsFull(); 
            
        }
        public void Remove()
        {
            if (tail != 0)
            {
                for (int i = 1; i < size; i++)
                    arr[i - 1] = arr[i];
                if (tail < size)
                    arr[tail] = 0;
                else
                    arr[size-1] = 0;
                tail--;
            }
            else
                WriteLine("Queue is already empty!");
        }
        public void IsFull()
        {
            WriteLine("Queue is full! Emptying...");
            for (int i = 0; i < size; i++)
            {
                Remove(); Print();
            }
        }
        public void Print()
        {
            if (tail == 0)
                WriteLine("Queue is empty!");
            else
                foreach (int el in arr)
                    if (el != 0)
                        Write(el + " ");
            WriteLine();
        }

    }


    class Program
    {
        static void Main()
        {
            Queue queue = new Queue(10);
            while (true)
            {
                Write("Input element to queue (int), or enter 'control' for controlled input: "); string input = ReadLine();
                bool check = int.TryParse(input, out int elem);
                if (input == "control")
                    for (int i = 0; i < 11; i++)
                        queue.Add(i+1);
                else if (!check)
                { WriteLine("Incorrect input, try again!"); continue; }
                else if (elem != 0)
                    queue.Add(elem);
                else
                    return;
            }
        }
    }
}


