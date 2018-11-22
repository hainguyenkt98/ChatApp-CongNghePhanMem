using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatApp.Common
{
    public static class CommonClearCache
    {
        public static void ClearCacheItems(HttpResponseBase Response)
        {
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
        }
    }
}