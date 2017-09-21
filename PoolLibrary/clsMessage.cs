using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolLibrary
{
    public class TelegramMessage
    {
        public int IndexInList { get; set; }
        public string TelegramID { get; set; }
        public string Message { get; set; }
        public TelegramMessage()
        {

        }
        public TelegramMessage(string pTelegramID, string pMessage)
        {
            TelegramID = pTelegramID;
            Message = pMessage;
        }
    }
}
