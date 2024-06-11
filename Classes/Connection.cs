using System.Data.SqlClient;

namespace PR42.Classes
{
    public class Connection
    {
        private static readonly string config = "" +
            "Trusted_Connection=No;" +
            "DataBase=ShopContent;" +
            "User=;" +
            "PWD=";
        public static SqlConnection OpenConnection()
        {
            SqlConnection connection = new SqlConnection(config);
            connection.Open();
            return connection;
        }
        public static SqlDataReader Query(string sql, out SqlConnection connection)
        {
            connection = OpenConnection();
            return new SqlCommand(sql, connection).ExecuteReader();
        }
        public static void CloseConnection(SqlConnection connection)
        {
            connection.Close();
            SqlConnection.ClearPool(connection);
        }
    }
}
