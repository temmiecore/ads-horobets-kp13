using System;
using static System.Console;
using static System.Math;
class First_Task
{
    static void Main()
    {
        bool input = false;
        double x = 0, y = 0, z = 0;
        while (input == false)
        {
            Write("Input x: "); bool x_ = double.TryParse(ReadLine(), out x);
            Write("Input y: "); bool y_ = double.TryParse(ReadLine(), out y);
            Write("Input z: "); bool z_ = double.TryParse(ReadLine(), out z);
            if (x_ == false || y_ == false || z_ == false)
                WriteLine("Incorrect input!");
            else
                input = true;
        }

        double a, b;
        if (x * x + x * y * y + x * x * z > 0)
            a = 1 + Sin(x) / Sqrt(x * x + x * y * y + x * x * z);
        else
        {
            WriteLine("Something in a is not in its range of valid values!");
            return;
        }

        if (a * a * a + x * x > 0 && a != 0 && y + z / a != 0 && x + a / (y + z / a) != 0)
        {
            b = Log(a * a * a + x * x) / (x + a / (y + z / a));
            WriteLine($"a = {Round(a,3)}\nb = {Round(b,3)}");
        }
        else
            WriteLine($"a = {Round(a,3)}\nSomething in b is not in its range of valid values!");


        
    }
}

