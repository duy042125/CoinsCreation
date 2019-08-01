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
    /// Interaction logic for ChartReport.xaml
    /// </summary>
    public partial class ChartReport : Window
    {
        public ChartReport(List<int> valueList, List<string> labelList, List<DateTime?> dateList)
        {
            InitializeComponent();
            BarGraph.BarValueList = valueList;
            BarGraph.BarLabelList = labelList;
            BarGraph.BarDateList = dateList;
            Information_Loaded();
        }

        private void Information_Loaded()
        {
            if(BarChart.barClicked != null)
            {
                Report fullReport = DBConnection.retrieveFullReportByKeys(LogInInformation.Username, LogInInformation.Child_name, LogInInformation.Behavior_name, BarChart.barClicked.date);
                ChildName.Content = fullReport.Child_name;
                Age.Content = DateTimeConverter.timeSpanToAge(DateTime.Now - fullReport.birthdate);
                Date.Content = DateTimeConverter.dateTimeToString(BarChart.barClicked.date);
                CoinEarned.Content = fullReport.coin_earned;

                BehaviorName.Content = fullReport.Behavior_name;
                Behavior1.Content = fullReport.behavior1;
                Behavior2.Content = fullReport.behavior2;
                Behavior3.Content = fullReport.behavior3;
                Behavior4.Content = fullReport.behavior4;
                Note.Text = fullReport.note;
            }
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            Information_Loaded();
        }
    }
}
