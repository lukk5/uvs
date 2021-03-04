using System.Windows.Forms;

namespace UVS.Logic
{
    interface IComputing
    {
        bool Execute(int threadcount, ListView listView);

        bool Stop();

        bool Resume();

        bool Suspend();
    }
}
