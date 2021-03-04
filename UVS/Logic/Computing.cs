using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using UVS.EF;
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

                var result = RandomString(stringsize, rnd);

                try
                {
                    using (var context = new UVSEntities1())
                    {

                        var row = new UV()
                        {
                            THREADID = Thread.CurrentThread.ManagedThreadId,
                            Time = TimeSpan.FromMilliseconds(sleeptime),
                            Date = DateTime.Now
                        };
                        context.UVS.Add(row);
                        context.SaveChanges();
                    }

                }
                catch (Exception)
                {
                    // ignored
                }

                yield return result;

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
                        listView.Items.Insert(0, item);

                        if (listView.Items.Count == 11)
                        {
                            listView.Items.RemoveAt(10);
                        }

                    }));
                }
                else
                {
                    listView.Items.Insert(0,item); // sortina nuo naujausio
                    if (listView.Items.Count == 11) // kad rodytu tik 10 naujausiu 
                    {
                        listView.Items.RemoveAt(10);
                    }
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

                    var thread = new Thread(() =>
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
                    thread?.Start();
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public bool Resume()
        {
            try
            {
                foreach (var thread in _threadPool)
                {
                    thread?.Resume();
                }

            }
            catch
            {
                return false;
            }

            return true;
        }
        public bool Suspend()
        {
            try
            {
                foreach (var thread in _threadPool)
                {
                   
                   thread?.Suspend();
                   
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool StopThreads()
        {
            if (_threadPool.Count == 0)
            {
                return true;
            }

            foreach (var thread in _threadPool)
            {
                try
                {
                    if (thread.ThreadState == ThreadState.Suspended)
                    {
                        thread?.Resume(); // abort neveikia jeigu threado state yra suspended 
                    }

                    thread?.Abort();
                }
                catch (Exception)
                {
                    return false;
                }
            }
            _threadPool.Clear();
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
