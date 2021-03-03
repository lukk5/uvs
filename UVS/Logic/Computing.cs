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
        
        private readonly List<Thread> _threadPool = new List<Thread>();

        public Computing()
        {
            

        }
        private IEnumerable<string> Compute(ListView listView)
        {
            while (true)
            {
                var id = Thread.CurrentThread.ManagedThreadId;

                var rnd = new Random(Guid.NewGuid().GetHashCode());

                int sleeptime = rnd.Next(500, 2000);

                int stringsize = rnd.Next(5, 10);

                Thread.Sleep(sleeptime);

                yield return RandomString(stringsize, rnd);

            }
        }
        private void ExecuteThread(ListView listView)
        {
            var id = Thread.CurrentThread.ManagedThreadId.ToString();

            foreach (var result in Compute(listView))
            {
                string[] row = {id, result};

                var item = new ListViewItem(row);

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
                    var thread = _threadPool.ElementAtOrDefault(threadcount);
                    if (thread != null)
                    {
                        thread.Start();
                        continue;
                    }    

                    thread = new Thread(() =>
                    {
                        ExecuteThread(listView);
                    });
                    _threadPool.Add(thread);
                }
            }
            catch (Exception) {
                return false;
            }

            if(_threadPool.Count==0)
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
                foreach (var thread in _threadPool)
                {
                    thread.Start();
                }
            }
            catch (Exception) { }
        }

        public void StopThreads()
        {
            foreach (var thread in _threadPool)
            {
                try
                {
                    thread.Abort();
                }
                catch (ThreadAbortException)
                {

                }
            }

            _threadPool.Clear();
        }
        public  string RandomString(int length, Random rnd)
        {
            const string chars = "abcdefghijklmnopqrstuvwyxzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789;'[[],./!@#$%^&*()_+|?/";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[rnd.Next(s.Length)]).ToArray());
        }
    }
}
