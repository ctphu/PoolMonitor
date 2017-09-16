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
        public List<clsWorkerCurrent> WorkerList;
        public clsPoolBase()
        {
            WorkerList = new List<clsWorkerCurrent>();
        }
        public void NiceHashCurrentSpeed()
        {
            WorkerList.Clear();
            String url;
            url = @"https://api.nicehash.com/api?method=stats.provider.workers&addr=" + Address;
            using (var webClient = new System.Net.WebClient())
            {
                var json = webClient.DownloadString(url);
                //clsResultWorkerP result = JsonConvert.DeserializeObject<clsResultWorkerP>(json);
                //MessageBox.Show(result.method);

                clsResultWorker result = JsonConvert.DeserializeObject<clsResultWorker>(json);
                for (int i = 0; i < result.result.workers.Length; i++)
                {
                    if (result.result.workers[i][0].ToString() == WorkerName)
                    {
                        clsWorkerSpeed wsCurrentSpeed = JsonConvert.DeserializeObject<clsWorkerSpeed>(result.result.workers[i][1].ToString());
                        if (wsCurrentSpeed.a > 0)
                        {
                            clsWorkerCurrent wc = new clsWorkerCurrent();
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
            //return 0;
        }
        public void CheckAndEmailStatus(bool bSendEmail = false)
        {
            if(WorkerList.Count == 0)
            {
                Utils.SendEmail(Email, WorkerName + " Fail !", "Check your rig");
            }
            else
            {
                if (bSendEmail)
                {
                    String sBody = "";
                    foreach(clsWorkerCurrent wc in WorkerList)
                    {
                        sBody = sBody + wc.ToString();
                    }
                    Utils.SendEmail(Email, WorkerName + " Status", sBody);
                }
            }
        }
    }

    public class clsWorkerCurrent
    {
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
            sReturn = String.Format("Worker: {0} Algo: {1} Speed: {2} TimeConnect: {3} min.\n", WorkerName, AlgoName, Speed, TimeConnect);
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
