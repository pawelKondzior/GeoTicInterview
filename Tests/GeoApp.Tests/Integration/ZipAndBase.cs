using Flurl;
using Flurl.Http;
using GeoApp.BL.Contracts.DTO.Integration.Here;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Xml.Serialization;
using Xunit;
using static System.Net.Mime.MediaTypeNames;

namespace GeoApp.Tests.Integration
{
    public class ZipAndBase
    { 
        public string ZipAndEncode = 
            @"UEsDBBQAAAAIANmztEQSwaeZzwAAAM8BAAAQAAAAc2FtcGxlLXRyYWNlLmdweIXPTQuCMBwG8HufQnZv%2F605S0k9djEIungdZjpSJ27kPn6%2BRBgYXcYYv2cPzzG2deU8805L1YSIYoLiaHMsWvv9uBlYowOrZYhKY9oAoO973DOsugJ2hFBIz8k1K%2FNabGWjjWiy%2FJ36ShjVqqITd2lxpmo4XVKgMP6vZaCneKIyYabivzHnr4BhCbb6hoZRpnvMp86L%2BdIapxImRJxiSuh%2Bj5xq7CWY%2Bcz1EaypA10qxlfVjvOl8rxVxfzDQrk%2FFCfLRs7YpOCzA%2BZd49LoBVBLAQIUABQAAAAIANmztEQSwaeZzwAAAM8BAAAQAAAAAAAAAAEAIAAAAAAAAABzYW1wbGUtdHJhY2UuZ3B4UEsFBgAAAAABAAEAPgAAAP0AAAAAAA%3D%3D";

        [Fact]
        public void TestRest()
        {
            var xmlFile = "GeoApp.Tests.Data.GPSTrace.xml";

            var assembly = typeof(ZipAndBase).Assembly;
            Stream resource = assembly.GetManifestResourceStream(xmlFile);
            var bytes = ReadFully(resource);

            var client = new RestClient("http://rme.api.here.com/2/matchroute.json");
            var request = new RestRequest("?app_id", Method.POST);

            request.AddHeader("Content-Type", "application/gpx+xml");

            request.AddParameter("application/gpx+xml", bytes, ParameterType.RequestBody);
       

            IRestResponse response = client.Execute(request);
            var content = response.Content; // raw content as string
        }

        [Fact]
        public void LoadTestXml()
        {
            var xmlFile = "GeoApp.Tests.Data.GPSTrace.xml";

            var assembly = typeof(ZipAndBase).Assembly;
            Stream resource = assembly.GetManifestResourceStream(xmlFile);

            var bytes = ReadFully(resource);
            var compresed = Compress(bytes);
            var base64 = base64_encode(compresed);
        }
        public static byte[] ReadFully(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }


        public static byte[] Compress(byte[] data)
        {
            using (var compressedStream = new MemoryStream())
            using (var zipStream = new GZipStream(compressedStream, CompressionMode.Compress))
            {
                zipStream.Write(data, 0, data.Length);
                zipStream.Close();
                return compressedStream.ToArray();
            }
        }

        public string base64_encode(byte[] data)
        {
            if (data == null)
                throw new ArgumentNullException("data");
            return Convert.ToBase64String(data);
        }



    }
}
