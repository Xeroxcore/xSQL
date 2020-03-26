using System;
using System.Data;
using MySql.Data.MySqlClient;
using xSql.Interface;

namespace xSql
{
    public class MySql : ISqlHelper
    {
        private MySqlConnection Connection { get; }
        public MySql(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new Exception("your connection string cant be null, empty or with whitespace");
            Connection = new MySqlConnection(connectionString);
        }

        private void ParametersIsValid(string query, object data)
        {
            if (string.IsNullOrWhiteSpace(query))
                throw new Exception("your query cant be null, empty or with whitespace");

            if (data == null)
                throw new Exception("The data you provided is null");
        }

        private MySqlCommand CreateSqlCommand<T>(string query, T data)
        {
            var command = new MySqlCommand(query, Connection);
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

        public DataTable ReadDataFromDatabase(MySqlCommand sqlCommand)
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
                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
            }
        }
    }
}