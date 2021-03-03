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

        private IEnumerable<string> Compute()
        {
            while (true)
            {

                var rnd = new Random(Guid.NewGuid().GetHashCode());

                var sleeptime = rnd.Next(500, 2000);

                var stringsize = rnd.Next(5, 10);

                Thread.Sleep(sleeptime);

                yield return RandomString(stringsize, rnd);

            }
        }
        private void ExecuteThread(ListView listView)
        {
            var id = Thread.CurrentThread.ManagedThreadId.ToString();

            foreach (var result in Compute())
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

        public bool Stop()
        {
            return StopThreads();
        }

        private bool CreateThreadList(int threadcount, ListView listView)
        {
            try
            {
                for (var i = 0; i < threadcount; i++)
                {
                    var thread = _threadPool.ElementAtOrDefault(i);
                    if (thread != null && thread.ThreadState == ThreadState.Suspended)
                    {
                        thread.Resume();
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

            return _threadPool.Count != 0;
        }

        public bool Execute(int threadcount, ListView listView)
        {
            if(CreateThreadList(threadcount, listView))
            {
                StartThreads();
            } 
            else
            {
                return false;
            }

            return true;
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
            catch (Exception)
            {
                // ignored
            }
        }

        public void DeleteThreads()
        {

            foreach (var thread in _threadPool)
            {
                try
                {
                    if(thread.ThreadState == ThreadState.Suspended)
                    {
                        thread.Resume();
                    }
                    thread.Abort();
                }
                catch (ThreadAbortException)
                {

                }

            }
            _threadPool.Clear();
        }

        public bool StopThreads()
        {
            foreach (var thread in _threadPool)
            {
                try
                {
                    thread?.Suspend();
                }
                catch (Exception)
                {
                    return false;
                }
            }

            return true;
        }
        public  string RandomString(int length, Random rnd)
        {
            const string chars = "abcdefghijklmnopqrstuvwyxzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789;'[[],./!@#$%^&*()_+|?/";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[rnd.Next(s.Length)]).ToArray());
        }
    }
}
