using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using DataStorage.Mappers;

namespace DataStorage
{
    public class DBHelper
    {
        public static List<T> GetData<T>(IMapper<T> mapper, string queryString, params SqlParameter[] args)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                return GetData(connection, mapper, queryString, args);
            }
        }

        private static List<T> GetData<T>(SqlConnection connection, IMapper<T> mapper, string queryString, params SqlParameter[] args)
        {
            var result = new List<T>();

            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddRange(args);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(mapper.ReadItem(reader));
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return result;
        }

        public static T GetItem<T>(IMapper<T> mapper, string queryString, params SqlParameter[] args)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                return GetItem(connection, mapper, queryString, args);
            }
        }

        private static T GetItem<T>(SqlConnection connection, IMapper<T> mapper, string queryString, params SqlParameter[] args)
        {
            T result = default(T);

            try
            {
                if (connection.State != ConnectionState.Open)
                connection.Open();

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddRange(args);

                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                result = mapper.ReadItem(reader);

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return result;
        }
    }
}
