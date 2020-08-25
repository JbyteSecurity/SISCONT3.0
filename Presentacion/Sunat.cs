using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tessnet2;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading;
using System.Collections;

namespace Presentacion
{
    class Sunat
    {
        string captcha = "";
        CookieContainer cokkie = new CookieContainer();
        string[] nrosRuc = new string[] { };

        private void genCaptcha()
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.sunat.gob.pe/cl-ti-itmrconsruc/captcha?accion=image");
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
                request.CookieContainer = cokkie;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();

                var image = new Bitmap(responseStream);
                var ocr = new Tesseract();
                ocr.Init(@"C:\Users\Developers\Source\Repos\SISCONT2.0\SISCONT\Presentacion\Content\tessdata", "eng", false);

                var result = ocr.DoOCR(image, Rectangle.Empty);
                foreach (Word word in result)
                {
                    captcha = word.Text;
                }
            }
            catch (Exception Ex)
            {
                Console.Write(Ex);
            }
        }

        public ArrayList buscarRuc(string ruc)
        {
            try
            {
                genCaptcha();

                ArrayList datosProveedor = new ArrayList();

                string myurl = @"http://www.sunat.gob.pe/cl-ti-itmrconsruc/jcrS00Alias?accion=consPorRuc&nroRuc=" + ruc.Trim() + "&codigo=" + captcha.Trim().ToUpper() + "&tipdoc=1";
                HttpWebRequest myWebRequest = (HttpWebRequest)WebRequest.Create(myurl);
                myWebRequest.CookieContainer = cokkie;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;

                HttpWebResponse myhttpWebResponse = (HttpWebResponse)myWebRequest.GetResponse();
                Stream myStream = myhttpWebResponse.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myStream);
                string xDat = ""; int pos = 0;
                while (!myStreamReader.EndOfStream)
                {
                    xDat = myStreamReader.ReadLine();
                    pos++;

                    switch (pos)
                    {
                        
                        case 140: //Razón Social
                            datosProveedor.Add(getDatafromXML(xDat, 40));
                            break;
                        case 144: //Tipo de Contribuyente
                            datosProveedor.Add(getDatafromXML(xDat, 25));
                            break;
                        case 149: // Nombre Comercial
                            datosProveedor.Add(getDatafromXML(xDat, 25));
                            break;
                        case 154: //Fecha de Inscripción
                            datosProveedor.Add(getDatafromXML(xDat, 25));
                            break;
                        case 160: // Estado
                            datosProveedor.Add(getDatafromXML(xDat, 25));
                            break;
                        case 170: //Condicion
                            datosProveedor.Add(getDatafromXML(xDat, 0));
                            break;
                        case 179: // Direccion
                            datosProveedor.Add(getDatafromXML(xDat, 25));
                            break;
                        case 186: //Actividades de Comercio Exterior
                            datosProveedor.Add(getDatafromXML(xDat, 25));
                            break;
                    }

                }
                Thread.Sleep(1000);
                return datosProveedor;
            }
            catch (Exception Ex)
            {
                return null;
            }
        }

        private string getDatafromXML(string lineRead, int len = 0)
        {

            try
            {
                lineRead = lineRead.Trim();
                lineRead = lineRead.Remove(0, len);
                lineRead = lineRead.Replace("</td>", "");
                while (lineRead.Contains("  "))
                {
                    lineRead = lineRead.Replace("  ", " ");
                }
                return lineRead;
            }
            catch (Exception Ex)
            {
                return "NO SE ENCONTRO RESULTADO " + Ex;
            }
        }
    }
}
