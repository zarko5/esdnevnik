using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;


namespace esdnevnik {
    class Connection {
        static public SqlConnection Connect() {
            string CS;
            CS = ConfigurationManager.ConnectionStrings["domacinstvo"].ConnectionString;
            return new SqlConnection(CS);
        }
    }
}
