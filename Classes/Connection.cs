using System.Data.SqlClient;

namespace PR42.Classes
{
    public class Connection
    {
        private static readonly string config = "server=HOME-PC\\MYSERVER;" +
            "DataBase=ShopContent;" +
            "User=sa;" +
            "PWD=sa";
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
