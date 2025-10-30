using Microsoft.Data.SqlClient;

namespace mini_supermarket.DB
{
    internal static class DbConnectionFactory
    {
        // TODO: Move to configuration if needed.
        // private const string ConnectionString = "Data Source=.\\sqlexpress;Initial Catalog=mini_sp;Integrated Security=True;Encrypt=False";
        private const string ConnectionString = "Data Source=.\\sqlexpress;Initial Catalog=mini_sp;Integrated Security=True;Encrypt=False";

        internal static SqlConnection CreateConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}
