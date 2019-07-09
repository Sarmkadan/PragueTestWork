using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace PragueTestWorkApi.Funct
{
    public class Bitly
    {
        private readonly string login = "o_1n8n4kk1rf";
        private readonly string apikey = "R_07f89ffea7b4476eb4d0d56ac072778a";
        public string Shorten(string longUrl)
        {
            var url = string.Format("http://api.bit.ly/shorten?format=json&version=2.0.1&login={1}&apiKey={2}&longUrl={0}", HttpUtility.UrlEncode(longUrl), login, apikey);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    dynamic jsonResponse = js.Deserialize<dynamic>(reader.ReadToEnd());
                    string s = jsonResponse["results"][longUrl]["shortUrl"];
                    return s;
                }
            }
            catch (WebException ex)
            {
                WebResponse errorResponse = ex.Response;
                using (Stream responseStream = errorResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                    String errorText = reader.ReadToEnd();
                   
                }

                throw;
            }
            catch
            {
                return "Wrong URL";
            }
        }

        public string Expand(string shortUrl)
        {

            try
            {


            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(shortUrl);
            req.AllowAutoRedirect = true;
            HttpWebResponse res = (HttpWebResponse)req.GetResponse();
            ServicePoint sp = req.ServicePoint;
            return sp.Address.ToString();
            }
            catch (Exception e)
            {
                return "can't expand";
            }
        }
    }
}