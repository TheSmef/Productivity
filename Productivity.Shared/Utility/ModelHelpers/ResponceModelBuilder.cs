using DocumentFormat.OpenXml.Spreadsheet;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using Productivity.Shared.Models.DTO.GetModels.CollectionModels;
using Productivity.Shared.Models.DTO.GetModels.StatsModels.SingleModels;
using Productivity.Shared.Models.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Productivity.Shared.Utility.ModelHelpers
{
    public static class ResponceModelBuilder
    {
        public static async Task<Result<CollectionDTO<T>>> Build<T>(int top, int skip, IQueryable<T> items,
            CancellationToken cancellationToken)
        {
            CollectionDTO<T> responce = new();
            responce.ElementsCount = items.Count();
            responce.Collection = await DataSpecificationQueryBuilder.GetQuery(top, skip, items)
                .ToListAsync(cancellationToken);
            responce.TotalPages = top == 0 ? 0 : PageCounter.CountPages(responce.ElementsCount, top);
            responce.CurrentPageIndex = top == 0 ? 0 : PageCounter.CountCurrentPage(
                responce.TotalPages,
                responce.ElementsCount,
                skip,
                top
            );
            return responce;
        }


        public static async Task<Result<CollectionDTO<T>>> Build<T>(QuerySupporter specification, IQueryable<T> items,
            CancellationToken cancellationToken)
        {
            CollectionDTO<T> responce = new();
            var countresult = DataSpecificationQueryBuilder.GetQueryCount(specification, items);
            if (countresult.IsFaulted)
            {
                Exception exception = default!;
                countresult.IfFail(ex => exception = ex);
                return new Result<CollectionDTO<T>>(exception);
            }
            countresult.IfSucc(succ => responce.ElementsCount = succ);
            var dataresult = DataSpecificationQueryBuilder.GetQuery(specification, items);
            if (dataresult.IsFaulted)
            {
                Exception exception = default!;
                dataresult.IfFail(ex => exception = ex);
                return new Result<CollectionDTO<T>>(exception);
            }
            IQueryable<T> collection = Enumerable.Empty<T>().AsQueryable();
            dataresult.IfSucc(succ => collection = succ);
            responce.Collection = await collection
                .ToListAsync(cancellationToken);
            responce.TotalPages = specification.Top == 0 ? 0 : PageCounter.CountPages(responce.ElementsCount, specification.Top);
            responce.CurrentPageIndex = specification.Top == 0 ? 0 : PageCounter.CountCurrentPage(
                responce.TotalPages,
                responce.ElementsCount,
                specification.Skip,
                specification.Top
                );
            return responce;
        }
    }
}
