using System;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            String message = String.Empty;
            while (true)
            {
                message = Encode();
                TCPClient.Connect(message);
            }
        }

        static string Encode()
        {
            return $"{GetCurrentNamespace()},{generateRandomSeq(5)}";
        }

        static string generateRandomSeq(int strLength)
        {
            Random rand = new Random();
            const string allowedChars = @"!@#$%^&*()-_=+[{]}\|;:',<.>/?";
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
