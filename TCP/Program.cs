using System;

namespace TCP
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 1)
            {
                foreach (var arg in args)
                {
                    Console.WriteLine($"{arg}");
                }
            }
            string str = String.Empty;
            str = Arguments(10, 20, "World");
            Console.WriteLine(str);
            Console.ReadKey();

        }

        static string Arguments(int i = 0, int j = 1, string s = "Hello")
        {
            return $"{i},{j},{s}";
        }
    }
}
