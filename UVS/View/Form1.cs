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

            btnstart.Enabled = false;
            btnsuspend.Enabled = true;

        }

        private void Stop()
        {
            if (_computing.Stop())
            {
                _started = false;
            }

            btnstart.Enabled = true;
            tbnresume.Enabled = false;
            btnsuspend.Enabled = false;
        }

        private int GetThreadCount()
        {
            var s = threadcount.Text;

            var count = int.Parse(s);

            if (count > 15 || count <= 0)
            {
                MessageBox.Show("Thread count is not valid", "Error");
                return 0;
            }


            return int.Parse(s);
        }

        private void Resume()
        {
            if (_computing.Resume())
            {
                _started = true;
            }

            btnsuspend.Enabled = true;
            tbnresume.Enabled = false;
        }

        private void Suspend()
        {
            if (_computing.Suspend())
            {
                _started = false;
            }

            tbnresume.Enabled = true;
            btnstart.Enabled = false;
            btnsuspend.Enabled = false;
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
            _computing.Stop();
            _computing = null;
        }

        private void tbnresume_Click(object sender, EventArgs e)
        {
            Resume();
        }

        private void btnsuspend_Click(object sender, EventArgs e)
        {
            Suspend();
        }
    }
}
