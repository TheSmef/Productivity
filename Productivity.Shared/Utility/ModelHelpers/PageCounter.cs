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
    }
}
