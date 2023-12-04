using DocumentFormat.OpenXml.Drawing;
using Productivity.Shared.Models.Utility;
using Productivity.Shared.Utility.Constants;
using Productivity.Shared.Utility.Exceptions;
using System.Linq.Dynamic.Core;
using System.Linq.Dynamic.Core.Exceptions;

namespace Productivity.Shared.Utility.ModelHelpers
{
    public static class DataSpecificationQueryBuilder
    {

        public static int GetQueryCount(
                QuerySupporter specificaion,
                IQueryable inputQuery)
        {
            try
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
            catch (Exception ex)
            {
                if (ex is ParseException || ex is InvalidOperationException 
                    || ex is FormatException)
                    throw new QueryException(ContextConstants.ParseError, ex);
                else
                    throw;
            }
        }

        public static IQueryable<T> GetQuery<T>(
        int top, int skip,
        IQueryable<T> inputQuery)
        {
            inputQuery = inputQuery.Skip(skip);
            if (top >= 1)
            {
                inputQuery = inputQuery.Take(top);
            }
            return inputQuery;
        }

        public static IQueryable<T> GetQuery<T>(
                QuerySupporter specificaion,
                IQueryable<T> inputQuery)
        {
            try 
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
                inputQuery = inputQuery.Skip(specificaion.Skip);
                if (specificaion.Top >= 1)
                {
                    inputQuery = inputQuery.Take(specificaion.Top);
                }
                return inputQuery;
            }
            catch (Exception ex)
            {
                if (ex is ParseException || ex is InvalidOperationException
                    || ex is FormatException)
                    throw new QueryException(ContextConstants.ParseError, ex);
                else
                    throw;
            }
        }
    }
}
