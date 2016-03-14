using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace AtlasReportToolkit
{
    public class Extensions
    {
        public static T BinaryClone<T>(T originalObject)
        {
            using (var stream = new System.IO.MemoryStream())
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, originalObject);
                stream.Position = 0;
                return (T)binaryFormatter.Deserialize(stream);
            }
        }

        public static void Save<T>(T item,String fileName)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(fileName,false))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    serializer.Serialize(writer, item);
                    writer.Close();
                }
            }
            catch (System.Exception exc)
            {
            }
        }

        public static T Load<T>(String fileName) where T : class
        {
            try
            {
                if (!File.Exists(fileName))
                    return null;

                FileStream file = new FileStream(fileName,FileMode.Open,FileAccess.Read,FileShare.Read);
                using (XmlReader reader = XmlReader.Create(file))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));

                    XmlDeserializationEvents errors = new XmlDeserializationEvents();
                    errors.OnUnknownAttribute += delegate (object sender, XmlAttributeEventArgs e)  { };
                    errors.OnUnknownElement += delegate (object sender, XmlElementEventArgs e)  { };
                    errors.OnUnknownNode += delegate (object sender, XmlNodeEventArgs e)  { };
                    errors.OnUnreferencedObject += delegate (object sender, UnreferencedObjectEventArgs e)  { };

                    T obj = (T)serializer.Deserialize(reader,errors);
                    reader.Close();
                    file.Close();

                    return obj;
                }
            }
            catch (System.Exception exc)
            {
            }
            return null;
        }
    }
}
