using Productivity.Shared.Models.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productivity.Shared.Utility.ModelHelpers
{
    public static class QueryMapper
    {
        public static List<KeyValuePair<string, string>> MapToQuery(QuerySupporter query)
        {
            var uriquery = new List<KeyValuePair<string, string>>
                {
                        new ("Filter", query.Filter!),
                        new ( "OrderBy", query.OrderBy!),
                        new ( "Top", query.Top!.ToString()),
                        new ( "Skip", query.Skip!.ToString()),
                };
            if (query.FilterParams != null)
            {
                foreach (string param in query.FilterParams)
                {
                    uriquery.Add(new ("FilterParams", param));
                }
            }
            return uriquery;
        }
    }
}
