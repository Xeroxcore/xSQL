using System;
using System.Data;
using xSql.Interface;
using Npgsql;

namespace xSql
{
    public class NpgSql : ISqlHelper
    {
        private NpgsqlConnection Connection { get; }
        public NpgSql(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new Exception("your connection string cant be null, empty or with whitespace");
            Connection = new NpgsqlConnection(connectionString);
        }

        private void ParametersIsValid(string query, object data)
        {
            if (string.IsNullOrWhiteSpace(query))
                throw new Exception("your query cant be null, empty or with whitespace");

            if (data == null)
                throw new Exception("The data you provided is null");
        }

        private NpgsqlCommand CreateSqlCommand<T>(string query, T data)
        {
            var command = new NpgsqlCommand(query, Connection);
            var properties = data.GetType().GetProperties();
            foreach (var item in properties)
            {
                if (item != null)
                    if (query.Contains(item.Name))
                        command.Parameters.AddWithValue(item.Name, item.GetValue(data));
            }
            return command;
        }

        public void AlterDataQuery<T>(string query, T data)
        {
            ParametersIsValid(query, data);
            try
            {
                var command = CreateSqlCommand(query, data);
                if (Connection.State != ConnectionState.Open)
                    Connection.Open();
                command.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {

                Connection.Close();
            }
        }

        public object AlterDataQueryScalar<T>(string query, T data)
        {
            ParametersIsValid(query, data);
            try
            {
                var command = CreateSqlCommand(query, data);
                if (Connection.State != ConnectionState.Open)
                    Connection.Open();
                var returningData = command.ExecuteScalar();
                return returningData;
            }
            catch
            {
                throw;
            }
            finally
            {
                Connection.Close();
            }
        }

        public DataTable ReadDataFromDatabase(NpgsqlCommand sqlCommand)
        {
            var dataTable = new DataTable();
            using (var reader = sqlCommand.ExecuteReader())
            {
                dataTable.Load(reader);
            }
            return dataTable;
        }

        public DataTable SelectQuery<T>(string query, T data)
        {
            ParametersIsValid(query, data);
            try
            {
                var command = CreateSqlCommand(query, data);
                if (Connection.State != ConnectionState.Open)
                    Connection.Open();
                return ReadDataFromDatabase(command);
            }
            catch
            {
                throw;
            }
            finally
            {
                    Connection.Close();
            }
        }
    }
}
