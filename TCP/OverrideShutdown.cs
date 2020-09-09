using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Transactions;

namespace TCP
{
    class OverrideShutdown
    {
        public int CurrentItteration
        { get; set; }

        public OverrideShutdown(int i)
        {
            this.CurrentItteration = i;
            Type myType = typeof(TCP.OverrideShutdown);
            Console.WriteLine($"Instance of {myType.Module} create on {myType.Namespace}");
        }
        public void AppendToFile(string message)
        {
            const string datePatt = @"yyyy mm dd H:mm:ss";
            DateTime dateTimeUTC = DateTime.UtcNow;
            string dtString = "[" + dateTimeUTC.ToString(datePatt) + " UTC] " + message;
            using (StreamWriter stream = File.AppendText("Shutdown.txt"))
            {
                stream.WriteLine($"{dtString}");
            }
        }
        ~OverrideShutdown()
        {
            AppendToFile($"This was created in Destructor Itteration: {CurrentItteration}");
        }
    }
}
