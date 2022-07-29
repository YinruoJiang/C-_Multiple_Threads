/*
* FILE : A-04
* PROJECT : PROG2121 - Assignment -04
* PROGRAMMER : Yinruo Jiang
* FIRST VERSION : 2021-10-20
* DESCRIPTION :
* The functions in this file are used to insert character to file and moniter file size concurrently using multithreads
*/
using System;
using System.IO;

namespace A_04
{
    class Program
    {
        static void Main(string[] args)
        {
            CancelToken token = new CancelToken();

            ValidateArgs(args);
            string path = Path.GetFullPath(args[0]);
            long sizeOfFile = long.Parse(args[1]);

            WriteThreadRepo writeThreadRepo = new WriteThreadRepo(path, ref token);
            MonitorThread monitorThread = new MonitorThread(path, sizeOfFile, ref token);

            writeThreadRepo.StartAll();
            monitorThread.Start();

            writeThreadRepo.JoinAll();
            monitorThread.Join();

            Console.WriteLine("Press any key to end...");
            Console.ReadKey();
        }

        static void ValidateArgs(string[] args)
        {
            if (args.Length != 2 || args[1] == "/?")
            {
                Console.WriteLine("Must specify the full file path and file size. ");
                Console.WriteLine("Press any key to end...");
                Console.ReadKey();
                return;
            }
            
            if (!File.Exists(args[0]))
            {
                // the Close() method is called implicitly in finally.
                using(File.Create(args[0])){}
            }

            long number = 0;
            bool canConvert = long.TryParse(args[1], out number);
            if (!canConvert)
            {
                Console.WriteLine("Second Argument should be integer!");
                Console.WriteLine("Press any key to end...");
                Console.ReadKey();
                return;
            }

            if (number < 1000 || number > 20000000)
            {
                Console.WriteLine("Please provide the valid file size which should between 1000 and 20000000!");
                Console.WriteLine("Press any key to end...");
                Console.ReadKey();
                return;
            }  
        }
    }   
}
