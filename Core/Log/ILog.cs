using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Log
{
    public interface ILog
    {

        /// <summary>
        /// Indica el nivell a partir del qual es guarda la informació.
        /// </summary>
        LogMessageType Mode { get; set; }

        /// <summary>
        /// Escriu un registre al log.
        /// </summary>
        void Write(LogMessageType typeMessage, string title, string message);
    }
}
