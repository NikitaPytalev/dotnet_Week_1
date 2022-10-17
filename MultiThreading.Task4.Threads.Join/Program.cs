/*
 * 4.	Write a program which recursively creates 10 threads.
 * Each thread should be with the same body and receive a state with integer number, decrement it,
 * print and pass as a state into the newly created thread.
 * Use Thread class for this task and Join for waiting threads.
 * 
 * Implement all of the following options:
 * - a) Use Thread class for this task and Join for waiting threads.
 * - b) ThreadPool class for this task and Semaphore for waiting threads.
 */

using System;
using System.Threading;

namespace MultiThreading.Task4.Threads.Join
{
    class Program
    {
        static Semaphore semaphoreObject = new Semaphore(initialCount: 1, maximumCount: 1);
        static void Main(string[] args)
        {
            Console.WriteLine("4.	Write a program which recursively creates 10 threads.");
            Console.WriteLine("Each thread should be with the same body and receive a state with integer number, decrement it, print and pass as a state into the newly created thread.");
            Console.WriteLine("Implement all of the following options:");
            Console.WriteLine();
            Console.WriteLine("- a) Use Thread class for this task and Join for waiting threads.");
            Console.WriteLine("- b) ThreadPool class for this task and Semaphore for waiting threads.");

            Console.WriteLine();

            CreateThread(10);
            QueueToThreadPool(10);


            Console.ReadLine();
        }

        static void QueueToThreadPool(object state)
        {
            semaphoreObject.WaitOne();

            int number = (int)state;

            if (number == 0) return;

            Console.WriteLine(number--);

            semaphoreObject.Release();

            ThreadPool.QueueUserWorkItem(QueueToThreadPool, number);
        }

        static void CreateThread(object state)
        {
            int number = (int)state;

            if (number == 0) return;

            Console.WriteLine(number--);

            var thread = new Thread(CreateThread);
            thread.Start(number);
            thread.Join();
        }
    }
}
