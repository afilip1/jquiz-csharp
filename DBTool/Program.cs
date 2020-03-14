using System.Data.SQLite;
using System.IO;
using System.Linq;

namespace DBTool
{
    class Program
    {
        static void Main(string[] files)
        {
            using var connection = new SQLiteConnection("URI=file:data.db");
            connection.Open();

            using var transaction = connection.BeginTransaction();
            using (var setupCommand = new SQLiteCommand(connection))
            {
                setupCommand.CommandText = "DROP TABLE IF EXISTS words;"
                + "CREATE TABLE words (kanji TEXT, readings TEXT NOT NULL, meanings TEXT NOT NULL, tags TEXT NOT NULL, PRIMARY KEY (kanji, readings));";
                setupCommand.ExecuteNonQuery();
            }

            using var insertCommand = new SQLiteCommand(connection);
            insertCommand.CommandText = "INSERT INTO words(kanji, readings, meanings, tags) VALUES (@kanji, @readings, @meanings, @tags);";

            using var selectCommand = new SQLiteCommand(connection);
            selectCommand.CommandText = "SELECT kanji, readings, meanings, tags FROM words WHERE kanji = @kanji;";

            using var deleteCommand = new SQLiteCommand(connection);
            deleteCommand.CommandText = "DELETE FROM words WHERE kanji = @kanji";

            foreach (var file in files)
            {
                foreach (var line in File.ReadLines(file))
                {
                    var parts = line.Split('\t');

                    var kanji = parts[0].Length == 0 ? null : parts[0];
                    var readings = parts[1];
                    var meanings = parts[2];
                    var tags = file.Split('.')[0];

                    selectCommand.Parameters.AddWithValue("@kanji", kanji);

                    if (kanji != null)
                    {
                        using (var reader = selectCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                var readingsOld = reader.GetString(1).Split('/');
                                var readingsNew = readings.Split('/');
                                readings = string.Join('/', readingsOld.Union(readingsNew));

                                meanings = $"{meanings}; {reader.GetString(2)}";

                                tags = string.Join(',', reader.GetString(3).Split(',').Union(new string[] {tags}));

                                deleteCommand.Parameters.AddWithValue("@kanji", kanji);
                                deleteCommand.ExecuteNonQuery();
                            }
                        }
                    }

                    insertCommand.Parameters.AddWithValue("@kanji", kanji);
                    insertCommand.Parameters.AddWithValue("@readings", readings);
                    insertCommand.Parameters.AddWithValue("@meanings", meanings);
                    insertCommand.Parameters.AddWithValue("@tags", tags);

                    insertCommand.ExecuteNonQuery();
                }
            }

            transaction.Commit();
        }
    }
}
