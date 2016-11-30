using System;
using System.IO;

namespace WFBS.Business.Log
{
    /// <summary>
    /// Clase Logger, encargada de los registros de sucesos importantes en el sistema.
    /// </summary>
    public class Logger
    {
        /** 
         * Log file path 
         *  
         * @var string 
         */
        private static string __log_file_path = @"c:\logfile_wfbs.txt";

        /// <summary>
        /// Propiedad estatica, recibe la ruta del archivo de log generado, y devuelve la ruta en caso de ser valida.
        /// </summary>
        public static string filePath
        {
            get { return Logger.__log_file_path; }
            set { if (value.Length > 0) Logger.__log_file_path = value; }
        }

        /// <summary>
        /// Genera un escrito de los sucesos gatillados.
        /// </summary>
        public static void flush()
        {
            File.WriteAllText(Logger.filePath, string.Empty);
        }

        /// <summary>
        /// Metodo estatico que escribe en texto plano los sucesos gatillados en la ruta especificada.
        /// </summary>
        /// <param name="msg">Recibe un parametro msg que contiene el mensaje gatillado en un suceso especifico</param>

        public static void log(string msg)
        {
            if (msg.Length > 0)
            {
                using (StreamWriter sw = File.AppendText(Logger.filePath))
                {
                    sw.WriteLine("{0} {1}: {2}", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), msg);
                    sw.Flush();
                }
            }
        }
    }
}
