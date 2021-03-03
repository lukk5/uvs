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

        public Main()
        {
            InitializeComponent();
            SetUI();
        }

        private void SetUI()
        {
            threadcount.SelectedIndex = 0;

            listView1.View = View.Details;
            listView1.FullRowSelect = true;

            listView1.Columns.Add("ThreadID", 80);
            listView1.Columns.Add("Result", 150);
        }

        private void Start()
        {
            var thcount = GetThreadCount();

            if (thcount != 0)
            {
               _computing.Execute(thcount, listView1);  
            }
        }

        private void Stop()
        {
            _computing.Stop();

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
    }
}
