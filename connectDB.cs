using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace healthcare
{
    public class connectDB
    {
        SqlConnection con = null;
        public connectDB()
        {
            con = new SqlConnection("server=localhost;uid=sa;pwd=123456;database=healthcare");
        }
        public SqlConnection getConn()
        {
            return con;
        }
    }
}
