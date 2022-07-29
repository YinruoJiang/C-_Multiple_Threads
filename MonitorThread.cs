/*
* NAME : MonitorThread
* PURPOSE : The MonitorThread class has been created to moniter the size of file. 
*/
using System;
using System.IO;
using System.Threading;

namespace A_04
{
    class MonitorThread
    {
        private string path;
        private long threadholdSize;
        private CancelToken cancelToken;

        private Thread monitorThread;
        public MonitorThread(string path, long threadholdSize, ref CancelToken cancelToken)
        {
            this.path = path;
            this.threadholdSize = threadholdSize;
            this.cancelToken = cancelToken;

            monitorThread = new Thread(this.MonitorSize);
        }

        public void Start()
        {
            monitorThread.Start();
        }

        public void Join()
        {
            monitorThread.Join();
        }

        public void MonitorSize()
        {
            while (true)
            {
                lock (cancelToken)
                {

                    long currentFileSize = new FileInfo(this.path).Length;

                    if (currentFileSize > threadholdSize)
                    {
                        Console.WriteLine("Final File size is " + currentFileSize);
                        break;
                    }

                    Console.WriteLine("File size is " + currentFileSize);
                }

                Thread.Sleep(1000);
            }

            cancelToken.Run = false;        
        }
    }
}
