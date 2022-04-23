using System;
using System.IO;
using System.Xml.Serialization;

namespace ConsoleFileManager.Serialization
{
    public class XmlSerialize : ISerialize
    {
        private static XmlSerialize serializer;
        private string _name = "config.xml";
        SerializeModel _model;

        private XmlSerialize() { }

        public static XmlSerialize GetInstance()
        {
            if (serializer == null)
                serializer = new XmlSerialize();
            return serializer;
        }

        public SerializeModel Deserialize()
        {
            if (File.Exists(_name))
            {
                try
                {
                    string xmlText = File.ReadAllText(_name);
                    StringReader reader = new StringReader(xmlText);
                    XmlSerializer xml = new XmlSerializer(typeof(SerializeModel));
                    _model = (SerializeModel)xml.Deserialize(reader);
                    if (_model == null)
                    {
                        return new SerializeModel();
                    }
                    return _model;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Произошла ошибка при десериализации: {ex.Message}\n" +
                        $"Для продолжения нажмите любую клавишу.");
                    Console.ReadKey(true);
                    return new SerializeModel();
                }
            }
            return new SerializeModel();
        }

        public void Serialize(SerializeModel model)
        {
            StringWriter writer = new StringWriter();
            XmlSerializer serializer = new XmlSerializer(typeof(SerializeModel));
            serializer.Serialize(writer, model);
            string xml = writer.ToString();
            File.WriteAllText(_name, xml);
        }
    }
}
