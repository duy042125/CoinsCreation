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
using Middleware.Database_Component;
using Middleware;

namespace Interface
{
    /// <summary>
    /// Interaction logic for FillOut.xaml
    /// </summary>
    public partial class FillOut : Window
    {

        public FillOut()
        {
            InitializeComponent();
        }


        private void Tip_Click(object sender, RoutedEventArgs e) // this might need a custom window for it.
        {
            string message = "5 coins (1 week of perfect behavior) = favorite meal, ice cream, a dollar store item, craft/bake together \n" +
                "10 coins (2 weeks of perfect behavior) = extra video game hour, extra TV hour, one chore-free night \n" +
                "15 coins (3 weeks of perfect behavior) = sleepover, movie, choice of restauraunt, download game app \n" +
                "20 coins (4 weeks of perfect hehavior) = trip to FunZone, trip to Edge Waterpark, new console game";
            MessageBox.Show(message, "Tip", MessageBoxButton.OK, MessageBoxImage.Information);
        }


        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult warning = MessageBox.Show("Are you sure to clear everything ?", "Clear", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (warning == MessageBoxResult.Yes)
            {
                #region Clear Name

                ChildName.Text = "";
                Birthdate.Text = "";

                #endregion

                #region Clear Behaviors

                Behavior1.Text = "";
                Behavior2.Text = "";
                Behavior3.Text = "";
                Behavior4.Text = "";

                #endregion

                #region Clear Rewards

                Coin51.Text = "";
                Coin52.Text = "";
                Coin53.Text = "";
                Coin101.Text = "";
                Coin102.Text = "";
                Coin103.Text = "";
                Coin151.Text = "";
                Coin152.Text = "";
                Coin20.Text = "";

                #endregion
            }
        }

        private bool isFillOut()
        {
            if (ChildName.Text.Equals("") || Birthdate.Text.Equals("") || BehaviorName.Text.Equals("") || Behavior1.Text.Equals("") || Behavior2.Text.Equals("") || Behavior3.Text.Equals("") || Behavior4.Text.Equals("") || 
                Coin51.Text.Equals("") || Coin52.Text.Equals("") || Coin53.Text.Equals("") || Coin101.Text.Equals("") || Coin102.Text.Equals("") || Coin103.Text.Equals("") || Coin151.Text.Equals("") || Coin152.Text.Equals("") || Coin20.Text.Equals(""))
            {
                MessageBox.Show("Please fill up all required fields !", "Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            else return true;
        }

        private bool isChildExist()
        {
            return true; // unfinish 
        }

        private bool isBehaviorExist()
        {
            return true; //unfinish
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            if (isFillOut())
            {
                if (!isChildExist())
                {
                    if (!isBehaviorExist())
                    {
                        DBConnection.insertChild(LogInInformation.Username, ChildName.Text, Birthdate.Text);
                        DBConnection.insertBehavior(BehaviorName.Text, Behavior1.Text, Behavior2.Text, Behavior3.Text, Behavior4.Text, Coin51.Text, Coin52.Text, Coin53.Text, Coin101.Text, Coin102.Text, Coin103.Text, Coin151.Text, Coin152.Text, Coin20.Text);
                    }
                    else DBConnection.insertBehavior(BehaviorName.Text, Behavior1.Text, Behavior2.Text, Behavior3.Text, Behavior4.Text, Coin51.Text, Coin52.Text, Coin53.Text, Coin101.Text, Coin102.Text, Coin103.Text, Coin151.Text, Coin152.Text, Coin20.Text);
                    
                    LogInInformation.Child_name = ChildName.Text;
                    LogInInformation.Behavior_name = BehaviorName.Text;
                    RewardApp reward = new RewardApp();
                    reward.Show();
                    this.Close();
                }
            }
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult warning = MessageBox.Show("Are you sure to log out ?", "Log Out", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if(warning == MessageBoxResult.Yes)
            {
                MainWindow backToLogIn = new MainWindow();
                backToLogIn.Show();
                this.Close();
            }
        }

        private void Behavior_Text_FillOut(object sender, MouseButtonEventArgs e)
        {
            BehaviorName.Text = "";
        }
    }
}
