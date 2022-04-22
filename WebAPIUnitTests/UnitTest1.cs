using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Net;

namespace WebAPIUnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestWebAPI()
        {
           

                string xml_response = null;
                var requestString = "testString";
                var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:64189/api/");
                httpWebRequest.ContentType = "application/json; charset=utf-8";
                httpWebRequest.Method = "POST";
                httpWebRequest.Accept = "application/json; charset=utf-8";
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {

                    streamWriter.Write(requestString);
                    streamWriter.Close();
                }
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    xml_response = result.ToString();
                }
            }
        }
    }
