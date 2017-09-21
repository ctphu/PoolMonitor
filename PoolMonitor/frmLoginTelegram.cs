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
    public partial class frmLoginTelegram : Form
    {
        public TelegramApi teleApi;
        public frmLoginTelegram()
        {
            InitializeComponent();
        }

        private async void btSendCode_Click(object sender, EventArgs e)
        {
            if(tbTelegram.Text.Equals(teleApi.PhoneNumber))
            {
                teleApi.PhoneNumber = tbTelegram.Text;
                Properties.Settings.Default.TelegramID = tbTelegram.Text;
                Properties.Settings.Default.Save();
            }
            await teleApi.AuthUser();
            MessageBox.Show("Telegram Code da duoc gui ve dien thoai. xin kiem tra tin nhan.");
        }

        private async void btLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbCode.Text))
            {
                MessageBox.Show("Nhap vao Telegram Code truoc khi login. Neu chua co Code thi nhan nut GETCODE");
                return;
            }
            await teleApi.MakeAuthAsync(tbCode.Text);
            var authorized = await teleApi.IsUserAuthorized();
            if(authorized)
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("Error Code");
            }
        }

        private void frmLoginTelegram_Load(object sender, EventArgs e)
        {
            tbTelegram.Text = Properties.Settings.Default.TelegramID;
            teleApi.PhoneNumber = Properties.Settings.Default.TelegramID;
        }
    }
}
