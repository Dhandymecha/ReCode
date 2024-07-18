using System.Data;
using System.Data.SQLite;
using WpfApp.Define;
using WpfApp.Model;

namespace WpfApp.DB
{
    internal class SQDatabase
    {
        private static readonly Lazy<SQDatabase> lazy = new Lazy<SQDatabase>(() => new SQDatabase());

        public static SQDatabase Instance { get { return lazy.Value; } }

        private SQLiteConnection Connection = null;

        private static string PATH   = @"..\..\..\..\..\Data\DVR.db";
        private static string DB_SOURCE = $"Data Source={PATH};Version=3;";

        private static readonly string TABLE = "rec";

        private static readonly int COL_ID      = 0;
        private static readonly int COL_NAME    = 1;
        private static readonly int COL_START   = 2;
        private static readonly int COL_END     = 3;
        private static readonly int COL_DATA1   = 4;


        /*
        CREATE TABLE "rec" (
	        "ID"	INTEGER NOT NULL UNIQUE,
	        "Name"	TEXT,
	        "Start"	TEXT,
	        "End"	TEXT,
	        "Data"	TEXT,
	        PRIMARY KEY("ID" AUTOINCREMENT)
        );
        */
        public SQDatabase() {


        }

        public List<RecordEntry> Select(DateTime begin, DateTime end)
        {
            List<RecordEntry> records = new List<RecordEntry>();

            var beginDate = begin.ToString(DEF.DATETIME_FORMAT);
            var endDate = end.AddDays(1).ToString(DEF.DATETIME_FORMAT);

            string sql = $"SELECT * FROM {TABLE} WHERE End BETWEEN '{beginDate}' AND '{endDate}'";

            using (SQLiteConnection con = new SQLiteConnection(DB_SOURCE)) {

                con.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(sql, con)) {
                    using (SQLiteDataReader reader = cmd.ExecuteReader()) {
                        while (reader.Read()) {

                            var r = new RecordEntry()
                            {
                                ID = reader.GetInt64(COL_ID),
                                Name = reader.IsDBNull(COL_NAME) ? string.Empty : reader.GetString(COL_NAME),
                                Start = reader.IsDBNull(COL_START) ? new DateTime() : reader.GetDateTime(COL_START),
                                End = reader.IsDBNull(COL_END) ? new DateTime() : reader.GetDateTime(COL_END),
                            };
                            records.Add(r);
                        }
                    }
                }

                con.Close();
            }

            return records;
        }
        public void Insert(RecordEntry record)
        {
            var start = record.Start.ToString(DEF.DATETIME_FORMAT);
            var end = record.End.ToString(DEF.DATETIME_FORMAT);

            string sql = $"INSERT INTO {TABLE} (Name, Start, End) " +
                $"VALUES ('{record.Name}', '{start}', '{end}')";

            using (SQLiteConnection con = new SQLiteConnection(DB_SOURCE)) {
                con.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(sql, con)) {
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
        }

    }
}
