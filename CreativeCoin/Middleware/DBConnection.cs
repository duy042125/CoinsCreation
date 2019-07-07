﻿using System;
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

        public static void createAccount(string theUsername, string thePassword, string theName, DateTime theBirthdate, string thePhoneNumber, string theSSN)
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
                    var checkAccount = connection.QuerySingle("dbo.SP_Account_VerifiedLogIn @username", new { username = theUsername });
                    if (thePassword != checkAccount.password) return false;
                    else return true;
                }
            }
            catch
            {
                //If the lists is blank => No match account
                return false;
            }
        }

        public static bool verifiedUsername(string theUsername, string theSSN, ref Account save)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(getConnectionString("CreativeCoinConnection")))
                {
                    var checkAccount = connection.QuerySingle("dbo.SP_Account_VerifiedUsername @username @SSN", new { username = theUsername });
                    return true;
                }
            }
            catch
            {
                //If the lists is blank => No match account
                return false;
            }
        }

        public static bool verifiedUsedUsername(string theUsername)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(getConnectionString("CreativeCoinConnection")))
                {
                    var checkAccount = connection.QuerySingle("dbo.SP_Account_VerifiedLogIn @username", new { username = theUsername });
                    return true;
                }
            }
            catch
            {
                //If the lists is blank => No match account
                return false;
            }
        }

        #endregion
    }
}