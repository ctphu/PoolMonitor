using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolLibrary
{
    public class clsPoolBase
    {
        public String PoolName { get; set; }
        public String Address { get; set; }
        public String WorkerName { get; set; }
        public String Email { get; set; }
        public String TelegramID { get; set; }
        public String LastMessage { get; set; }
        public int Round { get; set; }
        public List<clsWorkerCurrent> WorkerList;
        public clsPoolBase()
        {
            WorkerList = new List<clsWorkerCurrent>();
            Round = 72;
        }
        public void NiceHashCurrentSpeed()
        {
            
            try
            {
                String url;
                url = @"https://api.nicehash.com/api?method=stats.provider.workers&addr=" + Address;
                using (var webClient = new System.Net.WebClient())
                {
                    var json = webClient.DownloadString(url);
                    //clsResultWorkerP result = JsonConvert.DeserializeObject<clsResultWorkerP>(json);
                    //MessageBox.Show(result.method);
                    WorkerList.Clear();
                    clsResultWorker result = JsonConvert.DeserializeObject<clsResultWorker>(json);
                    for (int i = 0; i < result.result.workers.Length; i++)
                    {
                        if (result.result.workers[i][0].ToString() == WorkerName)
                        {
                            clsWorkerSpeed wsCurrentSpeed = JsonConvert.DeserializeObject<clsWorkerSpeed>(result.result.workers[i][1].ToString());
                            if (wsCurrentSpeed.a > 0)
                            {
                                clsWorkerCurrent wc = new clsWorkerCurrent();
                                wc.Address = Address;
                                wc.WorkerName = result.result.workers[i][0].ToString();
                                wc.Speed = wsCurrentSpeed.a;
                                wc.TimeConnect = Int32.Parse(result.result.workers[i][2].ToString());
                                wc.Difficulty = Double.Parse(result.result.workers[i][4].ToString());
                                wc.Algo = Int32.Parse(result.result.workers[i][6].ToString());
                                wc.AlgoName = Utils.AlgoToString(wc.Algo);
                                WorkerList.Add(wc);
                            }

                        }
                    }

                }
            }
            catch { }
            if (WorkerList.Count == 0)
            {
                LastMessage = String.Format("Time: {0}, Address: {1}, Worker: {2} Fail!. Check your rig.", DateTime.Now, Address, WorkerName);
            }
            else
            {
                LastMessage = "";
                foreach (clsWorkerCurrent wc in WorkerList)
                {
                    LastMessage = LastMessage + wc.ToString();
                }
            }
            //return 0;
        }
        public void CheckAndEmailStatus(bool bSendEmail = false)
        {
            try
            {
                if (WorkerList.Count == 0)
                {
                    Utils.SendEmail(Email, WorkerName + " Fail !", LastMessage);
                }
                else
                {
                    if (bSendEmail)
                    {
                        Utils.SendEmail(Email, WorkerName + " Status", LastMessage);
                    }
                }
            }
            catch { }
        }
        public async void CheckAndSendTelegram(TelegramApi pTele,bool bSendStatus = false)
        {
            try
            {
                if (WorkerList.Count == 0)
                {
                    await pTele.SendMessage(TelegramID, LastMessage);
                }
                else
                {
                    if (bSendStatus)
                    {
                        await pTele.SendMessage(TelegramID, LastMessage);
                    }
                }
            }
            catch { }
        }
        public TelegramMessage GetTelegramMessage(bool bSendStatus)
        {
            TelegramMessage msg = null;
            try
            {
                if (WorkerList.Count == 0)
                {
                    msg = new TelegramMessage(TelegramID, LastMessage);
                }
                else
                {
                    if (bSendStatus)
                    {
                        msg = new TelegramMessage(TelegramID, LastMessage);
                    }
                }
            }
            catch { }
            return msg;
        }
    }

    public class clsWorkerCurrent
    {
        public string Address { get; set; }
        public string WorkerName { set; get; }
        public int Algo { get; set; }
        public string AlgoName { get; set; }
        public double Speed { get; set; }
        public int TimeConnect { get; set; }
        public double Difficulty { get; set;}
        public int Location { get; set; }
        public override String ToString()
        {
            String sReturn;
            sReturn = String.Format("Time: {0}, Address: {1} Worker: {2} Algo: {3} Speed: {4} TimeConnect: {5} min.\n",DateTime.Now, Address, WorkerName, AlgoName, Speed, TimeConnect);
            return sReturn;
        }
    }
    public class clsWorker
    {
        public string addr { get; set; }
        public Object[][] workers { get; set; }
        public int algo { get; set; }
        //public clsWorkerSpeed speed { get; set; }

    }
    //{"result":{"addr":"3GrXGdR1fR7duAKN9HwaFPbjFudSxVF3wn","workers":[["worker1",{"a":"125.27"},19,1,"0.5",2,20]],"algo":20},"method":"stats.provider.workers"}
    public class clsResultWorker
    {
        public clsWorker result { get; set; }
        public string method { get; set; }
    }
    public class clsWorkerSpeed
    {
        public double a { get; set; }
    }
}
