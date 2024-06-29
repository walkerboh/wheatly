namespace Wheatly.GameRandoms
{
    public static class GameRandomFactory
    {
        private static readonly Dictionary<string, Type> Mappings = new (StringComparer.CurrentCultureIgnoreCase)
        {
            {"Ascension", typeof(Ascension) }
        };

        public static IGameRandom? GetGameRandom(string game)
        {
            var type = Mappings.GetValueOrDefault(game);

            if(type is null)
            {
                return null;
            }

            return Activator.CreateInstance(type) as IGameRandom;
        }
    }
}
