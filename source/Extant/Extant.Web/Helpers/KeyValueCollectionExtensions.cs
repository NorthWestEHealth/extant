using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace Extant.Web.Helpers
{
    public static class KeyValueCollectionExtensions
    {
        public static void Merge(this KeyValueConfigurationCollection collection, string key, string value)
        {
            if (collection.AllKeys.Contains(key)) collection[key].Value = value;
            else collection.Add(key, value);
        }
    }
}