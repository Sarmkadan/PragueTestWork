using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using PragueTestWorkApi.Funct;
using PragueTestWorkApi.Models;

namespace PragueTestWorkApi.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public string Get()
        {
            Uri uri = Request.RequestUri;
            Bitly bityly = new Bitly();
            UrlLog.UrlLogdbContext dblog = new UrlLog.UrlLogdbContext();
            string paramMethod = HttpUtility.ParseQueryString(uri.Query).Get("method");
            if (paramMethod==null)
            {
                return "error: no param method";
            }


            string querryvalue =  uri.Query;

            if (!querryvalue.Contains("&url="))
            {
                return "error: no param url";
            }

            if (querryvalue.Contains("&method="+paramMethod))
            {
                querryvalue = querryvalue.Replace("&method=" + paramMethod, "").Replace("url=","");
            }
            else
            {
                querryvalue = querryvalue.Replace("?method=" + paramMethod+"&url=", "");
            }

            string urifromget = querryvalue;//HttpUtility.ParseQueryString(uri.Query).Get("url");

           

            
            
            if (paramMethod == "short")
            {
                
                string returnUri = bityly.Shorten(HttpUtility.HtmlDecode(urifromget));
                if (returnUri == "Wrong URL")
                {
                    dblog.ItemDbSet.Add(new UrlLog.LogUrl()
                    {
                        Datetimeofrequest = DateTime.UtcNow, NewUrl = returnUri, OldUrl = urifromget,
                        Result = returnUri
                    });
                    dblog.SaveChanges();
                    return returnUri;
                }
                else
                {
                    dblog.ItemDbSet.Add(new UrlLog.LogUrl()
                    {
                        Datetimeofrequest = DateTime.UtcNow,
                        NewUrl = returnUri,
                        OldUrl = urifromget,
                        Result = "Success"
                    });
                    dblog.SaveChanges();
                    return returnUri;
                }
            }
            else if (paramMethod == "expand")
            {
                string returnuri = bityly.Expand(urifromget);
                if (returnuri == "can't expand")
                {
                    dblog.ItemDbSet.Add(new UrlLog.LogUrl()
                    {
                        Datetimeofrequest = DateTime.UtcNow,
                        NewUrl = returnuri,
                        OldUrl = urifromget,
                        Result = returnuri
                    });
                    dblog.SaveChanges();
                    return returnuri;
                }
                else
                {
                    dblog.ItemDbSet.Add(new UrlLog.LogUrl()
                    {
                        Datetimeofrequest = DateTime.UtcNow,
                        NewUrl = returnuri,
                        OldUrl = urifromget,
                        Result = "Success"
                    });
                    dblog.SaveChanges();
                    return returnuri;
                }

            }
            else
            {
                return "unsupported method";
            }


            // return new string[] { "value1", "value2" };
        }

        // GET api/values/5
       /* public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }*/
    }
}
