using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UVS.Logic
{
    interface IComputing
    {
        bool Execute(int threadcount, ListView listView);

        bool Stop();

        void DeleteThreads();

    }
}
