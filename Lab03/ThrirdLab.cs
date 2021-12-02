using System;
using static System.Console;
using System.Collections.Generic;

namespace Lab3ADS
{
    class Program
    {
        static void Main()
        {
            Random rnd = new Random();
            int N;
            while (true)
            {
                Write("Input N: "); bool check = int.TryParse(ReadLine(), out N);
                if (check == true && N > 0)
                    break;
                else
                    WriteLine("Incorrect input!");
            }

            int[] m = new int[N + 15];
            Write("\nInput array:\n");
            for (int i = 0; i < N + 15; i++)
            {
                m[i] = rnd.Next(1000, 2001);
                Write($"{m[i]} ");
            }
            WriteLine("\n\nResult array:");

            List<int> m1 = new List<int>(), m2 = new List<int>(), m3 = new List<int>();
            for (int i = 0; i < N + 15; i++)
            {
                if (m[i] % 2 == 0 && m[i] % N == 0)
                    m1.Add(m[i]);
                else if (m[i] % 2 == 1 && m[i] % N == 1)
                    m2.Add(m[i]);
                else
                    m3.Add(m[i]);
            }

            SortComb(m1); SortComb(m2);

            Console.ForegroundColor = ConsoleColor.Green;
            int j = 0;
            foreach (int a in m1)
            { m[j] = a; Write(m[j] + " "); j++; }
            Console.ForegroundColor = ConsoleColor.Cyan;
            foreach (int a in m2)
            { m[j] = a; Write(m[j] + " "); j++; }
            Console.ForegroundColor = ConsoleColor.Yellow;
            foreach (int a in m3)
            { m[j] = a; Write(m[j] + " "); j++; }
            Console.ForegroundColor = ConsoleColor.White;
            WriteLine("\nGreen - 1st condition, cyan - 2nd, yellow - no sort");
        }
        static void SortComb(List<int> m)
        {
            int gap = m.Count;
            bool sorted = false;

            while (gap != 1 || sorted == false)
            {
                sorted = true;
                gap = GapShrink(gap);

                for (int i = 0; i < m.Count - gap; i++)
                {
                    if (m[i] < m[i + gap])
                    {
                        int temp = m[i];
                        m[i] = m[i + gap];
                        m[i + gap] = temp;

                        sorted = false;
                    }
                }
            }
        }
        static int GapShrink(int gap)
        {
            gap = (gap * 10) / 13;
            if (gap < 1)
                return 1;
            return gap;
        }
    }
}
