using System;
using static System.Console;
using System.Collections.Generic;

namespace Lab5ASD
{
    class Program
    {
        static int[] toSort;
        static void Main()
        {
            int M = 0, N = 0;
            List<int> taskList = new List<int>();
            int[,] matx;

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
                { matx = GenMatControl(M, N, taskList); break; }
                else if (a == true && b == 2)
                { matx = GenMatRand(M, N, taskList); break; }
                else
                { WriteLine("Incorrect input!"); continue; }
            }

            toSort = new int[taskList.Count];
            for (int i = 0; i < taskList.Count; i++)
                toSort[i] = taskList[i];

            WriteLine("\nArray to sort:");
            PrintArr();

            SortFast(0, toSort.Length - 1);
            WriteLine("\nSorted:");
            PrintArr();

            WriteLine("\nMatrix with sorted array in it:");
            int ct = 0;
            for (int i = 0; i < matx.GetLength(0); i++)
            {
                for (int j = 0; j < matx.GetLength(1); j++)
                {
                    if (i % 2 == 1 && i != j && (i + j) != (matx.GetLength(0)-1))
                    { matx[i, j] = toSort[ct]; Console.ForegroundColor = ConsoleColor.Green; Write(matx[i, j] + "\t"); ct++; }
                    else
                    { Console.ForegroundColor = ConsoleColor.Red; Write(matx[i, j] + "\t"); Console.ForegroundColor = ConsoleColor.White; }
                }
                WriteLine();
            }


        }
        static int[,] GenMatControl(int Row, int Col, List<int> L)
        {
            int[,] Mat = new int[Row, Col]; int count = 0;
            for (int i = 0; i < Row; i++)
            {
                for (int j = 0; j < Col; j++)
                {
                    Mat[i, j] = count;
                    count++;
                    if (i % 2 == 1 && i != j && i + j != (Row - 1))
                    { Console.ForegroundColor = ConsoleColor.Green; Write(Mat[i, j] + "\t");  L.Add(Mat[i, j]); }
                    else
                    { Console.ForegroundColor = ConsoleColor.Red; Write(Mat[i, j] + "\t"); Console.ForegroundColor = ConsoleColor.White; }
                }
                WriteLine();
            }
            return Mat;
        }
        static int[,] GenMatRand(int Row, int Col, List<int> L)
        {
            Random rand = new Random(); int[,] Mat = new int[Row, Col];
            for (int i = 0; i < Row; i++)
            {
                for (int j = 0; j < Col; j++)
                {
                    Mat[i, j] = rand.Next(1, 31);
                    if (i % 2 == 1 && i != j && i + j != (Row - 1))
                    { Console.ForegroundColor = ConsoleColor.Green; Write(Mat[i, j] + "\t"); Console.ForegroundColor = ConsoleColor.White; L.Add(Mat[i, j]); }
                    else
                    { Console.ForegroundColor = ConsoleColor.Red; Write(Mat[i, j] + "\t"); Console.ForegroundColor = ConsoleColor.White; }
                }
                WriteLine();
            }
            return Mat;
        }



        //main
        static (int, int) DijkstraAl(int left, int right)
        {
            int i = left, eq = left, j = right, pivot = toSort[left];
            while (i <= j)
            {
                if (toSort[i] > pivot)
                { Swap(eq, i); eq++; i++; } //push left side ->
                else if (toSort[i] < pivot)
                { Swap(i, j); j--; } //push right side <-
                else
                    i++;
            }
            return (eq, j); //lowest and highest index of equal values
        }
        static void SortFast(int lo, int hi)
        {
            if (lo >= hi)
                return;
            int sm, gr;
            (gr, sm) = DijkstraAl(lo, hi); //find eq and j

            SortFast(lo, gr - 1);
            SortFast(sm + 1, hi);
        }




        //helper methods
        static void PrintArr()
        {
            foreach (int i in toSort)
                Write(i + " ");
            WriteLine();
        }
        static void Swap(int id1, int id2)
        {
            int buff = toSort[id1];
            toSort[id1] = toSort[id2];
            toSort[id2] = buff;
        }
    }
}
