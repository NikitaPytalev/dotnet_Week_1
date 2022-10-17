/*
 * 2.	Write a program, which creates a chain of four Tasks.
 * First Task – creates an array of 10 random integer.
 * Second Task – multiplies this array with another random integer.
 * Third Task – sorts this array by ascending.
 * Fourth Task – calculates the average value. All this tasks should print the values to console.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiThreading.Task2.Chaining
{
    class Program
    {
        static Random random = new Random();

        static async Task Main(string[] args)
        {
            Console.WriteLine(".Net Mentoring Program. MultiThreading V1 ");
            Console.WriteLine("2.	Write a program, which creates a chain of four Tasks.");
            Console.WriteLine("First Task – creates an array of 10 random integer.");
            Console.WriteLine("Second Task – multiplies this array with another random integer.");
            Console.WriteLine("Third Task – sorts this array by ascending.");
            Console.WriteLine("Fourth Task – calculates the average value. All this tasks should print the values to console");
            Console.WriteLine();

            Task chain = Task.Run(() => Console.WriteLine("Chain Started!"))
                .ContinueWith(_ => First())
                .ContinueWith(first => Second(first.Result))
                .ContinueWith(second => Third(second.Result))
                .ContinueWith(third => Fourth(third.Result))
                .ContinueWith(_ => Console.WriteLine("Chain Completed!"));

            Console.WriteLine("Some work while the chain is being processed........");

            await chain;

            Console.ReadLine();
        }

        private static int[] First() 
        {
            var integers = Get10RandomNumbers().ToArray();

            integers.Print();

            return integers;
        }

        private static int[] Second(int[] integers)
        {
            var factor = random.Next(0, 100);

            for (int i = 0; i < integers.Length; i++)
            {
                integers[i] *= factor;
            }

            integers.Print();

            return integers;
        }

        private static int[] Third(int[] integers)
        {
            Array.Sort(integers);

            integers.Print();

            return integers;
        }

        private static int Fourth(int[] integers)
        {
            var average = (int)integers.Average();

            Console.WriteLine($"Average: {average}");

            return average;
        }

        private static IEnumerable<int> Get10RandomNumbers()
        {
            for(int i = 0; i < 10; i++)
            {
                yield return random.Next(0, 100);
            }
        }
    }
}
