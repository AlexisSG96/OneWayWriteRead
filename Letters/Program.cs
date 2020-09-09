using System;
using System.Collections.Generic;
using System.IO;

namespace Letters
{
    class Program
    {
        static readonly string fileName = GetFileName();
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
            const string allowedChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
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
        static string GetFileName()
        {
            string fileName = String.Empty;
            List<int> fileNum = new List<int> { 1, 2, 3 };
            if (File.Exists(GetCurrentNamespace() + ".txt"))
            {
                for (int i = 0; i < fileNum.Count; i++)
                {
                    if (File.Exists(GetCurrentNamespace() + $"{fileNum[i]}"))
                    {
                        continue;
                    }
                    else
                    {
                        fileName = GetCurrentNamespace() + $"{fileNum[i]}";
                        return fileName;
                    }
                }
            }
            else
            {
                fileName = GetCurrentNamespace();
                return fileName;
            }
            return String.Empty;
        }
    }
}
