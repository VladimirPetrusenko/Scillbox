using System;
using System.Threading;
using System.Threading.Tasks;


namespace Petrusenko_Skillbox._15._11
{
    class Program
    {
        static void PrintMethod_1(object word)
        {
            Thread.CurrentThread.Name = "Thread - 1";
            for (int i = 0; i < 15; i++)
            {
                Console.WriteLine($"Слово <{word}> в {i+1}-й раз выводится в консоль в потоке {Thread.CurrentThread.Name}");
                Thread.Sleep(1000);
            }
        }

        static void PrintMethod_2(object word)
        {
            Thread.CurrentThread.Name = "Thread - 2";
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"Слово <{word}> в {i+1}-й раз выводится в консоль в потоке {Thread.CurrentThread.Name}");
                Thread.Sleep(2000);
            }
        }

        static async Task<int> SumCalculateAsync (int number)
        {
            int result = 0;
            for (int i = 0; i < number; i++)
            {
                result += i;
                Console.WriteLine($"Значение счетчика, выполняемого в рамках асинхронной задачи - {i}");
                await Task.Delay(3000);
            }
            return result;
        }

        static async Task Main(string[] args)
        {
            Task<int> myTask = SumCalculateAsync(10);

            ParameterizedThreadStart pth1 = new ParameterizedThreadStart(PrintMethod_1);
            Thread thread1 = new Thread(PrintMethod_1);
            thread1.Start("Skillbox");

            ParameterizedThreadStart pth2 = new ParameterizedThreadStart(PrintMethod_2);
            Thread thread2 = new Thread(PrintMethod_2);
            thread2.Start("Developer");

            myTask.Wait();
            int result = myTask.Result;
            Console.WriteLine($"Результат: {result}");
        }
    }
}
