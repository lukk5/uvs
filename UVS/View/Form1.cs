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
               _computing.Execute(thcount,this);  
            }
        }

        private void Stop()
        {
            _computing.Stop();

            _computing = null;

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
        public void UpdateUI(string text, int threadumber)
        {
            String[] row = { text, threadumber.ToString() };

            ListViewItem item = new ListViewItem(row);

            if (listView1.InvokeRequired)
            {
                listView1.Invoke(new MethodInvoker(delegate
                {

                    listView1.Items.Add(item);
               
                }));
            }
            else
            {
                listView1.Items.Add(item);
               
            }
        }
    }
}
