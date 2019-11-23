// Michael Ziegler    |     PROJECT: E-DISPLAY/SQLCONNECT     |     FILE: SQLCONNECT.CS    |    LANGUAGE: C#
using System;
using System.Text;
using System.Data.SqlClient;

namespace SqlServerSample
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Build connection string
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "192.168.1.178,1433";
                builder.UserID = "AS";
                builder.Password = "abc123";
                builder.InitialCatalog = "DATA";

                // Connect to SQL
                Console.Write("Connecting to SQL Server ... ");
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    Console.WriteLine("  Connected! ");

                    // Create a sample database
                    Console.Write("Dropping and creating database Diamonds ... ");
                    String sql = "DROP DATABASE IF EXISTS [TestDB]; CREATE DATABASE [TestDB]";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();
                        Console.WriteLine("Complete.");
                    }

                    // Create new table and propegate cells with new data
                    Console.Write("Creating new Diamond Table ...");
                    Console.ReadKey(true);
                    StringBuilder sb = new StringBuilder();
                    sb.Append("USE TestDB; ");
                    sb.Append("CREATE TABLE Diamonds ( ");
                    sb.Append(" idNum INT IDENTITY (1,1) NOT NULL PRIMARY KEY, ");
                    sb.Append(" STONE VARCHAR(20), ");
                    sb.Append(" SHAPE VARCHAR(20), ");
                    sb.Append(" CUT VARCHAR (20), ");
                    sb.Append(" COLOR CHAR(5), ");
                    sb.Append(" CLARITY VARCHAR(8), ");
                    sb.Append(" CARAT DECIMAL(3,2), ");
                    sb.Append(" RETAILPRICE INT, ");
                    sb.Append(" SALEPRICE INT, ");
                    sb.Append("); ");
                    sb.Append("INSERT INTO Diamonds (STONE, SHAPE, CUT, COLOR, CLARITY, CARAT, RETAILPRICE, SALEPRICE) VALUES ");
                    sb.Append("(' DIAMOND',  'ROUND   ', 'EXCELLENT  ', 'D', 'FL  ', 2.02, 99999, 99999),");
                    sb.Append("(' DIAMOND', 'PRINCESS', 'VERY GOOD  ', 'E', 'IF  ', 1.85, 15299, 11499),");
                    sb.Append("(' DIAMOND', 'EMERALD ', 'IDEAL      ', 'F', 'VVS1', 1.74, 14799, 10999),");
                    sb.Append("(' DIAMOND', 'ASSCHERX ', 'ASTOR IDEALX', 'G', 'VVS2', 1.62, 12399, 11999),");
                    sb.Append("(' DIAMOND', 'CUSHION ', 'POOR       ', 'H', 'VS1 ', 1.50, 11899, 10249),");
                    sb.Append("(' DIAMOND', 'MARQUISE', 'FAIR       ', 'I', 'VS2 ', 1.47, 10299, 7799),");
                    sb.Append("(' DIAMOND', 'RADIANT ', 'GOOD       ', 'J', 'SI1 ', 1.30, 10575, 9479),");
                    sb.Append("(' DIAMOND', 'PEAR    ', 'VERY GOOD  ', 'K', 'SI2 ', 1.25, 8999, 7499),");
                    sb.Append("(' DIAMOND', 'HEART   ', 'EXCELLENT  ', 'L', 'I1  ', 1.24, 9999, 8999),");
                    sb.Append("('DIAMOND', 'ROUND   ', 'GOOD       ', 'M', 'I2  ', 1.01, 5299, 3799),");
                    sb.Append("('DIAMOND', 'PRINCESS', 'VERY GOOD  ', 'N', 'I3  ', 0.85, 3299, 2499),");
                    sb.Append("('DIAMOND', 'EMERALD ', 'IDEAL      ', 'O', 'FL  ', 0.74, 2699, 2199),");
                    sb.Append("('DIAMOND', 'ASSCHER ', 'ASTOR IDEAL', 'P', 'IF  ', 0.62, 2399, 1999),");
                    sb.Append("('DIAMOND', 'CUSHION ', 'POOR       ', 'Q', 'VVS1', 0.50, 2099, 1999),");
                    sb.Append("('DIAMOND', 'MARQUISE', 'FAIR       ', 'R', 'VVS2', 0.45, 5299, 3799),");
                    sb.Append("('DIAMOND', 'RADIANT ', 'GOOD       ', 'S', 'VS1 ', 0.30, 1575, 1479),");
                    sb.Append("('DIAMOND', 'PEAR    ', 'VERY GOOD  ', 'T', 'VS2 ', 0.25, 1499, 1399),");
                    sb.Append("('DIAMOND', 'HEART   ', 'EXCELLENT  ', 'U', 'SI1 ', 0.22, 1299, 1179);");
                    sql = sb.ToString();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();
                        Console.WriteLine(" Done.");
                    }

                    // Read Data from server(SQL Query), write to console.
                    Console.WriteLine("Reading data from table, press any key to continue...");
                    Console.ReadKey(true);
                    sql = "SELECT * FROM Diamonds;";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine("{0} {1} {2} {3} {4} {5} {6} {7} {8}", reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetDecimal(6), reader.GetInt32(7), reader.GetInt32(8));
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.WriteLine("All done. Press any key to finish...");
            Console.ReadKey(true);
        }
    }
}