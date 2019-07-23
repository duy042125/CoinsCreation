﻿using System;
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
    /// Interaction logic for NewAccount.xaml
    /// </summary>
    public partial class NewAccount : Window
    {
        public NewAccount()
        {
            InitializeComponent();
        }

        #region Check

        private bool checkUsedUsername()
        {
            if (DBConnection.verifiedUsedUsername(Username.Text))
            {
                WarningUsername.Content = "This username is taken.";
                return false;
            }
            else
            {
                if (Username.Text.Length <= 3)
                {
                    WarningUsername.Content = "This username is too short.";
                    return false;
                }
                WarningUsername.Content = "This username is usable.";
                return true;
            }
        }

        private bool checkSSNFormat()
        {
            string ssn = SSN.Text;
            if (ssn.Length == 10)
            {
                for (int i = 0; i < ssn.Length; i++)
                {
                    if (ssn[i] > 57 || ssn[i] < 48)
                    {
                        WarningSSN.Content = "SSN format is wrong.";
                        return false;
                    }
                }
                return true;
            }
            WarningSSN.Content = "SSN format is wrong.";
            return false;
        }

        private bool checkPassword()
        {
            if (ConfirmPassword.Password.Equals(NewPassword.Password))
            {
                Match.Content = "Passwords match.";
                return true;
            }
                Match.Content = "Passwords don't match.";
                return false;
        }

        private bool checkUsedSSN()
        {
            if (DBConnection.verifiedUsedSSN(SSN.Text))
            {
                WarningSSN.Content = "This SSN is taken.";
                return false;
            }
            WarningSSN.Content = "This SSN is untaken.";
            return true;
        }

        private bool checkRequiredFields()
        {
            if (Username.Text.Equals("") || NewPassword.Password.Equals("") || ConfirmPassword.Password.Equals("") || SSN.Text.Equals("")) return false;
            return true;
        }

        #endregion

        #region Click

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            if (!checkRequiredFields())
            {
                MessageBox.Show("Fill up the required fields", "Required fields", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if(!checkSSNFormat())
            {
                return;
            }
            try
            {

                if (checkUsedUsername() && checkPassword() && checkUsedSSN())
                {
                    DBConnection.createAccount(Username.Text, Hashing.HashPassword(NewPassword.Password), newParentName.Text, Birthdate.SelectedDate, PhoneNumber.Text, SSN.Text);
                    MessageBox.Show("You created a new account.", "Creation Confirm", MessageBoxButton.OK, MessageBoxImage.Information);
                    MainWindow backToLogin = new MainWindow();
                    backToLogin.Show();
                    this.Close();
                    return;
                }
                MessageBox.Show("Check the required fields", "Required fields", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to go back ?", "Go Back", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                MainWindow main = new MainWindow();
                main.Show();
                this.Close();
            }
        }

        private void Check_Click(object sender, RoutedEventArgs e)
        {
            checkUsedUsername();
            if (!checkSSNFormat()) return;
            checkUsedSSN();
        }

        private void ConfirmPassword_Check(object sender, RoutedEventArgs e)
        {
            checkPassword();
        }

        #endregion
    }
}
