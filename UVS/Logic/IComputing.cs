using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UVS.Logic
{
    interface IComputing
    {
        void Execute(int threadcount,Main main);

        void Stop();

    }
}
