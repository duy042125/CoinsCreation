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

        private static SqlConnection connection;
        private static DBConnection currentConnection;
        private DBConnection(string username, string password)
        {
            try
            {
                string connectionString = getConnectionString("CreativeCoinConnection");
                connection = new SqlConnection(connectionString);
                connection.Open();
                
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        public static DBConnection openConnection(string username, string password)
        {
            if (currentConnection == null)
            {
                try
                {
                    currentConnection = new DBConnection(username, password);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            return currentConnection;
        }

        #region Methods

        public static string getConnectionString(string name)               //change into private if needed
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

        public static void closeConnection()
        {
            connection.Close();
            currentConnection = null;
        }

        #endregion

        #region Account

        public Account createAccount(string a)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
