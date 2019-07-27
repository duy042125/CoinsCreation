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
                    connection.Execute("dbo.SP_Account_Insert @username, @password, @full_name, @birthdate, @phone_number, @SSN", new { username = theUsername, password = thePassword, full_name = theName, birthdate = theBirthdate, phone_number = thePhoneNumber, SSN = theSSN });
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
                    var checkAccount = connection.Query<Account>("dbo.SP_Account_RetrieveAccountByUsername @username", new { username = theUsername }).ToList();
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

        public static bool verifiedAccount(string theUsername, string theSSN)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(getConnectionString("CreativeCoinConnection")))
                {
                    var checkAccount = connection.Query<Account>("dbo.SP_Account_RetrieveAccountByUsernameOrSSN @username, @SSN", new { username = theUsername, SSN = theSSN }).ToList();
                    if (checkAccount.Count != 0) return true;
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
                    var checkAccount = connection.Query<Account>("dbo.SP_Account_RetrieveAccountByUsername @username", new { username = theUsername }).ToList();
                    if (checkAccount.Count != 0) return true;
                    return false;
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public static bool verifiedUsedSSN(string theSSN)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(getConnectionString("CreativeCoinConnection")))
                {
                    var checkAccount = connection.Query<Account>("dbo.SP_Account_RetrieveAccountBySSN @SSN", new { SSN = theSSN }).ToList();
                    if (checkAccount.Count != 0) return true;
                    return false;
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public static Account retrieveAccount(string theUsername)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(getConnectionString("CreativeCoinConnection")))
                {
                    var accountList = connection.Query<Account>("dbo.SP_Account_RetrieveAccountByUsername @username", new { username = theUsername }).ToList();
                    return accountList[0];
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

        #region Child Connections

        public static void insertChild(string theParentUsername, string theChildName, DateTime? theBirthdate)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(getConnectionString("CreativeCoinConnection")))
                {
                    var checkAccount = connection.Execute("dbo.SP_Child_Insert @Parent_username, @Child_name, @birthdate", new { Parent_username = theParentUsername, Child_name = theChildName, birthdate = theBirthdate });
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public static bool verifiedChildName(string theParentUsername, string theChildName)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(getConnectionString("CreativeCoinConnection")))
                {
                    var checkAccount = connection.Query<Child>("dbo.SP_Child_RetrieveChildByName @Parent_username, @Child_name", new { Parent_username = theParentUsername, Child_name = theChildName }).ToList();
                    if (checkAccount.Count != 0) return true;
                    return false;
                }
            }
            catch (Exception exc)
            {
                throw exc; 
            }
        }

        public static List<Child> retrieveChildListByUsername(string theChildName)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(getConnectionString("CreativeCoinConnection")))
                {
                    return connection.Query<Child>("dbo.SP_Child_RetrieveChildListByUsername @Parent_username", new { Parent_username = theChildName }).ToList();
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public static Child retrieveChildByName(string theParentUsername, string theChildName)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(getConnectionString("CreativeCoinConnection")))
                {
                    var childList = connection.Query<Child>("dbo.SP_Child_RetrieveChildByName @Parent_username, @Child_name", new { Parent_username = theParentUsername, Child_name = theChildName }).ToList();
                    return childList[0];
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public static void updateChild(string theUsername, string theChildName, DateTime? theBirthdate)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(getConnectionString("CreativeCoinConnection")))
                {
                    var checkAccount = connection.Execute("dbo.SP_Child_UpdateChild @Parent_username, @Child_name, @birthdate", new { Parent_username = theUsername, Child_name = theChildName, birthdate = theBirthdate });
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public static void addCoin(string theUsername, string theChildName, int theNewCoin)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(getConnectionString("CreativeCoinConnection")))
                {
                    var checkAccount = connection.Execute("dbo.SP_Child_AddCoin @Parent_username, @Child_name, @newCoin", new { Parent_username = theUsername, Child_name = theChildName, newCoin = theNewCoin });
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public static void useCoin(string theUsername, string theChildName, int theUseCoin)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(getConnectionString("CreativeCoinConnection")))
                {
                    var checkAccount = connection.Execute("dbo.SP_Child_UseCoin @Parent_username, @Child_name, @useCoin", new { Parent_username = theUsername, Child_name = theChildName, newCoin = theUseCoin });
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        #endregion

        #region Behavior Connections

        public static bool verifiedBehaviorName(string theBehaviorName)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(getConnectionString("CreativeCoinConnection")))
                {
                    var checkAccount = connection.Query<Behavior>("dbo.SP_Behavior_RetrieveBehaviorByName @name", new { name = theBehaviorName }).ToList();
                    if (checkAccount.Count != 0) return true;
                    return false;
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public static void updateBehavior(string theBehaviorName, string theBehavior1, string theBehavior2, string theBehavior3, string theBehavior4,
            string theStar5_reward1, string theStar5_reward2, string theStar5_reward3, string theStar10_reward1, string theStar10_reward2, string theStar10_reward3, string theStar15_reward1, string theStar15_reward2, string theStar20_reward)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(getConnectionString("CreativeCoinConnection")))
                {
                    var checkAccount = connection.Execute("dbo.SP_Behavior_UpdateBehavior  @Behavior_name, @behavior1, @behavior2, @behavior3, @behavior4, @star5_reward1, @star5_reward2, @star5_reward3, @star10_reward1, @star10_reward2, @star10_reward3, @star15_reward1, @star15_reward2, @star20_reward", new
                    {
                        Behavior_name = theBehaviorName,
                        behavior1 = theBehavior1,
                        behavior2 = theBehavior2,
                        behavior3 = theBehavior3,
                        behavior4 = theBehavior4,
                        star5_reward1 = theStar5_reward1,
                        star5_reward2 = theStar5_reward2,
                        star5_reward3 = theStar5_reward3,
                        star10_reward1 = theStar10_reward1,
                        star10_reward2 = theStar10_reward2,
                        star10_reward3 = theStar10_reward3,
                        star15_reward1 = theStar15_reward1,
                        star15_reward2 = theStar15_reward2,
                        star20_reward = theStar20_reward
                    });
                }
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }

        public static Behavior retrieveBehaviorByName(string theBehaviorName)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(getConnectionString("CreativeCoinConnection")))
                {
                    var behaviorList = connection.Query<Behavior>("dbo.SP_Behavior_RetrieveBehaviorByName @Behavior_name", new { Behavior_name = theBehaviorName }).ToList();
                    return behaviorList[0];
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        #endregion

        #region Behavior Assign Connections

        public static void insertBehavior(string theParentUsername, string theBehaviorName, string theBehavior1, string theBehavior2, string theBehavior3, string theBehavior4,
            string theStar5_reward1, string theStar5_reward2, string theStar5_reward3, string theStar10_reward1, string theStar10_reward2, string theStar10_reward3, string theStar15_reward1, string theStar15_reward2, string theStar20_reward)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(getConnectionString("CreativeCoinConnection")))
                {
                    var checkAccount = connection.Execute("dbo.SP_Behavior_Assign_InsertBehavior @Parent_username, @Behavior_name, @behavior1, @behavior2, @behavior3, @behavior4, @star5_reward1, @star5_reward2, @star5_reward3, @star10_reward1, @star10_reward2, @star10_reward3, @star15_reward1, @star15_reward2, @star20_reward", new { Parent_username = theParentUsername, Behavior_name = theBehaviorName,
                        behavior1 = theBehavior1,
                        behavior2 = theBehavior2,
                        behavior3 = theBehavior3,
                        behavior4 = theBehavior4,
                        star5_reward1 = theStar5_reward1,
                        star5_reward2 = theStar5_reward2,
                        star5_reward3 = theStar5_reward3,
                        star10_reward1 = theStar10_reward1,
                        star10_reward2 = theStar10_reward2,
                        star10_reward3 = theStar10_reward3,
                        star15_reward1 = theStar15_reward1,
                        star15_reward2 = theStar15_reward2,
                        star20_reward = theStar20_reward
                });
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
        
        public static List<Behavior> retrieveBehaviorListByUsername(string theParentUsername)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(getConnectionString("CreativeCoinConnection")))
                {
                    return connection.Query<Behavior>("dbo.SP_Behavior_Assign_RetrieveBehaviorListByUsername @Parent_username", new { Parent_username = theParentUsername }).ToList();
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        #endregion

        #region Report Connections

        public static void insertReport(string theParentUsername, string theChildName, string theBehaviorName, DateTime? theDate, int theCoinEarned, string theNote)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(getConnectionString("CreativeCoinConnection")))
                {
                    connection.Execute("dbo.SP_Report_Insert @Parent_username, @Child_name, @Behavior_name, @date, @coin_earned, @note", new { Parent_username = theParentUsername, Child_name = theChildName, Behavior_name = theBehaviorName, date = theDate, coin_earned = theCoinEarned, note = theNote });
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