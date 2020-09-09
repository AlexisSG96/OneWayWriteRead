using System;

namespace Numbers
{
    class Program
    {
        static void Main(string[] args)
        {
            String message = String.Empty;
            int counter = 0;
            while (counter < 10000)
            {
                message = Encode();
                TCPClient.Connect(message);
                counter++;
            }
        }

        static string Encode()
        {
            return $"{GetCurrentNamespace()},{generateRandomSeq(5)}";
        }

        static string generateRandomSeq(int strLength)
        {
            Random rand = new Random();
            const string allowedChars = "0123456789";
            char[] chars = new char[strLength];
            for (int i = 0; i < strLength; i++)
            {
                chars[i] = allowedChars[rand.Next(0, allowedChars.Length)];
            }
            return new string(chars);
        }
        static string GetCurrentNamespace()
        {
            return System.Reflection.Assembly.GetExecutingAssembly().EntryPoint.DeclaringType.Namespace;
        }
    }
}
