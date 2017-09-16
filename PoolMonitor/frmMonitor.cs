using PoolLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PoolMonitor
{
    public partial class frmMonitor : Form
    {
        String sFileName;
        List<clsPoolBase> listPool;
        BindingSource bsListPool;
        clsPoolBase poolCurrent;
        int iCountCheck = 0;
        public frmMonitor()
        {
            InitializeComponent();
            //listPool = new List<clsPoolBase>();
        }

        private void btLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Pool Monitor Files(*.pmf)|*pmf";
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                sFileName = ofd.FileName;
                this.Text = sFileName;
                Properties.Settings.Default.FileName = sFileName;
                Properties.Settings.Default.Save();
                dgvPoolList.Enabled = true;
                listPool = Utils.ReadFromXmlFile<List<clsPoolBase>>(sFileName);
                bsListPool = new BindingSource { DataSource = listPool };
                dgvPoolList.DataSource = bsListPool;
            }
        }

        private void btNew_Click(object sender, EventArgs e)
        {
            SaveFileDialog sdf = new SaveFileDialog();
            sdf.Filter = "Pool Monitor Files(*.pmf)|*pmf";
            if(sdf.ShowDialog() == DialogResult.OK)
            {
                sFileName = sdf.FileName;
                this.Text = sFileName;
                Properties.Settings.Default.FileName = sFileName;
                Properties.Settings.Default.Save();
                dgvPoolList.Enabled = true;
                listPool = new List<clsPoolBase>();
                bsListPool = new BindingSource { DataSource = listPool };
                dgvPoolList.DataSource = bsListPool;
            }
        }

        private void addNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(bsListPool != null)
            {
                clsPoolBase objNiceHash = new clsPoolBase();
                try
                {
                    objNiceHash.PoolName = "NiceHash";
                    //listPool.Add(objNiceHash);
                    bsListPool.Add(objNiceHash);
                    //bsListPool.Add(objNiceHash);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(dgvPoolList.CurrentRow != null)
            {
                dgvPoolList.Rows.Remove(dgvPoolList.CurrentRow);
            }
        }

        private void btUpdate_Click(object sender, EventArgs e)
        {
            if(poolCurrent != null)
            {
                poolCurrent.PoolName = cbPoolName.Text;
                poolCurrent.WorkerName = tbWorker.Text;
                poolCurrent.Address = tbAddress.Text;
                poolCurrent.Email = tbEmail.Text;
                poolCurrent.TelegramID = tbTelegramID.Text;
                dgvPoolList.Refresh();
            }
            if(sFileName.Length > 0)
            {
                Utils.WriteToXmlFile<List<clsPoolBase>>(sFileName, listPool);
            }
        }

        private void dgvPoolList_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dgvPoolList.CurrentRow != null)
            {
                poolCurrent = (clsPoolBase)dgvPoolList.CurrentRow.DataBoundItem;
                if (poolCurrent != null)
                {
                    cbPoolName.Text = poolCurrent.PoolName;
                    tbWorker.Text = poolCurrent.WorkerName;
                    tbAddress.Text = poolCurrent.Address;
                    tbEmail.Text = poolCurrent.Email;
                    tbTelegramID.Text = poolCurrent.TelegramID;
                }
            }
        }

        //private void btEmail_Click(object sender, EventArgs e)
        //{
        //    //if (dgvPoolList.CurrentRow != null)
        //    //{
        //    //    Utils.SendEmail(tbEmail.Text, "Test Email", "Test Email");
        //    //    MessageBox.Show("Send Mail Finish");
        //    //}
        //}


        private void timer1_Tick(object sender, EventArgs e)
        {
            if(iCountCheck == 72)
            {
                iCountCheck = 0;
                foreach(clsPoolBase p in listPool)
                {
                    p.NiceHashCurrentSpeed();
                    p.CheckAndEmailStatus(true);
                }
            }
            else
            {
                iCountCheck++;
                foreach (clsPoolBase p in listPool)
                {
                    p.NiceHashCurrentSpeed();
                    p.CheckAndEmailStatus();
                }
            }
            Text = sFileName + " " + iCountCheck.ToString();
        }

        private void btStartTimer_Click(object sender, EventArgs e)
        {
            timer1.Interval = Int32.Parse(tbTimer.Text);
            if(timer1.Enabled)
            {
                timer1.Stop();
                btStartTimer.Text = "Start";
            }
            else
            {
                timer1.Start();
                btStartTimer.Text = "Stop";
            }
        }

        private void frmMonitor_Load(object sender, EventArgs e)
        {
            sFileName = Properties.Settings.Default.FileName;
            if(System.IO.File.Exists(sFileName))
            {
                dgvPoolList.Enabled = true;
                listPool = Utils.ReadFromXmlFile<List<clsPoolBase>>(sFileName);
                bsListPool = new BindingSource { DataSource = listPool };
                dgvPoolList.DataSource = bsListPool;
                timer1.Start();
                btStartTimer.Text = "Stop";
            }
        }
    }
}
