using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using WFBS.Business.Log;

namespace WFBS.IT.Communication
{
    /// <summary>
    /// Clase XML encargada de transformar a texto plano.
    /// </summary>
    public class XML
    {
        /// <summary>
        /// Serializa un objeto al tipo asignado.
        /// </summary>
        /// <typeparam name="T">Variable Generica</typeparam>
        /// <param name="type">Recibe un parametro del tipo de la Variable asginada</param>
        /// <returns>Devuelve un objeto del tipo de la variable asignada serializado</returns>
        public string Serializar<T>(T type)
        {
            try
            {
                XmlSerializer ser = new XmlSerializer(typeof(T));
                StringWriter write = new StringWriter();
                ser.Serialize(write, type);
                write.Close();

                return write.ToString();
            }
            catch (Exception ex)
            {
                Logger.log("No se pudo Serializar: " + ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Deserializa un objeto al tipo asignado.
        /// </summary>
        /// <typeparam name="T">Variable Generica</typeparam>
        /// <param name="xml">Recibe un parametro xml el cual trae un objeto serilizado</param>
        /// <returns>Retorna un objeto o variable acorde al tipo asignado</returns>
        public T Deserializar<T>(string xml)
        {
            try
            {
                XmlSerializer ser = new XmlSerializer(typeof(T));
                StringReader reader = new StringReader(xml);
                return (T)ser.Deserialize(reader);
            }
            catch (Exception ex)
            {

                Logger.log("No se pudo Deserializar: " + ex.ToString());
                return default(T);
            }
        }
    }
}
