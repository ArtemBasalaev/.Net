using System;
using Microsoft.Data.SqlClient;

namespace Transaction
{
    public class Program
    {
        public static void Main()
        {
            using var connection = new SqlConnection("Data Source=.;Initial Catalog=Shop;Integrated Security=true;TrustServerCertificate=True");

            connection.Open();

            var transaction = connection.BeginTransaction();

            try
            {
                var sqlCommand1 = "INSERT INTO dbo.categories(name) " +
                                        "VALUES (N'электрика')";
                var commandInTransaction = new SqlCommand(sqlCommand1, connection);
                commandInTransaction.ExecuteNonQuery();

                throw new ArgumentException();

                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
            }

            var sqlCommand2 = "INSERT INTO dbo.categories(name) " +
                                    "VALUES (N'ремонт')";
            var command = new SqlCommand(sqlCommand2, connection);
            command.ExecuteNonQuery();

            throw new ArgumentException();
        }
    }
}