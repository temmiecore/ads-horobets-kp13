using System;


namespace dateASD
{
    class Program
    {
        static void Main()
        {
            //Leap year check
            int func(int y0)
            {
                if ((y0 % 4 == 0 && y0 % 100 != 0) || (y0 % 400 == 0))
                    return 1;
                else
                    return 0;
            }

            int[,] day = new int[2, 12] { { 31,28,31,30,31,30,31,31,30,31,30,31 }, 
                                          { 31,29,31,30,31,30,31,31,30,31,30,31 } };
            int dj = 0, yj = 0, mj = 0;
            bool corr = false;
            while (corr != true)
            {
                Console.Write("Input day: "); bool d_ = int.TryParse(Console.ReadLine(), out dj);
                Console.Write("Input month: "); bool m_ = int.TryParse(Console.ReadLine(), out mj);
                Console.Write("Input year: "); bool y_ = int.TryParse(Console.ReadLine(), out yj);

                if (dj < 1 || mj < 1 || mj > 12 ||  d_ == false || m_ == false || y_ == false || dj > day[func(yj), mj - 1])
                    Console.WriteLine("Incorrect date input!");
                else
                    corr = true;
            }

            //Days' gap counting
            int d = dj, y = yj, m = mj;
            int gap = 0;
            for (int i = 300; i < y; i++)
            {
                if (i % 100 == 0 && i % 400 != 0)
                    gap++;
            }

            //Converting to Gregorian
            d += gap;
            if (d > day[func(y), m - 1])
            {
                d -= day[func(y), m - 1] - 1;
                m++;
                if (m > 12)
                {
                    m -= 12;
                    y++;
                }
            }

            //Counting how many days have passed
            int count = 0;
            for (int j = 0; j < m-1; j++)
            {
                count += day[func(y), j];
            }
            count += d;

            Console.WriteLine($"Date in Julian calendar:\tDate in Gregorian calendar:\n{dj}: 0{mj}: {yj}\t\t\t{d}: 0{m}: {y}");
            Console.WriteLine($"Days've passed since year started: {count}");
        }
    }
}
