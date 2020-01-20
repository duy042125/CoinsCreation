using System;
using System.Collections.Generic;
using System.Windows;
using Middleware;
using Middleware.Database_Component;

namespace Interface
{
    /// <summary>
    /// Interaction logic for ChartReport.xaml
    /// </summary>
    public partial class ChartReport : Window
    {
        public ChartReport(List<BarValue> valueList)
        {
            InitializeComponent();
            BarGraph.BarValueList = valueList;
            Information_Loaded();
        }

        private void Information_Loaded()
        {
            if(BarChart.barClicked != null)
            {
                Report fullReport = DBConnection.retrieveFullReportByKeys(LogInInformation.Username, LogInInformation.Child_name, BarChart.barClicked.date);
                ChildName.Content = fullReport.Child_name;
                Age.Content = DateTimeConverter.timeSpanToAge(DateTime.Now - DateTimeConverter.stringToDateTime(fullReport.birthdate));
                Date.Content = BarChart.barClicked.date;
                CoinEarned.Content = fullReport.coin_earned;

                BehaviorName.Content = fullReport.Behavior_name;
                Behavior1.Content = fullReport.behavior1;
                Behavior2.Content = fullReport.behavior2;
                Behavior3.Content = fullReport.behavior3;
                Behavior4.Content = fullReport.behavior4;
                Note.Text = fullReport.note;
            }
        }

        private void SeeReport_Click(object sender, RoutedEventArgs e)
        {
            Information_Loaded();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            ReportShop reportShop = new ReportShop();
            reportShop.Show();
            this.Close();
        }
    }
}
