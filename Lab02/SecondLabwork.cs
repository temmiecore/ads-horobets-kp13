using System;
using System.IO;
using System.Collections.Generic;

namespace SecondLab
{
    class Program
    {
        static void Main()
        {
            int M, N; int[,] A;
            int Sum = 0;
            List<string> biggernumbers = new List<string>();

            while (true)
            {
                Console.Write("Enter Rows or Columns (square matrix): "); bool a = int.TryParse(Console.ReadLine(), out int b);
                if (a == true && b > 1)
                { M = b; N = M; break; }
                else
                { Console.WriteLine("Incorrect input!"); continue; }
            }
            while (true)
            {
                Console.Write("Control (1) or random (2)?: "); bool a = int.TryParse(Console.ReadLine(), out int b);
                if (a == true && b == 1)
                { A = GenMatControl(M, N); break; }
                else if (a == true && b == 2)
                { A = GenMatRand(M, N); break; }
                else
                { Console.WriteLine("Incorrect input!"); continue; }
            }

            Console.WriteLine("Upper part: ");
            int Colmin = 1, Colmax = N, Rowmin = 0, Rowmax = M - 1;
            int Min = int.MaxValue;
            while (Colmax != 0)
            {
                for (int j = Colmin; j < Colmax; j++)
                {
                    Console.Write(A[Rowmin, j] + "  ");
                    if (A[Rowmin, j] < Min)
                        Min = A[Rowmin, j];
                }
                Rowmax--; Rowmin++;
                for (int i = Rowmin; i < Rowmax; i++)
                {
                    Console.Write(A[i, Colmax - 1] + "  ");
                    if (A[i, Colmax - 1] < Min)
                        Min = A[i, Colmax - 1];
                }
                Colmax--; Colmin++;
                for (int k = 0; k < Rowmax - Rowmin + 1; k++)
                {
                    Console.Write(A[Rowmax - k, Colmax - k] + "  ");
                    if (A[Rowmax - k, Colmax - k] < Min)
                        Min = A[Rowmax - k, Colmax - k];
                }
                Colmin++; Rowmax--;
            }

            Console.WriteLine("\nPart below:");
            int p = 0, q = 0;
            while (q != M && p != N)
            {
                Sum += A[q, p];
                q++; p++;
            }

            Sum /= 2;
 
            Colmin = 0; Colmax = N; Rowmin = 0; Rowmax = M;
            while (Colmax != 0 && Colmin != Colmax && Rowmax != 0)
            {
                for (int k = 0; k < Rowmax - Rowmin; k++)
                    Console.Write(A[Rowmin + k, Colmin + k] + "  ");
                Colmax--; Rowmin++;
                for (int j = Colmax - 1; j > Colmin; j--)
                    Console.Write(A[Rowmax - 1, j] + "  ");
                Rowmax--; Colmax--;
                for (int i = Rowmax; i >= Rowmin; i--)
                    Console.Write(A[i, Colmin] + "  ");
                Colmin++; Rowmin++;
            }

            for (int i = 0; i < M; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    string elm;
                    if (i > j)
                    {
                        if (A[i, j] > Sum)
                        {
                            elm = "A["+Convert.ToString(i+1) + ":" + Convert.ToString(j+1)+"] = "+A[i,j];
                            biggernumbers.Add(elm);
                        }
                    }
                    else
                        break;
                }
            }

            Console.WriteLine($"\nMin. element in upper part: {Min}");
            if (biggernumbers.Count == 0)
                Console.WriteLine($"No elements bigger then sum/2 = {Sum}!");
            else
            {
                Console.WriteLine($"Elements, bigger then sum/2 = {Sum}:");
                foreach (string a in biggernumbers)
                    Console.Write(a+"  ");
            }
            Console.WriteLine();
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
                    Console.Write(Mat[i, j] + "\t");
                }
                Console.WriteLine();
            }
            return Mat;
        }

        static int[,] GenMatRand(int Row, int Col)
        {
            Random rand = new Random();
            int[,] Mat = new int[Row, Col];
            for (int i = 0; i < Row; i++)
            {
                for (int j = 0; j < Col; j++)
                {
                    Mat[i, j] = rand.Next(1, 21);
                    Console.Write(Mat[i, j] + "\t");
                }
                Console.WriteLine();
            }
            return Mat;
        }
    }
}