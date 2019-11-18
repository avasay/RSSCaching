using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace RSSCaching
{
    public class RSSData
    {
        public string Key { get; set; }
        public string XMLData { get; set; }
        public RSSData(string key, string data)
        {
            Key = key;
            XMLData = data;
        }

        /// <summary>
        /// UpdateCache calls the HttpRuntime Cache method to insert data into 
        /// the application cache, provides the policy for expiration, and 
        /// the associated callback when it expires.
        /// </summary>
        /// <param name="sender"></param>
        public void UpdateCache(RSSCacher sender)
        {
            try
            {

                HttpRuntime.Cache.Insert(Key,
                    this,
                    null,
                    DateTime.Now.AddSeconds(60),
                    System.Web.Caching.Cache.NoSlidingExpiration,
                    CacheItemPriority.Default,
                    new CacheItemRemovedCallback(sender.RemovedCallback));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error inserting feed data into cache. " + ex.Message);
            }
        }

    }
}