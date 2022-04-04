using System;
using static System.Console;
using System.Collections.Generic;

namespace Lab5ASD
{
    class Program
    {
        static List<int> Tosort = new List<int>();
        static void Main()
        {
            int M = 0, N = 0;

            while (true)
            {
                Write("Enter Rows: "); bool a = int.TryParse(ReadLine(), out int b);
                if (a == true && b > 1)
                    M = b;
                Write("Enter Cols: "); a = int.TryParse(ReadLine(), out b);
                if (a == true && b > 1)
                { N = b; break; }
                else
                { WriteLine("Incorrect input!"); continue; }
            }

            while (true)
            {
                Write("Control (1) or random (2)?: "); bool a = int.TryParse(ReadLine(), out int b);
                if (a == true && b == 1)
                { GenMatControl(M, N); break; }
                else if (a == true && b == 2)
                { GenMatRand(M, N); break; }
                else
                { WriteLine("Incorrect input!"); continue; }
            }

            WriteLine("List to sort:");
            PrintList();

            SortFast(0, Tosort.Count - 1);
            WriteLine("Sorted:");
            PrintList();

        }
        static int[,] GenMatControl(int Row, int Col)
        {
            int[,] Mat = new int[Row, Col];
            int count = 0;
            for (int i = 0; i < Row; i++)
            {
                for (int j = 0; j < Col; j++)
                {
                    Mat[i, j] = count;
                    count++;
                    if (i % 2 == 1 && i != j && i + j != (Row - 1))
                    { Console.ForegroundColor = ConsoleColor.Green; Write(Mat[i, j] + "\t"); Console.ForegroundColor = ConsoleColor.White; Tosort.Add(Mat[i, j]); }
                    else
                    { Console.ForegroundColor = ConsoleColor.Red; Write(Mat[i, j] + "\t"); Console.ForegroundColor = ConsoleColor.White; }
                }
                WriteLine();
            }
            return Mat;
        }
        static void GenMatRand(int Row, int Col)
        {
            Random rand = new Random();
            int[,] Mat = new int[Row, Col];
            for (int i = 0; i < Row; i++)
            {
                for (int j = 0; j < Col; j++)
                {
                    Mat[i, j] = rand.Next(1, 31);
                    if (i % 2 == 1 && i != j && i + j != (Row - 1))
                    { Console.ForegroundColor = ConsoleColor.Green; Write(Mat[i, j] + "\t"); Console.ForegroundColor = ConsoleColor.White; Tosort.Add(Mat[i, j]); }
                    else
                    { Console.ForegroundColor = ConsoleColor.Red; Write(Mat[i, j] + "\t"); Console.ForegroundColor = ConsoleColor.White; }
                }
                WriteLine();
            }
        }


        
        static (int,int) MainAlgorithm(int left, int right)
        {
            int i = left, eq = left, j = right, pivot = Tosort[left];
            while (i <= j)
            {
                if (Tosort[i] > pivot)
                { Swap(eq, i); eq++; i++; } //push left side ->
                else if (Tosort[i] < pivot)
                { Swap(i, j); j--; } //push right side <-
                else
                { i++; }
            }
            return (eq, j); //lowest and highest index of equal values
        }
        static void SortFast(int lo, int hi)
        {
            if (lo >= hi)
                return;
            int sm, gr;
            (gr, sm) = MainAlgorithm(lo, hi); //greater and smaller values (333|2|111)

            SortFast(lo, gr-1); 
            SortFast(sm+1, hi);
        }


        //helper methods
        static void PrintList()
        {
            foreach (int i in Tosort)
                Write(i + " ");
            WriteLine();
        }
        static void Swap(int id1, int id2)
        {
            int buff = Tosort[id1];
            Tosort[id1] = Tosort[id2];
            Tosort[id2] = buff;
        }

    }
}
