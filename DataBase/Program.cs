using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace DataBase
{
    public class Program
    {
        public static void Main()
        {
            using var connection = new SqlConnection("Data Source=.;Initial Catalog=Shop;Integrated Security=true;TrustServerCertificate=True");

            connection.Open();

            var sqlCommand1 = "SELECT COUNT(*) FROM dbo.products";
            var command = new SqlCommand(sqlCommand1, connection);
            var productsCount = command.ExecuteScalar();

            Console.WriteLine($"В таблице products хранится {productsCount} товаров");

            var sqlCommand2 = "INSERT INTO dbo.products(name, price, categoryId) " +
                                    "VALUES ('наушники HD660', 27550.00, 1)";
            command = new SqlCommand(sqlCommand2, connection);
            command.ExecuteNonQuery();

            var sqlCommand3 = "INSERT INTO dbo.categories(name) " +
                                    "VALUES ('автотовары')";
            command = new SqlCommand(sqlCommand3, connection);
            command.ExecuteNonQuery();

            var sqlCommand4 = "UPDATE dbo.products " +
                                    "SET name = 'газонокосилка HN-1/02', price = 1720.00 " +
                                    "WHERE id = 1";
            command = new SqlCommand(sqlCommand4, connection);
            command.ExecuteNonQuery();

            var sqlCommand5 = "DELETE FROM dbo.products " +
                                    "WHERE price < 100";
            command = new SqlCommand(sqlCommand5, connection);
            command.ExecuteNonQuery();

            var sqlCommand6 = "SELECT p.name, p.price, c.name " +
                                    "FROM dbo.products p " +
                                    "INNER JOIN dbo.categories c " +
                                    "ON p.categoryId = c.id";
            command = new SqlCommand(sqlCommand6, connection);
            var reader = command.ExecuteReader();

            Console.WriteLine("Перечень товаров:");

            while (reader.Read())
            {
                Console.WriteLine("{0,-25} {1,8} {2,-25}", reader[0], reader[1], reader[2]);
            }

            reader.Close();

            var adapter = new SqlDataAdapter(sqlCommand6, connection);

            var dataSet = new DataSet();
            adapter.Fill(dataSet);
            var table = dataSet.Tables[0];

            Console.WriteLine("Перечень товаров:");

            foreach (DataRow row in table.Rows)
            {
                var cells = row.ItemArray;

                foreach (var cell in cells)
                {
                    Console.Write("{0,-25}", cell);
                }

                Console.WriteLine();
            }
        }
    }
}