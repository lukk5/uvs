
namespace UVS
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.threadcount = new System.Windows.Forms.ComboBox();
            this.t = new System.Windows.Forms.Label();
            this.btnstart = new System.Windows.Forms.Button();
            this.btnstop = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.tbnresume = new System.Windows.Forms.Button();
            this.btnsuspend = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // threadcount
            // 
            this.threadcount.FormattingEnabled = true;
            this.threadcount.Items.AddRange(new object[] {
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "14",
            "15"});
            this.threadcount.Location = new System.Drawing.Point(93, 14);
            this.threadcount.Name = "threadcount";
            this.threadcount.Size = new System.Drawing.Size(121, 21);
            this.threadcount.TabIndex = 3;
            // 
            // t
            // 
            this.t.AutoSize = true;
            this.t.Location = new System.Drawing.Point(16, 16);
            this.t.Name = "t";
            this.t.Size = new System.Drawing.Size(74, 13);
            this.t.TabIndex = 1;
            this.t.Text = "&Thread count:";
            // 
            // btnstart
            // 
            this.btnstart.Location = new System.Drawing.Point(220, 14);
            this.btnstart.Name = "btnstart";
            this.btnstart.Size = new System.Drawing.Size(75, 23);
            this.btnstart.TabIndex = 0;
            this.btnstart.Text = "&Start";
            this.btnstart.UseVisualStyleBackColor = true;
            this.btnstart.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnstop
            // 
            this.btnstop.Location = new System.Drawing.Point(220, 39);
            this.btnstop.Name = "btnstop";
            this.btnstop.Size = new System.Drawing.Size(75, 23);
            this.btnstop.TabIndex = 1;
            this.btnstop.Text = "&Stop";
            this.btnstop.UseVisualStyleBackColor = true;
            this.btnstop.Click += new System.EventHandler(this.button2_Click);
            // 
            // listView1
            // 
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(12, 68);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(364, 211);
            this.listView1.TabIndex = 2;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // tbnresume
            // 
            this.tbnresume.Enabled = false;
            this.tbnresume.Location = new System.Drawing.Point(301, 14);
            this.tbnresume.Name = "tbnresume";
            this.tbnresume.Size = new System.Drawing.Size(75, 23);
            this.tbnresume.TabIndex = 4;
            this.tbnresume.Text = "&Resume";
            this.tbnresume.UseVisualStyleBackColor = true;
            this.tbnresume.Click += new System.EventHandler(this.tbnresume_Click);
            // 
            // btnsuspend
            // 
            this.btnsuspend.Enabled = false;
            this.btnsuspend.Location = new System.Drawing.Point(301, 39);
            this.btnsuspend.Name = "btnsuspend";
            this.btnsuspend.Size = new System.Drawing.Size(75, 23);
            this.btnsuspend.TabIndex = 5;
            this.btnsuspend.Text = "&Suspend";
            this.btnsuspend.UseVisualStyleBackColor = true;
            this.btnsuspend.Click += new System.EventHandler(this.btnsuspend_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 290);
            this.Controls.Add(this.btnsuspend);
            this.Controls.Add(this.tbnresume);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.btnstop);
            this.Controls.Add(this.btnstart);
            this.Controls.Add(this.t);
            this.Controls.Add(this.threadcount);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox threadcount;
        private System.Windows.Forms.Label t;
        private System.Windows.Forms.Button btnstart;
        private System.Windows.Forms.Button btnstop;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button tbnresume;
        private System.Windows.Forms.Button btnsuspend;
    }
}

