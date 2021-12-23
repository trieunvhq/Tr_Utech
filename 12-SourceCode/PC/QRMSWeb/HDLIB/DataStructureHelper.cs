using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace WinFormAPI
{
    public class DataStructureHelper
    {
        public static void SaveObjectToXmlFile(string filePath, Type objectType, object obj)
        {
            // Create an instance of the XmlSerializer class;
            // specify the type of object to serialize.
            XmlSerializer serializer = new XmlSerializer(objectType);
            TextWriter writer = new StreamWriter(filePath);
            serializer.Serialize(writer, obj);
            writer.Close();
            writer.Dispose();
        }

        //public static void SaveObjectToXmlFileStream(string filePath, Type objectType, object obj)
        //{
        //    // Create an instance of the XmlSerializer class;
        //    // specify the type of object to serialize.
        //    XmlSerializer serializer =
        //    new XmlSerializer(objectType);
        //    FileStream fs = new FileStream();
        //    TextWriter writer = new StreamWriter(filePath);
        //    serializer.Serialize(writer, obj);
        //    writer.Close();
        //    writer.Dispose();
        //}

        public static object LoadObjectFromXmlFile(string filePath, Type objectType)
        {
            // Create an instance of the XmlSerializer specifying type and namespace.
            XmlSerializer serializer = new XmlSerializer(objectType);

            // A FileStream is needed to read the XML document.
            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            XmlReader reader = XmlReader.Create(fs);

            // Declare an object variable of the type to be deserialized.
            object obj;

            // Use the Deserialize method to restore the object's state.
            obj = serializer.Deserialize(reader);
            fs.Close();
            fs.Dispose();
            reader.Dispose();

            return obj;
        }

    }
}
