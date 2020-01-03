using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
            updateComboBox();
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

        private void updateComboBox()
        {
            #region Child Name ComboBox
            List<Child> childList = DBConnection.retrieveChildListByUsername(LogInInformation.Username);
            foreach (Child child in childList)
            {
                if(!ChildName.Items.Contains(child.Child_name)) ChildName.Items.Add(child.Child_name);
            }
            #endregion

            #region Behavior Name ComboBox
            List<Behavior> behaviorList = DBConnection.retrieveBehaviorListByUsername(LogInInformation.Username);
            foreach (Behavior behavior in behaviorList)
            {
                if (!BehaviorName.Items.Contains(behavior.Behavior_name)) BehaviorName.Items.Add(behavior.Behavior_name);
            }
            #endregion
        }

        #region Check Method

        private bool isFillOut()
        {

            if (ChildName.Text.Equals("") || Birthdate.Text.Equals("") || BehaviorName.Text.Equals(""))
            {
                MessageBox.Show("Please fill up all required fields !", "Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (!checkBirthdate())
            {
                MessageBox.Show("The Birthdate format is wrong", "Wrong Birthdate format.", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }

            #region Verified Child

        private bool isChildExist()
        {
            return DBConnection.verifiedChildName(LogInInformation.Username, ChildName.Text);
        }

        private bool isChildTheSame()
        {
            if(isChildExist())
            {
                Child currentChild = DBConnection.retrieveChildByKeys(LogInInformation.Username, ChildName.Text);
                if (currentChild.Child_name == ChildName.Text && currentChild.birthdate == DateTimeConverter.dateTimeToString(Birthdate.SelectedDate)) return true;
                return false;
            }
            return false;
        }

        private void updateExistChild()
        {
            if(!isChildTheSame())
            {
                MessageBoxResult result = MessageBox.Show("Do you want to update your information?", "Update Information", MessageBoxButton.YesNo, MessageBoxImage.Information);
                if (result == MessageBoxResult.Yes)
                {
                    DBConnection.updateChildByKeys(LogInInformation.Username, ChildName.Text, Birthdate.SelectedDate);
                }
                else if (result == MessageBoxResult.No) autoFillChild(ChildName.Text);
                updateComboBox();
            }
        }

            #endregion

            #region Verified Behavior

        private bool isBehaviorExist()
        {
            return DBConnection.verifiedBehaviorName(BehaviorName.Text);
        }

        private bool isBehaviorTheSame()
        {
            if (isChildExist())
            {
                Behavior currentBehavior = DBConnection.retrieveBehaviorByKeys(BehaviorName.Text);
                if (currentBehavior.Behavior_name == BehaviorName.Text &&
                    currentBehavior.behavior1 == Behavior1.Text &&
                    currentBehavior.behavior2 == Behavior2.Text &&
                    currentBehavior.behavior3 == Behavior3.Text &&
                    currentBehavior.behavior4 == Behavior4.Text &&
                    currentBehavior.star5_reward1 == Coin51.Text &&
                    currentBehavior.star5_reward2 == Coin52.Text &&
                    currentBehavior.star5_reward3 == Coin53.Text &&
                    currentBehavior.star10_reward1 == Coin101.Text &&
                    currentBehavior.star10_reward2 == Coin102.Text &&
                    currentBehavior.star10_reward3 == Coin103.Text &&
                    currentBehavior.star15_reward1 == Coin151.Text &&
                    currentBehavior.star15_reward2 == Coin152.Text &&
                    currentBehavior.star20_reward == Coin20.Text) return true;
                return false;
            }
            return false;
        }

        private void updateExistBehavior()
        {
            if(!isBehaviorTheSame())
            {
                MessageBoxResult result = MessageBox.Show("Do you want to update your information?", "Update Information", MessageBoxButton.YesNo, MessageBoxImage.Information);
                if (result == MessageBoxResult.Yes)
                {
                    DBConnection.updateBehaviorByKeys(BehaviorName.Text, Behavior1.Text, Behavior2.Text, Behavior3.Text, Behavior4.Text, Coin51.Text, Coin52.Text, Coin53.Text, Coin101.Text, Coin102.Text, Coin103.Text, Coin151.Text, Coin152.Text, Coin20.Text);
                }
                else if (result == MessageBoxResult.No) autoFillBehavior(BehaviorName.Text);
                updateComboBox();
            }
        }
            #endregion


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
                Behavior currentBehavior = DBConnection.retrieveBehaviorByKeys(theBehaviorName);
                BehaviorName.Text = currentBehavior.Behavior_name;
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
                return;
            }
            isCheck = false;
        }

        private void autoFillChild(string theChildName)
        {
            if(DBConnection.verifiedChildName(LogInInformation.Username, theChildName))
            {
                Child currentChild = DBConnection.retrieveChildByKeys(LogInInformation.Username, theChildName);
                ChildName.Text = currentChild.Child_name;
                Birthdate.Text = currentChild.birthdate;
                return;
            }
            isCheck = false;
        }

        private static bool isCheck = false;
        
        #endregion

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (isCheck)
            {
                isCheck = false;
                if (Coin51.Text.Equals("") || Coin52.Text.Equals("") || Coin53.Text.Equals("") || Coin101.Text.Equals("") || Coin102.Text.Equals("") || Coin103.Text.Equals("") || Coin151.Text.Equals("") || Coin152.Text.Equals("") || Coin20.Text.Equals("") || Behavior1.Text.Equals("") || Behavior2.Text.Equals("") || Behavior3.Text.Equals("") || Behavior4.Text.Equals(""))
                {
                    MessageBoxResult result = MessageBox.Show("There are empty fields, Do you wish to continue with out them ?", "Warning!", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.No) return;
                }
                LogInInformation.Child_name = ChildName.Text;
                LogInInformation.Behavior_name = BehaviorName.Text;
                RewardApp reward = new RewardApp();
                reward.Show();
                this.Close();
                return;
            }

            if (isFillOut())
            {
                if (!isChildExist() && !isBehaviorExist())
                {
                    DBConnection.insertChild(LogInInformation.Username, ChildName.Text, Birthdate.SelectedDate);
                    DBConnection.insertBehavior(LogInInformation.Username, BehaviorName.Text, Behavior1.Text, Behavior2.Text, Behavior3.Text, Behavior4.Text, Coin51.Text, Coin52.Text, Coin53.Text, Coin101.Text, Coin102.Text, Coin103.Text, Coin151.Text, Coin152.Text, Coin20.Text);
                    MessageBox.Show("You just create new information for your Child and new Behavior Group.", "New Child and Behavior Group.", MessageBoxButton.OK, MessageBoxImage.Information);
                    isCheck = true;
                    updateComboBox();
                    return;
                }
                else if (!isChildExist() && isBehaviorExist())
                {
                    DBConnection.insertChild(LogInInformation.Username, ChildName.Text, Birthdate.SelectedDate);
                    updateExistBehavior();
                    isCheck = true;
                    return;
                }
                else if (isChildExist() && !isBehaviorExist())
                {
                    DBConnection.insertBehavior(LogInInformation.Username, BehaviorName.Text, Behavior1.Text, Behavior2.Text, Behavior3.Text, Behavior4.Text, Coin51.Text, Coin52.Text, Coin53.Text, Coin101.Text, Coin102.Text, Coin103.Text, Coin151.Text, Coin152.Text, Coin20.Text);
                    updateExistChild();
                    isCheck = true;
                    return;
                }
                else if (isChildExist() && isBehaviorExist())
                {
                    isCheck = true;
                    if (isChildTheSame() && isBehaviorTheSame())
                    {
                        Submit_Click(sender, e);
                    }
                    updateExistChild();
                    updateExistBehavior();
                    updateComboBox();
                    return;
                }
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

        private void ChildName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ChildName.Text = (string)ChildName.SelectedItem;
            autoFillChild(ChildName.Text);
        }

        private void BehaviorName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BehaviorName.Text = (string)BehaviorName.SelectedItem;
            autoFillBehavior(BehaviorName.Text);
        }

        private void isModified(object sender, TextChangedEventArgs e)
        {
            isCheck = false;
        }

        private void isModified(object sender, TextCompositionEventArgs e)
        {
            isCheck = false;
        }
    }
}
