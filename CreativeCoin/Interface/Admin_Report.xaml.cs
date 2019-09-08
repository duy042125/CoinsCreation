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
        private bool isSave;
        private bool isEdit;

        public Admin_Report()
        {
            InitializeComponent();
            isSave = false;
            isEdit = false;
        }

        private void AccountTable_Loaded(object sender, RoutedEventArgs e)
        {
            ReportTable.ItemsSource = DBConnection.retrieveAllReports();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (ReportTable.IsReadOnly)
            {
                ReportTable.IsReadOnly = false;
                isEdit = true;
            }
            else
            {
                ReportTable.IsReadOnly = true;
                isEdit = false;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Report report = (Report)ReportTable.SelectedItem;
                if (report != null)
                {
                    DBConnection.updateReportByKeys(report);
                    MessageBox.Show("Data Saved", "Saved Data", MessageBoxButton.OK, MessageBoxImage.Information);
                    isSave = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (isEdit)
            {
                if (!isSave)
                {
                    MessageBoxResult result = MessageBox.Show("Your data is unsave. Do you want to go back ?", "Unsaved Data", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.Yes) ReportTable.IsReadOnly = true;
                    else return;
                }
            }
            Admin_MainMenu adminMainMenu = new Admin_MainMenu();
            adminMainMenu.Show();
            this.Close();
        }
    }
}
