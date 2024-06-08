namespace Wheatly.Extensions
{
    public static class IEnumerableExtensions
    {
        // Source: https://stackoverflow.com/a/648240
        public static T RandomElement<T>(this IEnumerable<T> source, Random rng)
        {
            T? current = default;
            int count = 0;
            foreach(T element in source)
            {
                count++;
                if(rng.Next(count) == 0)
                {
                    current = element;
                }
            }
            if(count == 0 || current is null)
            {
                throw new InvalidOperationException("Sequence was empty");
            }
            return current;
        }
    }
}
