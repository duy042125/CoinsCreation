using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using Dapper;
using System.Data;
using Middleware.Database_Component;

namespace Middleware
{
    public class DBConnection
    {
        #region Private Components

        private static DBConnection currentConnection;

        private DBConnection()
        {
            
        }

        public static DBConnection openConnection()
        {
            if (currentConnection == null)
            {
                try
                {
                    currentConnection = new DBConnection();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            return currentConnection;
        }

        private static string getConnectionString(string name)               
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

        #endregion

        #region Account Connections

        public static void createAccount(string username, string password, string name)
        {
            try
            {
                using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(getConnectionString("CreativeCoinConnection")))
                {
                    Account newAccount = new Account(username, password, name);
                    connection.Execute("dbo.SP_Account_Insert @username, @password, @name", newAccount);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion
    }
}
