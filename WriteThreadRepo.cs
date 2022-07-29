/*
* NAME : WriteThreadRepo
* PURPOSE : The WriteThreadRepo class has been created to write 50 random characters to files in 50 threads  
*/
using System;
using System.Collections.Generic;
using System.Threading;
using System.IO;

namespace A_04
{
    class WriteThreadRepo
    {
        private const int kThreadNum = 50;
        private const int kStringLength= 50;

        private string path;
        private List<Thread> writeThreads;
        private CancelToken cancelToken;
        public WriteThreadRepo(string path, ref CancelToken cancelToken)
        {
            this.path = path;
            this.cancelToken = cancelToken;

            writeThreads = new List<Thread>();
            for (int i = 0; i < kThreadNum; i++) 
            {
                Thread t = new Thread(this.WriteToFile);
                writeThreads.Add(t);
            }
        }

        public void StartAll()
        {
            foreach (Thread t in writeThreads)
            {
                t.Start();
            }
        }

        public void JoinAll()
        {
            foreach (Thread t in writeThreads)
            {
                t.Join();
            }
        }

        public void KillAll()
        {
            foreach (Thread t in writeThreads)
            {
                t.Abort();
            }
        }

        public void WriteToFile()
        {
            while (true)
            {
                lock (cancelToken)
                {
                    if (!cancelToken.Run)
                    {
                        break;
                    }

                    using (StreamWriter sw = File.AppendText(path))
                    {
                        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
                        var stringChars = new char[kStringLength];
                        var random = new Random();
                        for (int i = 0; i < stringChars.Length; i++)
                        {
                            stringChars[i] = chars[random.Next(chars.Length)];
                        }
                        var finalString = new String(stringChars);
                        sw.WriteLine(finalString);
                    }
                }
            }
        }

    }
}
