using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Utility_Library
{
    public static class DBConnUtil
    {
        public static SqlConnection GetConnection(string connectionString)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            return conn;
        }
    }
}
