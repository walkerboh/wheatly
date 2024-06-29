using Wheatly.Extensions;

namespace Wheatly.GameRandoms
{
    public class Ascension : IGameRandom
    {
        private static readonly List<string> Expansions =
        [
            "ROTF",
            "SOS",
            "IH",
            "ROV",
            "DU",
            "RU",
            "DOC",
            "DS",
            "WOS",
            "GOTE",
            "VOTA",
            "DLV"
        ];

        private static readonly List<string> Promos = [ "1", "2", "3", "4", "5", "6" ];

        public string GetValues(Random random)
        {
            var expansions = Expansions.Shuffle(random).Take(4);
            var promos = Promos.Shuffle(random).Take(2);

            return $"Expansions: {string.Join(' ', expansions)} | Promos: {string.Join(' ', promos)}";
        }
    }
}
