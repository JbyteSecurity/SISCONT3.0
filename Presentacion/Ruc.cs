using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace Presentacion
{
    public class Ruc
    {
        public ArrayList GetData(string rucP)
        {
            WebRequest request = WebRequest.Create("https://incared.com/api/apirest");
            request.Method = "POST";
            string postData = "action=getnumero&numero=" + rucP;
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;

            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            WebResponse response = request.GetResponse();
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            using (dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                RUC ruc = JsonConvert.DeserializeObject<RUC>(responseFromServer);
                //Console.WriteLine(responseFromServer);
                ArrayList Data = new ArrayList();
                //Data.Add(ruc.ruc);
                Data.Add(ruc.rs);
                Data.Add(ruc.tvia + " " + ruc.nvia + " " + ruc.numero);
                Data.Add(ruc.estado);
                Data.Add(ruc.condom);

                return Data;
            }

            //response.Close();
        }
    }

    public class RUC
    {
        public string ruc { get; set; }
        public string rs { get; set; }
        public string estado { get; set; }
        public string condom { get; set; }
        public string tvia { get; set; }
        public string nvia { get; set; }
        public string numero { get; set; }
    }

}
