using DocumentFormat.OpenXml.Spreadsheet;

namespace Productivity.Shared.Utility.ModelHelpers
{
    public static class PageCounter
    {
        public static int CountPages(int count, int top)
        {
            decimal a = count / top;
            if (count % top > 0)
            {
                a++;
            }
            a = Math.Ceiling(a);
            return Convert.ToInt32(a);
        }

        public static int CountCurrentPage(int total,
            int elementCount, int skip, int top)
        {
            int elementsAfterSkip = elementCount - skip;
            if (elementsAfterSkip < 0)
            {
                elementsAfterSkip = 0;
            }
            return total + 1 - CountPages(elementsAfterSkip, top);
        }
    }
}
