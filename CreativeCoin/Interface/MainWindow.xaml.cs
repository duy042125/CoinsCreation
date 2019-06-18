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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Interface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            FillOut fillOut = new FillOut();
            fillOut.Show();
            this.Close();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            NewAccount newAccount = new NewAccount();
            newAccount.Show();
            this.Close();
        }

        private void Forget_Click(object sender, RoutedEventArgs e)
        {
            ForgetAccount forgetAccount = new ForgetAccount();
            forgetAccount.Show();
            this.Close();
        }
    }
}