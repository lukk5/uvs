using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Windows.Forms;
using UVS.Logic;

namespace UVS
{
    class Computing : IComputing
    {
        
        List<Thread> threads = new List<Thread>();

        public Computing()
        {
            

        }

        private void Compute(ListView listView)
        {
            while (true)
            {
                var id = Thread.CurrentThread.ManagedThreadId;

                var rnd = new Random(Guid.NewGuid().GetHashCode());

                int sleeptime = rnd.Next(500, 2000);

                int stringsize = rnd.Next(5, 10);

                Thread.Sleep(sleeptime);

                var result = RandomString(stringsize, rnd);

                string[] row = { result, id.ToString() };

                ListViewItem item = new ListViewItem(row);

                if (listView.InvokeRequired)
                {
                    listView.Invoke(new MethodInvoker(delegate
                    {

                        listView.Items.Add(item);

                    }));
                }
                else
                {
                    listView.Items.Add(item);

                }
            }
        }

        public void Stop()
        {
            StopThreads();
        }

        private bool CreateThreadList(int threadcount, ListView listView)
        {
            try
            {
                for (int i = 0; i < threadcount; i++)
                {
                    Thread thread = new Thread(() => Compute(listView));
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


        public void Execute(int threadcount, ListView listView)
        {
            if(CreateThreadList(threadcount, listView))
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
