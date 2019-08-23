using System;
using System.Windows;
using Middleware;
using Middleware.Database_Component;


namespace Interface
{
    /// <summary>
    /// Interaction logic for Admin_Report.xaml
    /// </summary>
    public partial class Admin_Report : Window
    {
        public Admin_Report()
        {
            InitializeComponent();
        }

        private void AccountTable_Loaded(object sender, RoutedEventArgs e)
        {
            ReportTable.ItemsSource = DBConnection.retrieveAllReports();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (ReportTable.IsReadOnly) ReportTable.IsReadOnly = false;
            else ReportTable.IsReadOnly = true;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Report theReport = (Report)ReportTable.SelectedItem;
                DBConnection.updateReportByKeys(theReport);
                MessageBox.Show("Data Saved", "Saved Data", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (ReportTable.IsReadOnly)
            {
                Admin_MainMenu adminMainMenu = new Admin_MainMenu();
                adminMainMenu.Show();
                this.Close();
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Your data is unsave. Do you want to go back ?", "Unsaved Data", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    ReportTable.IsReadOnly = true;
                    Back_Click(sender, e);
                }
                else return;
            }
        }
    }
}
