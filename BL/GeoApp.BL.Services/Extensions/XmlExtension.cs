using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace GeoApp.BL.Services.Extensions
{
    public static class XmlExtension
    {
        public static string Serialize<T>(this T value)
        {
            if (value == null) return string.Empty;

            var xmlSerializer = new XmlSerializer(typeof(T));

            using (var stringWriter = new StringWriter())
            {
                using (var xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings { Indent = true }))
                {
                    xmlSerializer.Serialize(xmlWriter, value);
                    return stringWriter.ToString();
                }
            }
        }

        public static byte[] SerializeToByteArray<T>(this T value)
        {
            if (value == null)
            {
                return new byte[0];
            }

            var xmlSerializer = new XmlSerializer(typeof(T));

            using (var stream = new MemoryStream())
            {
                xmlSerializer.Serialize(stream, value);
                return stream.ToArray();
            }
        }
    }
}
