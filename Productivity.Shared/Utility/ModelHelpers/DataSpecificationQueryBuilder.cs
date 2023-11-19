using DocumentFormat.OpenXml.Drawing;
using Productivity.Shared.Models.Utility;
using Productivity.Shared.Utility.Exceptions;
using System.Linq.Dynamic.Core;

namespace Productivity.Shared.Utility.ModelHelpers
{
    public static class DataSpecificationQueryBuilder
    {

        public static int GetQueryCount(
                QuerySupporter specificaion,
                IQueryable inputQuery)
        {
            if (!string.IsNullOrEmpty(specificaion.Filter))
            {
                if (specificaion.FilterParams != null)
                {
                    inputQuery = inputQuery.Where(specificaion.Filter, specificaion.FilterParams);
                }
                else
                {
                    inputQuery = inputQuery.Where(specificaion.Filter);
                }
            }
            if (!string.IsNullOrEmpty(specificaion.OrderBy))
            {
                inputQuery = inputQuery.OrderBy(specificaion.OrderBy);
            }
            return inputQuery.Count();
        }

        public static IQueryable<T> GetQuery<T>(
                QuerySupporter specificaion,
                IQueryable<T> inputQuery)
        {
            if (!string.IsNullOrEmpty(specificaion.Filter))
            {
                if (specificaion.FilterParams != null)
                {
                    inputQuery = inputQuery.Where(specificaion.Filter, specificaion.FilterParams);
                }
                else
                {
                    inputQuery = inputQuery.Where(specificaion.Filter);
                }
            }
            if (!string.IsNullOrEmpty(specificaion.OrderBy))
            {
                inputQuery = inputQuery.OrderBy(specificaion.OrderBy);
            }
            if (specificaion.Top >= 1)
            {
                inputQuery.Take(specificaion.Top);
            }
            inputQuery.Skip(specificaion.Skip);
            return inputQuery;
        }
    }
}
