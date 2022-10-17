/*
*  Create a Task and attach continuations to it according to the following criteria:
   a.    Continuation task should be executed regardless of the result of the parent task.
   b.    Continuation task should be executed when the parent task finished without success.
   c.    Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation
   d.    Continuation task should be executed outside of the thread pool when the parent task would be cancelled
   Demonstrate the work of the each case with console utility.
*/
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading.Task6.Continuation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Create a Task and attach continuations to it according to the following criteria:");
            Console.WriteLine("a.    Continuation task should be executed regardless of the result of the parent task.");
            Console.WriteLine("b.    Continuation task should be executed when the parent task finished without success.");
            Console.WriteLine("c.    Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation.");
            Console.WriteLine("d.    Continuation task should be executed outside of the thread pool when the parent task would be cancelled.");
            Console.WriteLine("Demonstrate the work of the each case with console utility.");
            Console.WriteLine();

            A();
            B();
            C();
            D();

            Console.ReadLine();
        }

        static void A()
        {
            Console.WriteLine("Scenario A");

            Action parentAction = () =>
            {
                Console.WriteLine("Parent running...");
            };

            Action<Task> childAction = parent =>
            {
                Console.WriteLine("Child running...");
                Console.WriteLine($"Parent status: {parent.Status}");
            };

            var parentTask = new Task(parentAction);
            parentTask.Start();

            parentTask.ContinueWith(childAction).Wait(); ;

            Console.WriteLine("Scenario A completed \n");
        }

        static void B()
        {
            Console.WriteLine("Scenario B");

            Action parentAction = () =>
            {
                Console.WriteLine("Parent running...");
                throw new Exception();
            };

            Action<Task> childAction = parent =>
            {
                Console.WriteLine("Child running...");
                Console.WriteLine($"Parent status: {parent.Status}");
            };

            var parentTask = new Task(parentAction);
            parentTask.Start();

            parentTask.ContinueWith(childAction, TaskContinuationOptions.OnlyOnFaulted).Wait();

            Console.WriteLine("Scenario B completed \n");
        }

        static void C()
        {
            Console.WriteLine("Scenario C");

            Action parentAction = () =>
            {
                Console.WriteLine("Parent running...");
                throw new Exception();
            };

            Action<Task> childAction = parent =>
            {
                Console.WriteLine("Child running...");
                Console.WriteLine($"Parent status: {parent.Status}");
            };

            var parentTask = new Task(parentAction);
            parentTask.Start();

            parentTask.ContinueWith(childAction, TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously).Wait();

            Console.WriteLine("Scenario C completed \n");
        }

        static void D()
        {
            Console.WriteLine("Scenario D");

            var cts = new CancellationTokenSource();
            cts.Token.Register(() => Console.WriteLine("Task cancelled"));

            Action parentAction = () =>
            {
                Console.WriteLine("Parent running...");
            };

            Action<Task> childAction = parent =>
            {
                Console.WriteLine("Child running...");
                Console.WriteLine($"Parent status: {parent.Status}");
            };

            var parentTask = new Task(parentAction, cts.Token);
            parentTask.Start();
            cts.Cancel();

            parentTask.ContinueWith(childAction, TaskContinuationOptions.OnlyOnCanceled | TaskContinuationOptions.ExecuteSynchronously);

            Console.WriteLine("Scenario D completed");
        }
    }
}
