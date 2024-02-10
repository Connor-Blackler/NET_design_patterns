namespace Extensions
{
    public static class EnumerableExtensions
    {
        public static void IterateWithIndex<T>(this IEnumerable<T> thisEnum, Action<T, int> action)
        {
            var i = 0;
            foreach (var thisValue in thisEnum)
            {
                action(thisValue, i);
                i++;
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> mytStrings = new();
            mytStrings.Add("test123");
            mytStrings.Add("ttttt");
            mytStrings.Add("ddddd");
            mytStrings.IterateWithIndex((myString, index) =>
            {
                Console.WriteLine($"{myString} + index = {index})");
            });
        }
    }
}
