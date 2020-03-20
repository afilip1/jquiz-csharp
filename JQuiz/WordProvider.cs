using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace JQuiz
{
    public interface IWordProvider
    {
        List<Word> GetWords(params string[] tags);
        List<Word> GetWordsWithKanji(params string[] tags);
    }

    public class SQLiteWordProvider : IWordProvider
    {
        public SQLiteWordProvider(string dbfile)
        {
            connection = new SQLiteConnection("URI=file:" + dbfile);
            connection.Open();
        }

        public List<Word> GetWords(params string[] tags)
        {
            using var selectCommand = new SQLiteCommand(connection)
            {
                CommandText = "SELECT kanji, readings, meanings, tags FROM words"
            };

            if (tags.Count() != 0) {
                selectCommand.CommandText += $" WHERE tags like '%{tags.First()}%'";

                foreach (var tag in tags.Skip(1)) {
                    selectCommand.CommandText += $" OR tags like '%{tag}%'";
                }
            }

            using var reader = selectCommand.ExecuteReader();

            var words = new List<Word>();
            while (reader.Read())
            {
                var word = new Word(reader.GetString(0), reader.GetString(1).Split('/'), reader.GetString(2), reader.GetString(3));
                words.Add(word);
            }
            return words;
        }

        public List<Word> GetWordsWithKanji(params string[] tags)
        {
            using var selectCommand = new SQLiteCommand(connection)
            {
                CommandText = "SELECT kanji, readings, meanings, tags FROM words WHERE kanji IS NOT NULL"
            };

            if (tags.Count() != 0) {
                selectCommand.CommandText += $" AND (tags like '%{tags.First()}%'";

                foreach (var tag in tags.Skip(1)) {
                    selectCommand.CommandText += $" OR tags like '%{tag}%'";
                }
                selectCommand.CommandText += ")";
            }

            using var reader = selectCommand.ExecuteReader();

            var words = new List<Word>();
            while (reader.Read())
            {
                var word = new Word(reader.GetString(0), reader.GetString(1).Split('/'), reader.GetString(2), reader.GetString(3));
                words.Add(word);
            }
            return words;
        }

        readonly SQLiteConnection connection;
    }
}
