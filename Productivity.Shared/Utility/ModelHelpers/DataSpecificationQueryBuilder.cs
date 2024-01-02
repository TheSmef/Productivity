using DocumentFormat.OpenXml.Drawing;
using LanguageExt.Common;
using Productivity.Shared.Models.Utility;
using Productivity.Shared.Utility.Constants;
using Productivity.Shared.Utility.Exceptions;
using System.Linq.Dynamic.Core;
using System.Linq.Dynamic.Core.Exceptions;

namespace Productivity.Shared.Utility.ModelHelpers
{
    public static class DataSpecificationQueryBuilder
    {

        public static Result<int> GetQueryCount(
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
                    return new Result<int>(new QueryException(ContextConstants.ParseError, ex));
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

        public static Result<IQueryable<T>> GetQuery<T>(
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
                return new Result<IQueryable<T>>(inputQuery);
            }
            catch (Exception ex)
            {
                if (ex is ParseException || ex is InvalidOperationException
                    || ex is FormatException)
                    return new Result<IQueryable<T>>(new QueryException(ContextConstants.ParseError, ex));
                else
                    throw;
            }
        }
    }
}
