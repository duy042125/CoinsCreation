using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Middleware;
using Middleware.Database_Component;

namespace Interface
{
    /// <summary>
    /// Interaction logic for RewardApp.xaml
    /// </summary>
    public partial class RewardApp : Window
    {
        public RewardApp()
        {
            InitializeComponent();
        }
        
        private int TotalStar { get{return Behavior1.Value + Behavior2.Value + Behavior3.Value + Behavior4.Value;} }
        private int coinEarn = 0;

        private void Progress_Click(object sender, RoutedEventArgs e)
        {
            if (TotalStar >= 0 && TotalStar < 10) Progress.Text = "Not Enough";
            else if (TotalStar >= 10 && TotalStar < 12)
            {
                Progress.Text = "Very Low";
                coinEarn = 1;
            }
            else if (TotalStar >= 12 && TotalStar < 15) 
            {
                Progress.Text = "Some";
                coinEarn = 2;
            }
            else if (TotalStar >= 15 && TotalStar < 18) 
            {
                Progress.Text = "Medium";
                coinEarn = 3;
            }
            else if (TotalStar >= 18 && TotalStar < 20) 
            {
                Progress.Text = "High";
                coinEarn = 4;
            }
            else if (TotalStar == 20) 
            {
                Progress.Text = "Very High";
                coinEarn = 5;
            }
            CoinEarned.Content = coinEarn;
        }

        private void Edit_Info_Click(object sender, RoutedEventArgs e)
        {
            FillOut fill = new FillOut();
            fill.Show();
            this.Close();
        }

        private void Report_Shop_Click(object sender, RoutedEventArgs e)
        {
            ReportShop reportShop = new ReportShop();
            reportShop.Show();
            this.Close();
        }

        private void Information_Loaded(object sender, RoutedEventArgs e)
        {
            #region Behavior Loader
            Behavior behaviorLoader = DBConnection.retrieveBehaviorByName(LogInInformation.Behavior_name);

            // this could be fix 4 behaviors
            if (behaviorLoader.behavior1.Equals(""))
            {
                BehaviorName1.Content = "None";
                Behavior1.IsEnabled = false;
            }
            else BehaviorName1.Content = behaviorLoader.behavior1;
            if (behaviorLoader.behavior2.Equals(""))
            {
                BehaviorName2.Content = "None";
                Behavior2.IsEnabled = false;
            }
            else BehaviorName2.Content = behaviorLoader.behavior1;
            if (behaviorLoader.behavior3.Equals(""))
            {
                BehaviorName3.Content = "None";
                Behavior3.IsEnabled = false;
            }
            else BehaviorName3.Content = behaviorLoader.behavior1;
            if (behaviorLoader.behavior4.Equals(""))
            {
                BehaviorName4.Content = "None";
                Behavior4.IsEnabled = false;
            }
            else BehaviorName4.Content = behaviorLoader.behavior1;
            #endregion

            #region Child Loader
            Child childLoader = DBConnection.retrieveChildByName(LogInInformation.Username, LogInInformation.Child_name);
            ChildName.Text = childLoader.Child_name;
            Age.Text = DateTimeConverter.timeSpanToAge(DateTime.Now - childLoader.birthdate);
            Date.Text = DateTime.Now.ToString("MM/dd/yyyy");
            CoinEarned.Content = coinEarn;
            #endregion
        }

        private void Bank_Click(object sender, RoutedEventArgs e)
        {
            if(coinEarn != 0)
            {
                DBConnection.addCoin(LogInInformation.Username, LogInInformation.Child_name, coinEarn);
                LogInInformation.coin_earned = coinEarn;
                coinEarn = 0;
                CoinEarned.Content = coinEarn;
                MessageBox.Show("Congratulation! You just put 5 coins in your bank.","Banked Coin",MessageBoxButton.OK, MessageBoxImage.Information);
            }
            System.Media.SoundPlayer starSound = new System.Media.SoundPlayer(Interface.Properties.Resources.cashregg);
            starSound.Play();
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult warning = MessageBox.Show("Are you sure to log out ?", "Log Out", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (warning == MessageBoxResult.Yes)
            {
                MainWindow backToLogIn = new MainWindow();
                backToLogIn.Show();
                this.Close();
                LogInInformation.Clear();
            }
        }
       
    }
}

