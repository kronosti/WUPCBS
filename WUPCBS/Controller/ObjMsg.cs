using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WUPCBS.Models;
using System.Net.Http;
using Newtonsoft.Json;
using WUPCBS.Class;
using System.Net;
using System.IO;

namespace WUPCBS.Controller
{
  public   class ObjMsg
    {
        public async Task<List<Msgjs>> GetData()
        {
            string url = "http://localhost:54426/api/Messages"; // the url of the second project with the web service, this may be running before this app
            List<Msgjs> result;
            try
            {
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                HttpClient client = new HttpClient(clientHandler);
                var response = await client.GetStringAsync(url); // receiving the json string of from the web service
                try
                {
                    result = JsonConvert.DeserializeObject<List<Msgjs>>(response);// converting the json string to a list of Msgjs class
                }
                catch (Exception)
                {
                    // if the json is just one item
                    var singleResult = JsonConvert.DeserializeObject<Msgjs>(response);
                    result = new List<Msgjs>();
                    result.Add(singleResult);
                }
            }
            catch (Exception)
            {
                result = null;
            }
            return result;
        }
        public async Task<Resultado> SaveSendedmesage(MsgDTO T)
        {
               var rst = new Resultado();// declare a object to recive the result of the web service
               var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:54426/api/Messages"); // the url of the second project with the web service, this may be running before this app
            httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
               using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = System.Text.Json.JsonSerializer.Serialize(T);// convertin the class to json string to send to the webservivce
                    streamWriter.Write(json);
                }
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            if (httpResponse.StatusCode == HttpStatusCode.OK) // if the response is ok set the result = 1 and 0 if diferent, to read it in the front end 
            {
                rst = new Resultado(1);
            }
            else
            {
                rst = new Resultado(0);
            }
            return rst; 
        }
    }
}
