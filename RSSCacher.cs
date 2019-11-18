using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Net.Http;
using System.Web.Caching;

namespace RSSCaching
{
    public class RSSCacher
    {
        public CacheItemRemovedReason Reason { get; set; }


        public RSSCacher()
        {
        }

        public string GetFeed(string key)
        {
            RSSData rssData = HttpRuntime.Cache.Get(key) as RSSData;
            if (rssData != null) 
            {                               
                return rssData.XMLData;
            }
            else              
            {
                using (HttpClient client = new HttpClient())
                {
                    
                    var uri = new Uri(key);
                    HttpResponseMessage content = client.GetAsync(uri).Result;
                    string xmlData = content.Content.ReadAsStringAsync().Result;

                    rssData = new RSSData(key, xmlData);
                    rssData.UpdateCache(this);

                    return rssData.XMLData;
                }
            }
        }


        public void RemovedCallback(String k, Object v, CacheItemRemovedReason r)
        {
            Reason = r; // Using Reason for unit testing
            var rss = v as RSSData;
            if (r != CacheItemRemovedReason.Expired) // If reason is other than "Expired" do nothing.
            {
                return;
            }


                GetFeed(k);
            
        }
    }
}