using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
namespace DB_2020_CS_104
{
    class Configuration
    {
        String ConnectionStr = @"Data Source = DESKTOP-FHJVRDL; Initial Catalog = ProjectB; Integrated Security = True;MultipleActiveResultSets=true";
        SqlConnection con;
        private static Configuration _instance;
        public static Configuration getInstance()
        {
            if (_instance == null)
                _instance = new Configuration();
            return _instance;
        }
        private Configuration()
        {
            con = new SqlConnection(ConnectionStr);
            con.Open();
        }
        public SqlConnection getConnection()
        {
            return con;
        }
    }
}
