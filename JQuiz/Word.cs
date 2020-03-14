using System.Collections.Generic;
using System.Linq;

namespace JQuiz
{
    class Word
    {
        public Word(string? kanji, string[] readings, string meanings, string tags)
        {
            this.Kanji = kanji;
            this.Readings = readings.ToList();
            this.Meanings = meanings;
            this.Tags = tags;
        }

        public override string ToString()
        {
            return $"{Kanji} {string.Join(',', Readings.Select(r => $"【{r}】"))} ({Tags})\n" +
            $"    {Meanings}";
        }

        public string? Kanji { get; private set; }
        public List<string> Readings { get; private set; }
        public string Meanings { get; private set; }
        public string Tags { get; private set; }
    }
}