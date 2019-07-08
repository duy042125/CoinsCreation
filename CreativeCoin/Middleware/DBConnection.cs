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

        public static void createAccount(string theUsername, string thePassword, string theName, DateTime? theBirthdate, string thePhoneNumber, string theSSN)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(getConnectionString("CreativeCoinConnection")))
                {
                    Account newAccount = new Account(theUsername, thePassword, theName, theBirthdate, thePhoneNumber, theSSN);
                    connection.Execute("dbo.SP_Account_Insert", newAccount, null, null, CommandType.StoredProcedure);
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public static bool verifiedLogIn(string theUsername, string thePassword) 
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(getConnectionString("CreativeCoinConnection")))
                {
                    var checkAccount = connection.Query<Account>("dbo.SP_Account_VerifiedLogIn @username", new { username = theUsername }).ToList();
                    if (checkAccount.Count != 0)
                    {
                        if (thePassword != checkAccount[0].password) return false;
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public static bool verifiedUsername(string theUsername, string theSSN, ref Account save)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(getConnectionString("CreativeCoinConnection")))
                {
                    var checkAccount = connection.Query<Account>("dbo.SP_Account_VerifiedUsername @username, @SSN", new { username = theUsername, SSN = theSSN }).ToList();
                    if(checkAccount.Count != 0)
                    {
                        save = checkAccount[0];
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public static bool verifiedUsedUsername(string theUsername)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(getConnectionString("CreativeCoinConnection")))
                {
                    var checkAccount = connection.Query<Account>("dbo.SP_Account_VerifiedLogIn @username", new { username = theUsername }).ToList();
                    if (checkAccount.Count != 0) return true;
                    return false;
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public static void changePassword(string theUsername, string thePassword)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(getConnectionString("CreativeCoinConnection")))
                {
                    var checkAccount = connection.Execute("dbo.SP_Account_ChangePassword @username, @password", new { username = theUsername, password = thePassword });
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        #endregion
    }
}