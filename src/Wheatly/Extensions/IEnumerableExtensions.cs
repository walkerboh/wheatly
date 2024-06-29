namespace Wheatly.Extensions
{
    public static class IEnumerableExtensions
    {
        // Source: https://stackoverflow.com/a/648240
        public static T RandomElement<T>(this IEnumerable<T> source, Random rng)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(rng);

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

        // Source: https://stackoverflow.com/a/5807238
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Random random)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(random);

            return source.ShuffleIterator(random);
        }

        private static IEnumerable<T> ShuffleIterator<T>(this IEnumerable<T> source, Random random)
        {
            var buffer = source.ToList();
            for(var i = 0; i < buffer.Count; i++)
            {
                var j = random.Next(i, buffer.Count);
                yield return buffer[j];

                buffer[j] = buffer[i];
            }
        }
    }
}
