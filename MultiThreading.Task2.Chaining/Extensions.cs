using System;

namespace MultiThreading.Task2.Chaining
{
    public static class Extensions
    {
        public static void Print(this int[] integers)
        {
            Console.Write("[");

            foreach (var integer in integers)
            {
                Console.Write($"{integer} ");
            }

            Console.Write("]");

            Console.WriteLine();
        }
    }
}
