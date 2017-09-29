using PoolLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
//Test
//Test 2
//Test 3
//Test 4
//Test 5

namespace PoolMonitor
{
    public partial class frmMonitor : Form
    {
        String sFileName;
        List<clsPoolBase> listPool;
        BindingSource bsListPool;
        clsPoolBase poolCurrent;
        int iCountCheck = 1;
        TelegramApi teleApi = new TelegramApi();
        List<TelegramMessage> listWaitingSendMessage = new List<TelegramMessage>();
        //List<TelegramMessage> listSendingMessage = new List<TelegramMessage>();

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
                    dgvPoolList.Refresh();
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
                poolCurrent.Round = Int32.Parse(cbRound.Text);
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
                    cbRound.Text = poolCurrent.Round.ToString();
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


        private async void timer1_Tick(object sender, EventArgs e)
        {
            if(iCountCheck == 72)
            {
                iCountCheck = 1;
                foreach(clsPoolBase p in listPool)
                {
                    p.NiceHashCurrentSpeed();
                    p.CheckAndEmailStatus(true);
                    TelegramMessage msg = p.GetTelegramMessage(true);
                    if (msg != null)
                    {
                        listWaitingSendMessage.Add(msg);
                        //msg.IndexInList = listWaitingSendMessage.IndexOf(msg) + 1;
                    }
                    //p.CheckAndSendTelegram(teleApi, true);
                }
            }
            else
            {
                iCountCheck++;
                foreach (clsPoolBase p in listPool)
                {
                    
                    p.NiceHashCurrentSpeed();
                    p.CheckAndEmailStatus();
                    TelegramMessage msg = p.GetTelegramMessage(((iCountCheck) % p.Round == 0));
                    if (msg != null)
                    {
                        listWaitingSendMessage.Add(msg);
                        //msg.IndexInList = listWaitingSendMessage.IndexOf(msg) + 1;
                    }
                    //p.CheckAndSendTelegram(teleApi,((iCountCheck + listPool.IndexOf(p))%12==0));
                }
            }
            Text = String.Format("{0} {1} {2}", sFileName ,iCountCheck, DateTime.Now);
            dgvPoolList.Refresh();
        }

        private void btStartTimer_Click(object sender, EventArgs e)
        {
            timer1.Interval = Int32.Parse(tbTimer.Text);
            if(timer1.Enabled)
            {
                timer1.Stop();
                timerSendTelegram.Stop();
                btStartTimer.Text = "Start";
            }
            else
            {
                timer1.Start();
                timerSendTelegram.Start();
                btStartTimer.Text = "Stop";
            }
        }

        private async void frmMonitor_Load(object sender, EventArgs e)
        {
            sFileName = Properties.Settings.Default.FileName;
            if(System.IO.File.Exists(sFileName))
            {
                dgvPoolList.Enabled = true;
                try
                {
                    listPool = Utils.ReadFromXmlFile<List<clsPoolBase>>(sFileName);
                }
                catch { }
                if ((listPool!=null)&&(listPool.Count > 0))
                {
                    bsListPool = new BindingSource { DataSource = listPool };
                    dgvPoolList.DataSource = bsListPool;
                    timer1.Start();
                    timerSendTelegram.Start();
                    btStartTimer.Text = "Stop";
                }
            }
            try
            {
                var authorized = await teleApi.IsUserAuthorized();
                if (!authorized)
                {
                    btLoginTelegram.Enabled = true;
                }
                else
                {
                    btLoginTelegram.Enabled = false;
                }
            }
            catch { }
            //teleApi.PhoneNumber = Properties.Settings.Default.TelegramID;

            //await teleApi.AuthUser();
        }

        private async void btLoginTelegram_Click(object sender, EventArgs e)
        {
            frmLoginTelegram f = new frmLoginTelegram();
            f.teleApi = this.teleApi;
            f.ShowDialog();
            var authorized = await teleApi.IsUserAuthorized();
            if (!authorized)
            {
                btLoginTelegram.Enabled = true;
            }
            else
            {
                btLoginTelegram.Enabled = false;
            }
        }

        private async void timerSendTelegram_Tick(object sender, EventArgs e)
        {
            if (listWaitingSendMessage.Count > 0)
            {
                TelegramMessage msg = listWaitingSendMessage[0];
                await teleApi.SendMessage(msg.TelegramID, msg.Message);
                listWaitingSendMessage.Remove(msg);
            }
        }
    }
}
