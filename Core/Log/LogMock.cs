using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Log
{
    public class LogMock : ILog
    {

        public LogMock()
        {
            this.Mode = LogMessageType.Debug;
        }

        /// <summary>
        /// Indica el nivell a partir del qual es guarda la informació.
        /// </summary>
        public LogMessageType Mode { get; set; }


        public void Write(LogMessageType typeMessage, string title, string message)
        {

        }
    }
}
