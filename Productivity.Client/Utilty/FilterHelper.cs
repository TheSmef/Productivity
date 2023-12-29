using Productivity.Client.Models;
using Productivity.Shared.Models.DTO.GetModels.SignleEntityModels;
using Productivity.Shared.Models.Utility;
using System.Collections.ObjectModel;

namespace Productivity.Client.Utilty
{
    public static class FilterHelper
    {
        public static void SetFilterQuery(QuerySupporter query, List<string> regions, List<string> cultures,
            RangeModel productivityRange, RangeModel yearRange)
        {
            query.Filter = string.Empty;
            List<string> parameters = [];
            if (regions != null)
            {
                if (regions.Count > 0)
                {
                    query.Filter += "(";
                }
                for (int i = 0; i < regions.Count; i++)
                {
                    if (i != 0)
                        query.Filter += " OR ";
                    parameters.Add(regions[i]);
                    query.Filter += $"Region = @{parameters.Count - 1}";
                }
                if (query.Filter != string.Empty)
                {
                    query.Filter += ")";
                }
            }


            if (cultures != null)
            {
                if (query.Filter != string.Empty &&
                    cultures.Count > 0)
                {
                    query.Filter += " AND ";
                }
                if (cultures.Count > 0)
                {
                    query.Filter += "(";
                }
                for (int i = 0; i < cultures.Count; i++)
                {
                    if (i != 0)
                        query.Filter += " OR ";
                    parameters.Add(cultures[i]);
                    query.Filter += $"Culture = @{parameters.Count - 1}";
                }
                if (query.Filter != string.Empty && cultures.Count > 0)
                {
                    query.Filter += ")";
                }
            }


            if (query.Filter != string.Empty)
            {
                query.Filter += " AND ";
            }
            parameters.Add(yearRange.Min.ToString());
            parameters.Add(yearRange.Max.ToString());
            query.Filter += $"Year >= @{parameters.Count - 2} AND Year <= @{parameters.Count - 1}";
            if (productivityRange.Min != 0)
            {
                parameters.Add(productivityRange.Min.ToString());
                query.Filter += $" AND ProductivityValue >= @{parameters.Count - 1}";
            }

            if (productivityRange.Max != 0)
            {
                parameters.Add(productivityRange.Max.ToString());
                query.Filter += $" AND ProductivityValue <= @{parameters.Count - 1}";
            }


            if (parameters.Count > 0)
            {
                query.FilterParams = [.. parameters];
            }
        }


        public static void SetFilterQuery(QuerySupporter query, string filter)
        {
            query.Filter = filter != string.Empty ? $"Name.Contains(@0)" : string.Empty;
            query.FilterParams = filter != string.Empty ? [filter] : null;
        }
    }
}
