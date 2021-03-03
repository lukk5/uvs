using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using UVS.Logic;

namespace UVS
{
    class Computing : IComputing
    {
        
        List<Thread> threads = new List<Thread>();

        public Computing()
        {
            

        }

        private void Compute(Main main)
        {
            while (true)
            {
                var id = Thread.CurrentThread.ManagedThreadId;

                var rnd = new Random(Guid.NewGuid().GetHashCode());

                int sleeptime = rnd.Next(500, 2000);

                int stringsize = rnd.Next(5, 10);

                Thread.Sleep(sleeptime);

                var result = RandomString(stringsize, rnd);

                main.UpdateUI(result, id);
            }
        }

        public void Stop()
        {
            StopThreads();
        }

        private bool CreateThreadList(int threadcount, Main main)
        {
            try
            {
                for (int i = 0; i < threadcount; i++)
                {
                    Thread thread = new Thread(() => Compute(main));
                    threads.Add(thread);
                }
            }
            catch (Exception) {
                return false;
            }

            if(threads.Count==0)
            {
                return false;
            }

            return true;
        }


        public void Execute(int threadcount, Main main)
        {
            if(CreateThreadList(threadcount, main))
            {
                StartThreads();
            }
            
        }

        private void StartThreads()
        {
            try
            {
                foreach (var thread in threads)
                {
                    thread.Start();
                }
            }
            catch (Exception) { }
        }

        public void StopThreads()
        {
            foreach (var thread in threads)
            {
                try
                {
                    thread.Abort();
                }
                catch (ThreadAbortException)
                {

                }
            }

            threads.Clear();
        }
        public  string RandomString(int length, Random rnd)
        {
            const string chars = "abcdefghijklmnopqrstuvwyxzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789;'[[],./!@#$%^&*()_+|?/";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[rnd.Next(s.Length)]).ToArray());
        }
    }
}
