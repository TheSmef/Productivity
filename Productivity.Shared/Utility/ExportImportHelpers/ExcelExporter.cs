﻿using ClosedXML.Attributes;
using ClosedXML.Excel;
using Productivity.Shared.Utility.Constants;
using Productivity.Shared.Utility.Exceptions;
using System.Reflection;

namespace Productivity.Shared.Utility.ExportImportHelpers
{
    public static class ExcelExporter
    {
        public static byte[] GetExcelReport<T>(List<T> values, string worksheetname)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                XLWorkbook wb = new XLWorkbook();
                wb.AddWorksheet(worksheetname).FirstCell().InsertTable(values).SetShowTotalsRow(true);
                wb.Worksheet(worksheetname).Cells().Style.Alignment.WrapText = true;
                wb.Worksheet(worksheetname).Columns().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                wb.Worksheet(worksheetname).Columns().Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                wb.Worksheet(worksheetname).Columns().Width = 50;
                wb.SaveAs(memoryStream);
                return memoryStream.ToArray();
            }
        }

        public static List<T> GetImportModel<T>(byte[] array, string worksheetname)
        {
            try
            {
                List<T> items = new List<T>();
                Type type = typeof(T);
                var properties = type.GetProperties();
                using (var ms = new MemoryStream(array))
                {
                    using (XLWorkbook wb = new XLWorkbook(ms))
                    {
                        var worksheet = wb.Worksheet(worksheetname);
                        var columns = worksheet.FirstRow().Cells().Select((v, i) => new { Value = v.Value, Index = i + 1 });
                        foreach (var row in worksheet.RowsUsed().Skip(1))
                        {
                            T obj = (T)Activator.CreateInstance(type)!;

                            foreach (var prop in properties)
                            {
                                if (!prop.GetCustomAttribute<XLColumnAttribute>()!.Ignore)
                                {
                                    int colIndex = columns
                                        .Single(x => x.Value.ToString() == prop.GetCustomAttribute<XLColumnAttribute>()!.Header).Index;
                                    var value = row.Cell(colIndex).Value;
                                    var proptype = prop.PropertyType;
                                    try
                                    {
                                        prop.SetValue(obj, Convert.ChangeType(value.ToString(), proptype));
                                    }
                                    catch { }
                                }
                            }
                            items.Add(obj);
                        }
                    }
                }
                return items;
            }
            catch(Exception ex)
            {
                if (ex is FileFormatException || ex is NotImplementedException)
                    throw new DataException(ContextConstants.ParseErrorFile, ex);
                else
                    throw;
            }

        }
    }
}
