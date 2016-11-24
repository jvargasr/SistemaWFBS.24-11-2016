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
    public class XML
    {
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
