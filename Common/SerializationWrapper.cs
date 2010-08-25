using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace StateMagic.Common
{
    public static class SerializationWrapper
    {
        
        public static void Serialize<T>(string filename, T objectModel)
        {
            if (filename == null) throw new ArgumentNullException("filename");

            XmlSerializer serializer = new XmlSerializer(typeof(T));
            FileStream outputFile = new FileStream(filename, FileMode.Create);
            serializer.Serialize(outputFile, objectModel);
            outputFile.Close();
        }
        
 
        public static string Serialize<T>(T objectModel)
        {
            StringWriter sw = new StringWriter();
            XmlWriter xmlWriter = XmlWriter.Create(sw);

            XmlSerializer serializer = new XmlSerializer(typeof(T));
            serializer.Serialize(xmlWriter, objectModel);
            
            return sw.ToString();
        }
        


        public static string Serialize(object objectModel, Type type)
        {
            if (type == null) throw new ArgumentNullException("type");

            StringWriter sw = new StringWriter();

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = true;
            XmlWriter xmlWriter = XmlWriter.Create(sw, settings);

            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            XmlSerializer serializer = new XmlSerializer(type);
            serializer.Serialize(xmlWriter, objectModel, ns);

            return sw.ToString();
        }

        public static T DeserializeString<T>(string value) where T : class
        {
            if (value == null) return null;
            
            StringReader sr = new StringReader(value);
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            return serializer.Deserialize(sr) as T;
        }


        public static T DeserializeFile<T>(string filename) where T : class
        {
            if (filename == null) throw new ArgumentNullException("filename");

            XmlSerializer serializer = new XmlSerializer(typeof(T));
            FileStream outputFile = new FileStream(filename, FileMode.Open,FileAccess.Read);
            T objectModel = serializer.Deserialize(outputFile) as T;
            outputFile.Close();
            return objectModel;
        }

    }
}
