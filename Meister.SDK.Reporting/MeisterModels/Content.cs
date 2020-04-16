using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
namespace Meister.SDK.Reporting.MeisterModel
{
    public static class Content
    {
        /// <summary>
        /// Builds a datatable from the content json
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static DataTable ToEdmDataTable(string json)
        {
            IEnumerable<List<NameValuePair>> contents = ToNameValuePairs(json);
            return CreateDataTable<List<NameValuePair>>(contents);
        }
        internal static IEnumerable<List<NameValuePair>> ToNameValuePairs(string json)
        {
            List<List<NameValuePair>> pairs = new List<List<NameValuePair>>();
            ContentHeader header = JsonConvert.DeserializeObject<ContentHeader>(json);
            foreach (var contents in header.contents)
            {
                List<NameValuePair> list = new List<NameValuePair>();
                foreach (var item in contents)
                    list.Add(new NameValuePair(item.Key, item.Value));
                pairs.Add(list);
            }
            return pairs;
        }
        /// <summary>
        /// Transforms each item of list into a datarow containing the nvp ... 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        internal static DataTable CreateDataTable<T>(IEnumerable<T> list)
        {
            DataTable dataTable = new DataTable();
            var first = list.FirstOrDefault();
            foreach (var col in ToIEnumerator<NameValuePair>(first))
                dataTable.Columns.Add(new DataColumn(col.Name, Nullable.GetUnderlyingType(col.Name.GetType()) ?? col.Name.GetType()));
            foreach (var line in list)
            {
                IEnumerable<NameValuePair> nvps = line as IEnumerable<NameValuePair>;
                List<object> vals = new List<object>();
                foreach (var nvp in nvps)
                    vals.Add(nvp.Value);
                dataTable.Rows.Add(vals.ToArray());
            }
            return dataTable;
        }
        internal static IEnumerable<T> ToIEnumerator<T>(dynamic source)
        {
            if (source is IEnumerable<T>)
                return source as IEnumerable<T>;
            else
                throw new ApplicationException("Cannot iterate on the passed dynamic");
        }
    }
    internal class ContentHeader
    {
        [JsonProperty("CONTENT")]
        public IEnumerable<IDictionary<string, string>> contents { get; set; }
    }
}
