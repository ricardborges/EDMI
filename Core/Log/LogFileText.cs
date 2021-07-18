using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Core.Log
{
    public class LogFileText : ILog
    {

        private string directoryPath;
        private object semafor;


        /// <summary>
        /// Crea una instancia de Log. La ruta on es guardaran els logs será el directori d'execució en un directori anomenat Log.
        /// </summary>
        public LogFileText()
            : this(AppDomain.CurrentDomain.BaseDirectory + "Log\\")
        {

        }


        /// <summary>
        /// Crea una instància de Log.
        /// </summary>
        public LogFileText(string directoryPath)
        {
            this.Mode = LogMessageType.Debug;

            this.directoryPath = directoryPath;

            try
            {
                if (!Directory.Exists(this.directoryPath))
                    Directory.CreateDirectory(this.directoryPath);
            }
            catch { }

            this.semafor = new object();
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

                string strTypeMessage = typeMessage.ToString();
                string strDataHora = string.Format("{0}/{1}/{2} {3}:{4}:{5}", day, month, year, hour, minute, second);

                string registre = string.Format("{0} {1} {2} {3}", strTypeMessage, strDataHora, title ?? "", message ?? "") + Environment.NewLine;

                File.AppendAllText(this.GetPathLogFile(now), registre);
            }
        }


        /// <summary>
        /// Retorna el nom del fitxer de log a partir de la ruta del directori i la data i hora passada per paràmetre.
        /// </summary>
        private string GetPathLogFile(DateTime date)
        {
            string day = date.Day < 10 ? "0" + date.Day.ToString() : date.Day.ToString();
            string month = date.Month < 10 ? "0" + date.Month.ToString() : date.Month.ToString();
            string year = date.Year.ToString();

            string result = this.directoryPath + '\\' + day + month + year + ".log";

            return (result);
        }

        
    }
}
