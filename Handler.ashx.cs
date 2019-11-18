using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RSSCaching
{
    /// <summary>
    /// Summary description for Handler
    /// </summary>
    public class Handler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/xml";

            RSSCacher rssCacher = new RSSCacher();
            string feed = rssCacher.GetFeed(@"https://seekingalpha.com/market_currents.xml");

            context.Response.Write(feed);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}