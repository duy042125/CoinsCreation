using System;
using System.Collections.Generic;
using System.Linq;
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

        private DBConnection() { }

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

        public static void createAccount(string theUsername, string thePassword, string theName, DateTime? theBirthdate, string thePhoneNumber)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(getConnectionString("CreativeCoinConnection")))
                {
                    connection.Execute("dbo.SP_Account_Insert @username, @password, @full_name, @birthdate, @phone_number", new { username = theUsername, password = thePassword, full_name = theName, birthdate = DateTimeConverter.dateTimeToString(theBirthdate), phone_number = thePhoneNumber});
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
                    var checkAccount = connection.Query<Account>("dbo.SP_Account_RetrieveAccountByKeys @username", new { username = theUsername }).ToList();
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

        public static bool verifiedAccount(string theUsername, string thePhoneNumber)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(getConnectionString("CreativeCoinConnection")))
                {
                    var checkAccount = connection.Query<Account>("dbo.SP_Account_RetrieveAccountByUsernameOrPhone @username, @phone_number", new { username = theUsername, phone_number = thePhoneNumber }).ToList();
                    if (checkAccount.Count != 0) return true;
                    return false;
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public static bool verifiedAccountType(string theUsername)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(getConnectionString("CreativeCoinConnection")))
                {
                    var checkAccount = connection.Query<Account>("dbo.SP_Account_RetrieveAccountTypeByUsername @username", new { username = theUsername }).ToList();
                    if (checkAccount.Count != 0)
                    {
                        if (checkAccount[0].type == 'A') return true;       // 'A' for Adminstrator 'N' for Normal Users
                        return false;
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
                    var checkAccount = connection.Query<Account>("dbo.SP_Account_RetrieveAccountByKeys @username", new { username = theUsername }).ToList();
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
                    connection.Execute("dbo.SP_Account_ChangePassword @username, @password", new { username = theUsername, password = thePassword });
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public static List<Account> retrieveAllAccounts()
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(getConnectionString("CreativeCoinConnection")))
                {
                    return connection.Query<Account>("dbo.SP_Account_RetrieveAllAccounts").ToList();
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public static void deleteAccount(Account account)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(getConnectionString("CreativeCoinConnection")))
                {
                    connection.Execute("dbo.SP_Account_DeleteAccount @username", account);
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public static void updateAccountByKeys(Account theAccount)
        {
            try
            {
                if (theAccount.type == 'a') theAccount.type = 'A';
                else if (theAccount.type == 'n') theAccount.type = 'N';
                using (IDbConnection connection = new SqlConnection(getConnectionString("CreativeCoinConnection")))
                {
                    connection.Execute("dbo.SP_Account_UpdateAccountByKeys @username, @full_name, @birthdate, @phone_number, @type", theAccount);
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
                    var checkChild = connection.Execute("dbo.SP_Child_Insert @Parent_username, @Child_name, @birthdate, @start_date", new { Parent_username = theParentUsername, Child_name = theChildName, birthdate = DateTimeConverter.dateTimeToString(theBirthdate), start_date = DateTimeConverter.dateTimeToString(DateTime.Now) });
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
                    var checkChild = connection.Query<Child>("dbo.SP_Child_RetrieveChildByKeys @Parent_username, @Child_name", new { Parent_username = theParentUsername, Child_name = theChildName }).ToList();
                    if (checkChild.Count != 0) return true;
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

        public static Child retrieveChildByKeys(string theParentUsername, string theChildName)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(getConnectionString("CreativeCoinConnection")))
                {
                    var childList = connection.Query<Child>("dbo.SP_Child_RetrieveChildByKeys @Parent_username, @Child_name", new { Parent_username = theParentUsername, Child_name = theChildName }).ToList();
                    return childList[0];
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public static void updateChildByKeys(string theUsername, string theChildName, DateTime? theBirthdate)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(getConnectionString("CreativeCoinConnection")))
                {
                    connection.Execute("dbo.SP_Child_UpdateChildByKeys @Parent_username, @Child_name, @birthdate", new { Parent_username = theUsername, Child_name = theChildName, birthdate = DateTimeConverter.dateTimeToString(theBirthdate) });
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public static void updateChildByKeys(Child theChild)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(getConnectionString("CreativeCoinConnection")))
                {
                    connection.Execute("dbo.SP_Child_UpdateChildByKeys @Parent_username, @Child_name, @birthdate", theChild);
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
                    connection.Execute("dbo.SP_Child_AddCoin @Parent_username, @Child_name, @newCoin", new { Parent_username = theUsername, Child_name = theChildName, newCoin = theNewCoin });
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
                    connection.Execute("dbo.SP_Child_UseCoin @Parent_username, @Child_name, @useCoin", new { Parent_username = theUsername, Child_name = theChildName, useCoin = theUseCoin });
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public static List<Child> retrieveAllChildren()
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(getConnectionString("CreativeCoinConnection")))
                {
                    return connection.Query<Child>("dbo.SP_Child_RetrieveAllChildren").ToList();
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public static void deleteChild(Child child)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(getConnectionString("CreativeCoinConnection")))
                {
                    connection.Execute("dbo.SP_Child_DeleteChild @Parent_username, @Child_name", child);
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        #endregion

        #region Behavior Connections

        public static void insertBehavior(string theParentUsername, string theBehaviorName, string theBehavior1, string theBehavior2, string theBehavior3, string theBehavior4,
            string theStar5_reward1, string theStar5_reward2, string theStar5_reward3, string theStar10_reward1, string theStar10_reward2, string theStar10_reward3, string theStar15_reward1, string theStar15_reward2, string theStar20_reward)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(getConnectionString("CreativeCoinConnection")))
                {
                    var checkAccount = connection.Execute("dbo.SP_Behavior_Insert @Parent_username, @Behavior_name, @behavior1, @behavior2, @behavior3, @behavior4, @star5_reward1, @star5_reward2, @star5_reward3, @star10_reward1, @star10_reward2, @star10_reward3, @star15_reward1, @star15_reward2, @star20_reward", new
                    {
                        Parent_username = theParentUsername,
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

        public static bool verifiedBehaviorName(string theBehaviorName)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(getConnectionString("CreativeCoinConnection")))
                {
                    var checkAccount = connection.Query<Behavior>("dbo.SP_Behavior_RetrieveBehaviorByKeys @Parent_username, @Behavior_name", new {Parent_username = LogInInformation.Username, Behavior_name = theBehaviorName }).ToList();
                    if (checkAccount.Count != 0) return true;
                    return false;
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public static void updateBehaviorByKeys(string theBehaviorName, string theBehavior1, string theBehavior2, string theBehavior3, string theBehavior4,
            string theStar5_reward1, string theStar5_reward2, string theStar5_reward3, string theStar10_reward1, string theStar10_reward2, string theStar10_reward3, string theStar15_reward1, string theStar15_reward2, string theStar20_reward)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(getConnectionString("CreativeCoinConnection")))
                {
                    connection.Execute("dbo.SP_Behavior_UpdateBehaviorByKeys @Parent_username, @Behavior_name, @behavior1, @behavior2, @behavior3, @behavior4, @star5_reward1, @star5_reward2, @star5_reward3, @star10_reward1, @star10_reward2, @star10_reward3, @star15_reward1, @star15_reward2, @star20_reward", new
                    {
                        Parent_username = LogInInformation.Username,
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

        public static void updateBehaviorByKeys(Behavior theBehavior)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(getConnectionString("CreativeCoinConnection")))
                {
                    connection.Execute("dbo.SP_Behavior_UpdateBehaviorByKeys @Parent_username, @Behavior_name, @behavior1, @behavior2, @behavior3, @behavior4, @star5_reward1, @star5_reward2, @star5_reward3, @star10_reward1, @star10_reward2, @star10_reward3, @star15_reward1, @star15_reward2, @star20_reward", theBehavior);
                }
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }

        public static Behavior retrieveBehaviorByKeys(string theBehaviorName)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(getConnectionString("CreativeCoinConnection")))
                {
                    var behaviorList = connection.Query<Behavior>("dbo.SP_Behavior_RetrieveBehaviorByKeys @Parent_username, @Behavior_name", new { Parent_username = LogInInformation.Username, Behavior_name = theBehaviorName }).ToList();
                    return behaviorList[0];
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public static List<Behavior> retrieveAllBehaviors()
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(getConnectionString("CreativeCoinConnection")))
                {
                    return connection.Query<Behavior>("dbo.SP_Behavior_RetrieveAllBehaviors").ToList();
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public static void deleteBehavior(Behavior behavior)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(getConnectionString("CreativeCoinConnection")))
                {
                    connection.Execute("dbo.SP_Behavior_DeleteBehavior @Behavior_name", behavior);
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
                    return connection.Query<Behavior>("dbo.SP_Behavior_RetrieveListByUsername @Parent_username", new { Parent_username = theParentUsername }).ToList();
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
                    connection.Execute("dbo.SP_Report_Insert @Parent_username, @Child_name, @Behavior_name, @date, @coin_earned, @note", new { Parent_username = theParentUsername, Child_name = theChildName, Behavior_name = theBehaviorName, date = DateTimeConverter.dateTimeToString(theDate), coin_earned = theCoinEarned, note = theNote });
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public static List<Report> retrieveReportListByUsernameAndChildName(string theParentUsername, string theChildName)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(getConnectionString("CreativeCoinConnection")))
                {
                    return connection.Query<Report>("dbo.SP_Report_RetrieveReportListByUsernameAndChildName @Parent_username, @Child_name", new { Parent_username = theParentUsername, Child_name = theChildName }).ToList();
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public static Report retrieveReportByKeys(string theParentUsername, string theChildName, string theBehaviorName, DateTime? theDate)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(getConnectionString("CreativeCoinConnection")))
                {
                    var checkReport = connection.Query<Report>("dbo.SP_Report_RetrieveReportByKeys @Parent_username, @Child_name, @Behavior_name, @date", new { Parent_username = theParentUsername, Child_name = theChildName, Behavior_name = theBehaviorName, date = DateTimeConverter.dateTimeToString(theDate) }).ToList();
                    return checkReport[0];
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public static bool verirfiedReportByKeys(string theParentUsername, string theChildName, string theBehaviorName, DateTime? theDate)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(getConnectionString("CreativeCoinConnection")))
                {
                    string c = DateTimeConverter.dateTimeToString(theDate);
                    var checkReport = connection.Query<Report>("dbo.SP_Report_RetrieveReportByKeys @Parent_username, @Child_name, @Behavior_name, @date", new { Parent_username = theParentUsername, Child_name = theChildName, Behavior_name = theBehaviorName, date = DateTimeConverter.dateTimeToString(theDate) }).ToList();
                    if (checkReport.Count != 0) return true;
                    return false;
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public static void updateReportByKeys(string theParentUsername, string theChildName, string theBehaviorName, DateTime? theDate, int theCoinEarned, string theNote)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(getConnectionString("CreativeCoinConnection")))
                {
                    connection.Execute("dbo.SP_Report_UpdateReportByKeys @Parent_username, @Child_name, @Behavior_name, @date, @coin_earned, @note", new { Parent_username = theParentUsername, Child_name = theChildName, Behavior_name = theBehaviorName, date = DateTimeConverter.dateTimeToString(theDate), coin_earned = theCoinEarned, note = theNote });
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public static void updateReportByKeys(Report theReport)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(getConnectionString("CreativeCoinConnection")))
                {
                    connection.Execute("dbo.SP_Report_UpdateReportByKeys @Parent_username, @Child_name, @Behavior_name, @date, @coin_earned, @note", theReport);
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public static List<Report> retrieveAllReports()
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(getConnectionString("CreativeCoinConnection")))
                {
                    var a = connection.Query<Report>("dbo.SP_Report_RetrieveAllReports").ToList();
                    return a;
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public static TimeSpan? retrieveProgressWeek(string theParentUsername, string theChildName, string currentReportDate)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(getConnectionString("CreativeCoinConnection")))
                {
                    var retreiveChild = connection.Query<Child>("dbo.SP_Child_RetrieveChildStartDateByUsernameAndChildName @Parent_username, @Child_name", new { Parent_username = theParentUsername, Child_name = theChildName }).ToList();
                    DateTime? startDate = DateTimeConverter.stringToDateTime(retreiveChild[0].start_date);
                    DateTime? currentDate = DateTimeConverter.stringToDateTime(currentReportDate);
                    TimeSpan? progressWeek = (currentDate - startDate);

                    //TimeSpan progressWeek = currentDate.Subtract(startDate).Duration();
                    return progressWeek;
                }
            }
            catch (Exception exc) 
            {
                throw exc;
            }
        }

        public static Report retrieveFullReportByKeys(string theParentUsername, string theChildName, string theDate)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(getConnectionString("CreativeCoinConnection")))
                {
                    var checkReport = connection.Query<Report>("dbo.SP_Report_RetrieveFullReportInformationByKeys @Parent_username, @Child_name, @date", new { Parent_username = theParentUsername, Child_name = theChildName, date = theDate }).ToList();
                    return checkReport[0];
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public static void deleteReport(Report report)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(getConnectionString("CreativeCoinConnection")))
                {
                    connection.Execute("dbo.SP_Report_DeleteReport @Parent_username, @Child_name, @Behavior_name, @date", report);
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