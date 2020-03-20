using System.Collections.Generic;

namespace JQuiz
{
    public class KanjiQuiz
    {
        readonly List<Word> wordList;

        public KanjiQuiz()
        {
            wordList = new SQLiteWordProvider("data.db").GetWordsWithKanji();
        }

        public Word GetRandomWord()
        {
            return wordList.Random();
        }
    }
}
