using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Caching;
using System.Data;
using System.ComponentModel;
namespace FastForward.SCMPDA.Services
{
    public static class CacheLayer
    {
        public enum Key
        {
            ProfitCenter,
            PriceDefinition,
            //CompanyItemStatus,
            ChannelDefinition,
            IsSaleValueRoundUp,
            CompanyInfor,
            ChannelParameter
        }

        static readonly ObjectCache Cache = MemoryCache.Default;
        /// <summary>
        /// Retrieve cached item
        /// </summary>
        /// <typeparam name="T">Type of cached item</typeparam>
        /// <param name="key">Name of cached item</param>
        /// <returns>Cached item as type</returns>
        public static T Get<T>(string key) where T : class
        {
            try
            {
                return (T)Cache[key];
            }
            catch
            {
                return null;
            }
        }
        public static bool Get(string key)
        {
            try
            {
                return (bool)Cache[key];
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Insert value into the cache using
        /// appropriate name/value pairs
        /// </summary>
        /// <typeparam name="T">Type of cached item</typeparam>
        /// <param name="objectToCache">Item to be cached</param>
        /// <param name="key">Name of item</param>
        public static void Add<T>(T objectToCache, string key) where T : class
        {
            Cache.Add(key, objectToCache, DateTime.Now.AddDays(30));
        }

        /// <summary>
        /// Insert value into the cache using
        /// appropriate name/value pairs
        /// </summary>
        /// <param name="objectToCache">Item to be cached</param>
        /// <param name="key">Name of item</param>
        public static void Add(object objectToCache, string key)
        {
            Cache.Add(key, objectToCache, DateTime.Now.AddDays(30));
        }
        /// <summary>
        /// Insert value into the cache using
        /// appropriate name/value pairs
        /// </summary>
        /// <param name="objectToCache">Item to be cached</param>
        /// <param name="key">Name of item</param>
        public static void Add(bool objectToCache, string key)
        {
            Cache.Add(key, objectToCache, DateTime.Now.AddDays(30));
        }
        /// <summary>
        /// Remove item from cache
        /// </summary>
        /// <param name="key">Name of cached item</param>
        public static void Clear(string key)
        {
            Cache.Remove(key);
        }

        /// <summary>
        /// Check for item in cache
        /// </summary>
        /// <param name="key">Name of cached item</param>
        /// <returns></returns>
        public static bool Exists(string key)
        {
            return Cache.Get(key) != null;
        }

        /// <summary>
        /// Gets all cached items as a list by their key.
        /// </summary>
        /// <returns></returns>
        public static List<string> GetAll()
        {
            return Cache.Select(keyValuePair => keyValuePair.Key).ToList();
        }


        public static DataTable ToDataTable<T>(this IList<T> data)
        {
            //http://stackoverflow.com/questions/564366/convert-generic-list-enumerable-to-datatable
            PropertyDescriptorCollection properties =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }

    }
}