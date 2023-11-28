using DocumentFormat.OpenXml.Spreadsheet;
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
        public static async Task<CollectionDTO<T>> Build<T>(int top, int skip, IQueryable<T> items,
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


        public static async Task<CollectionDTO<T>> Build<T>(QuerySupporter specification, IQueryable<T> items,
            CancellationToken cancellationToken)
        {
            CollectionDTO<T> responce = new();
            responce.ElementsCount = DataSpecificationQueryBuilder.GetQueryCount(specification, items);
            responce.Collection = await DataSpecificationQueryBuilder.GetQuery(specification, items)
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
