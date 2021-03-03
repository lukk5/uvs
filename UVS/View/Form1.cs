using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using UVS.Logic;

namespace UVS
{
    public partial class Main : Form
    {

        private IComputing _computing = new Computing();

        private bool _started;

        public Main()
        {
            InitializeComponent();
            SetUi();
        }

        private void SetUi()
        {
            threadcount.SelectedIndex = 0;

            listView1.View = View.Details;
            listView1.FullRowSelect = true;

            listView1.Columns.Add("ThreadID", 80);
            listView1.Columns.Add("Result", 150);
        }

        private void Start()
        {
            if(_started)
            {
                return;
            }

            var count = GetThreadCount();

            if (count != 0)
            {
              _started = _computing.Execute(count, listView1);
            }
        }

        private void Stop()
        {
            if (_computing.Stop())
            {
                _started = false;
            }
        }

        private int GetThreadCount()
        {
            var s = threadcount.Text;
            return int.Parse(s);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Start();  
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Stop();
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            _computing.DeleteThreads();
            _computing = null;
        }
    }
}
