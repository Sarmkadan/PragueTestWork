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
            string urifromget = HttpUtility.ParseQueryString(uri.Query).Get("url");
            if (paramMethod == "short")
            {
                
               
             
                string returnuri = bityly.Shorten(urifromget);
                if (returnuri == "Wrong URL")
                {
                    dblog.ItemDbSet.Add(new UrlLog.LogUrl()
                    {
                        Datetimeofrequest = DateTime.UtcNow, NewUrl = returnuri, OldUrl = urifromget,
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
