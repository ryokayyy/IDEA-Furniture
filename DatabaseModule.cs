using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;

public class DatabaseModule : IDisposable
{
    private SqliteConnection Connection_ { get; }

    public DatabaseModule(string dbpath)
    {
        try
        {
            Connection_ = new SqliteConnection("Data Source=" + dbpath);
            Connection_.Open();
        }
        catch (Exception ex) { }
    }

    public IList<string> GetRecords(string commandText)
    {
        List<string> records = new();

        if (Connection_ != null && Connection_.State.HasFlag(ConnectionState.Open))
        {
            var command = new SqliteCommand(commandText, Connection_);

            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    string record = String.Empty;
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        record += reader.GetValue(i).ToString() + ",";
                    }
                    if (!String.IsNullOrEmpty(record))
                        records.Add(record.TrimEnd(','));
                }
            }

            command.Dispose();
        }

        return (records);
    }

    public void Dispose()
    {
        Connection_?.Dispose();
    }
}
