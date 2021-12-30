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

            var totalProductsCountSqlQuery = "SELECT COUNT(*) FROM dbo.products";
            var command = new SqlCommand(totalProductsCountSqlQuery, connection);
            var productsCount = command.ExecuteScalar();

            Console.WriteLine($"В таблице products хранится {productsCount} товаров");

            var productInsertSqlQuery = "INSERT INTO dbo.products(name, price, categoryId) " +
                                              "VALUES (N'наушники HD660', 27550.00, 1)";
            command = new SqlCommand(productInsertSqlQuery, connection);
            command.ExecuteNonQuery();

            var categoryInsertSqlQuery = "INSERT INTO dbo.categories(name) " +
                                               "VALUES (N'автотовары')";
            command = new SqlCommand(categoryInsertSqlQuery, connection);
            command.ExecuteNonQuery();

            Console.WriteLine("Для обновления информации о товаре введите следующие данные: ");

            Console.Write("1. Наименование товара: ");
            var productName = Console.ReadLine();

            Console.Write("2. Цена: ");
            var productPrice = Convert.ToDouble(Console.ReadLine());

            Console.Write("3. Id товара: ");
            var productId = Convert.ToInt32(Console.ReadLine());

            var productUpdateSqlQuery = "UPDATE dbo.products " +
                                              "SET name = @productName, price = @productPrice " +
                                              "WHERE id = @productId";
            command = new SqlCommand(productUpdateSqlQuery, connection);

            command.Parameters.Add(new SqlParameter("@productName", productName) { SqlDbType = SqlDbType.NVarChar });
            command.Parameters.Add(new SqlParameter("@productPrice", productPrice) { SqlDbType = SqlDbType.Decimal });
            command.Parameters.Add(new SqlParameter("@productId", productId) { SqlDbType = SqlDbType.Int });

            command.ExecuteNonQuery();

            var productDeleteSqlQuery = "DELETE FROM dbo.products " +
                                              "WHERE price < 100";
            command = new SqlCommand(productDeleteSqlQuery, connection);
            command.ExecuteNonQuery();

            var productsSelectSqlQuery = "SELECT p.name, p.price, c.name " +
                                              "FROM dbo.products p " +
                                              "INNER JOIN dbo.categories c " +
                                              "ON p.categoryId = c.id";
            command = new SqlCommand(productsSelectSqlQuery, connection);

            using (var reader = command.ExecuteReader())
            {
                Console.WriteLine("Перечень товаров:");

                while (reader.Read())
                {
                    Console.WriteLine("{0,-25} {1,8} {2,-25}", reader[0], reader[1], reader[2]);
                }
            }

            var adapter = new SqlDataAdapter(productsSelectSqlQuery, connection);

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