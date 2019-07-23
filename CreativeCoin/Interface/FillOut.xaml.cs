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

            if(LogInInformation.Child_name != null && LogInInformation.Behavior_name != null)
            {
                autoFillBehavior(LogInInformation.Behavior_name);
                autoFillChild(LogInInformation.Child_name);
            }
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

                BehaviorName.Text = "";
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

        #region Check Method

        private bool isFillOut()
        {

            if (ChildName.Text.Equals("") || Birthdate.Text.Equals("") || BehaviorName.Text.Equals("") || Behavior1.Text.Equals("") || Behavior2.Text.Equals("") || Behavior3.Text.Equals("") || Behavior4.Text.Equals("") ||
                Coin51.Text.Equals("") || Coin52.Text.Equals("") || Coin53.Text.Equals("") || Coin101.Text.Equals("") || Coin102.Text.Equals("") || Coin103.Text.Equals("") || Coin151.Text.Equals("") || Coin152.Text.Equals("") || Coin20.Text.Equals(""))
            {
                MessageBox.Show("Please fill up all required fields !", "Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if(!checkBirthdate())
            {
                MessageBox.Show("The Birthdate format is wrong", "Wrong Birthdate format.", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }

        private bool isChildExist()
        {
            return DBConnection.verifiedChildName(LogInInformation.Username, ChildName.Text);
        }

        private bool isBehaviorExist()
        {
            return DBConnection.verifiedBehaviorName(BehaviorName.Text);
        }

        private bool checkBirthdate()
        {
            if (Birthdate.SelectedDate.HasValue)
            {
                DateTime birthdate = Birthdate.SelectedDate.Value;
                TimeSpan age = DateTime.Now - birthdate;
                if (age.Days <= 0) return false;
                return true;
            }
            return false;
        }

        private void autoFillBehavior(string theBehaviorName)
        {
            if (DBConnection.verifiedBehaviorName(theBehaviorName))
            {
                Behavior currentBehavior = DBConnection.retrieveBehaviorByName(theBehaviorName);
                BehaviorName.Text = currentBehavior.name;
                Behavior1.Text = currentBehavior.behavior1;
                Behavior2.Text = currentBehavior.behavior2;
                Behavior3.Text = currentBehavior.behavior3;
                Behavior4.Text = currentBehavior.behavior4;
                Coin51.Text = currentBehavior.star5_reward1;
                Coin52.Text = currentBehavior.star5_reward2;
                Coin53.Text = currentBehavior.star5_reward3;
                Coin101.Text = currentBehavior.star10_reward1;
                Coin102.Text = currentBehavior.star10_reward2;
                Coin103.Text = currentBehavior.star10_reward3;
                Coin151.Text = currentBehavior.star15_reward1;
                Coin152.Text = currentBehavior.star15_reward2;
                Coin20.Text = currentBehavior.star20_reward;
                isCheck = true;
                return;
            }
            MessageBox.Show("There is no Behavior Group that has name like this.", "No Behavior Group", MessageBoxButton.OK, MessageBoxImage.Warning);
            isCheck = false;
        }

        private void autoFillChild(string theChildName)
        {
            if(DBConnection.verifiedChildName(LogInInformation.Username, theChildName))
            {
                Child currentChild = DBConnection.retrieveChildByName(LogInInformation.Username, theChildName);
                ChildName.Text = currentChild.Child_name;
                Birthdate.Text = DateTimeConverter.dateTimeToString(currentChild.birthdate);
                isCheck = true;
                return;
            }
            MessageBox.Show("There is no Child that has name like this.", "No Child", MessageBoxButton.OK, MessageBoxImage.Warning);
            isCheck = false;
        }

        private static bool isCheck = false;
        
        #endregion

        private void AutoFill_Click(object sender, RoutedEventArgs e)
        {
            autoFillBehavior(BehaviorName.Text);
            autoFillChild(ChildName.Text);
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            if (isFillOut())
            {
                if (!isChildExist() && !isBehaviorExist())
                {
                    DBConnection.insertChild(LogInInformation.Username, ChildName.Text, Birthdate.SelectedDate);
                    DBConnection.insertBehavior(LogInInformation.Username, BehaviorName.Text, Behavior1.Text, Behavior2.Text, Behavior3.Text, Behavior4.Text, Coin51.Text, Coin52.Text, Coin53.Text, Coin101.Text, Coin102.Text, Coin103.Text, Coin151.Text, Coin152.Text, Coin20.Text);
                }
                else if (!isChildExist() && isBehaviorExist())
                {
                    DBConnection.insertChild(LogInInformation.Username, ChildName.Text, Birthdate.SelectedDate);
                    MessageBoxResult result = MessageBox.Show("This Behavior Group already existed, Do you want to fill you with the old information? (Click NO to update them)", "Existed Child and Behavior Group", MessageBoxButton.YesNo, MessageBoxImage.Information);
                    if (result == MessageBoxResult.Yes) autoFillBehavior(BehaviorName.Text);
                    else if (result == MessageBoxResult.No) DBConnection.updateBehavior(BehaviorName.Text, Behavior1.Text, Behavior2.Text, Behavior3.Text, Behavior4.Text, Coin51.Text, Coin52.Text, Coin53.Text, Coin101.Text, Coin102.Text, Coin103.Text, Coin151.Text, Coin152.Text, Coin20.Text);
                    isCheck = true;
                }
                else if (isChildExist() && !isBehaviorExist())
                {
                    DBConnection.insertBehavior(LogInInformation.Username, BehaviorName.Text, Behavior1.Text, Behavior2.Text, Behavior3.Text, Behavior4.Text, Coin51.Text, Coin52.Text, Coin53.Text, Coin101.Text, Coin102.Text, Coin103.Text, Coin151.Text, Coin152.Text, Coin20.Text);
                    MessageBoxResult result = MessageBox.Show("This Child's Name already existed, Do you want to fill you with the old information? (Click NO to update them)", "Existed Child's Name", MessageBoxButton.YesNo, MessageBoxImage.Information);
                    if (result == MessageBoxResult.Yes) autoFillChild(ChildName.Text);
                    else if (result == MessageBoxResult.No) DBConnection.updateChild(LogInInformation.Username, ChildName.Text, Birthdate.SelectedDate);
                    isCheck = true;
                }
                else if (isBehaviorExist() && isBehaviorExist() && !isCheck)
                {
                    MessageBoxResult result = MessageBox.Show("These Child's Name and Behavior Group already existed, Do you want to fill you with the old information? (Click NO to update them)", "Existed Child and Behavior Group", MessageBoxButton.YesNo, MessageBoxImage.Information);
                    if (result == MessageBoxResult.Yes)
                    {
                        autoFillBehavior(BehaviorName.Text);
                        autoFillChild(ChildName.Text);
                    }
                    else if (result == MessageBoxResult.No)
                    {
                        DBConnection.updateBehavior(BehaviorName.Text, Behavior1.Text, Behavior2.Text, Behavior3.Text, Behavior4.Text, Coin51.Text, Coin52.Text, Coin53.Text, Coin101.Text, Coin102.Text, Coin103.Text, Coin151.Text, Coin152.Text, Coin20.Text);
                        DBConnection.updateChild(LogInInformation.Username, ChildName.Text, Birthdate.SelectedDate);
                    }
                    isCheck = true;
                    return;
                }
            }
            if (isCheck)
            {
                LogInInformation.Child_name = ChildName.Text;
                LogInInformation.Behavior_name = BehaviorName.Text;
                isCheck = false;
                RewardApp reward = new RewardApp();
                reward.Show();
                this.Close();
            }
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

        private void Behavior_Text_FillOutClear(object sender, MouseButtonEventArgs e)
        {
            BehaviorName.Text = "";
        }
    }
}
