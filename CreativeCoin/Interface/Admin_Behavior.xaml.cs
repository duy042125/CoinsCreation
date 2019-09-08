﻿using System;
using System.Windows;
using Middleware;
using Middleware.Database_Component;

namespace Interface
{
    /// <summary>
    /// Interaction logic for Admin_Behavior.xaml
    /// </summary>
    public partial class Admin_Behavior : Window
    {
        public Admin_Behavior()
        {
            InitializeComponent();
            isSave = false;
    }

        private void AccountTable_Loaded(object sender, RoutedEventArgs e)
        {
            BehaviorTable.ItemsSource = DBConnection.retrieveAllBehaviors();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (BehaviorTable.IsReadOnly) BehaviorTable.IsReadOnly = false;
            else BehaviorTable.IsReadOnly = true;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Behavior behavior = (Behavior)BehaviorTable.SelectedItem;
                DBConnection.updateBehaviorByName(behavior);
                MessageBox.Show("Data Saved", "Saved Data", MessageBoxButton.OK, MessageBoxImage.Information);
                isSave = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool isSave;

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (!isSave)
            {
                MessageBoxResult result = MessageBox.Show("Your data is unsave. Do you want to go back ?", "Unsaved Data", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes) BehaviorTable.IsReadOnly = true;
                else return;
            }
            Admin_MainMenu adminMainMenu = new Admin_MainMenu();
            adminMainMenu.Show();
            this.Close();
        }
    }
}
