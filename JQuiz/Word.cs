using System.Collections.Generic;
using System.Linq;

namespace JQuiz
{
    public class Word
    {
        public string? Kanji { get; private set; }
        public List<string> Readings { get; private set; }
        public string Meanings { get; private set; }
        public string Tags { get; private set; }

        public Word(string? kanji, IEnumerable<string> readings, string meanings, string tags)
        {
            this.Kanji = kanji;
            this.Readings = readings.ToList();
            this.Meanings = meanings;
            this.Tags = tags;
        }

        public override string ToString()
        {
            var readings = Readings.Select(r => $"【{r}】");
            return $"{Kanji} {string.Join('、', readings)} ({Tags})\n    {Meanings}";
        }
    }
}