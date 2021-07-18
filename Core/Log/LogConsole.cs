using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Log
{

    public class LogConsole : ILog
    {
        private object semafor;

        public LogConsole()
        {
            this.semafor = new object();
            this.Mode = LogMessageType.Debug;
        }


        /// <summary>
        /// Indica el nivell a partir del qual es guarda la informació.
        /// </summary>
        public LogMessageType Mode { get; set; }



        /// <summary>
        /// Escriu un registre al log.
        /// </summary>
        public void Write(LogMessageType typeMessage, string title, string message)
        {
            if ((int)typeMessage < (int)this.Mode)
                return;

            lock (this.semafor)
            {
                DateTime now = DateTime.Now;

                string day = now.Day < 10 ? "0" + now.Day.ToString() : now.Day.ToString();
                string month = now.Month < 10 ? "0" + now.Month.ToString() : now.Month.ToString();
                string year = now.Year.ToString();

                string hour = now.Hour < 10 ? "0" + now.Hour.ToString() : now.Hour.ToString();
                string minute = now.Minute < 10 ? "0" + now.Minute.ToString() : now.Minute.ToString();
                string second = now.Second < 10 ? "0" + now.Second.ToString() : now.Second.ToString();

                string strTypeMessage = Fill(typeMessage.ToString(), ' ', 8);

                switch (typeMessage)
                {
                    case LogMessageType.Debug:
                        strTypeMessage = "[Debug]";
                        break;
                    case LogMessageType.Information:
                        strTypeMessage = "[Info]";
                        break;
                    case LogMessageType.Error:
                        strTypeMessage = "[Error]";
                        break;
                }

                string strDataHora = string.Format("{0}/{1}/{2} {3}:{4}:{5}", day, month, year, hour, minute, second);

                string registre = string.Format("{0} {1} {2} {3}", strDataHora, strTypeMessage, title ?? "", message ?? "");

                Console.WriteLine(registre);
            }
        }

        private static string Fill(string value, char filler, int lenght)
        {
            string result = value ?? "";

            if (!string.IsNullOrEmpty(value))
            {
                if (value.Length < lenght)
                {
                    string strFiller = filler.ToString();

                    for (int i = value.Length; i < lenght; i++)
                        result = result + strFiller;
                }
                else
                    result = value.Substring(0, lenght);
            }

            return (result);
        }
    }
}
