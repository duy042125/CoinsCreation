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
    /// Interaction logic for ReportShop.xaml
    /// </summary>
    public partial class ReportShop : Window
    {
        public ReportShop()
        {
            InitializeComponent();
        }

        private void Information_Loaded(object sender, RoutedEventArgs e)
        {
            #region Report Load
            Child childLoader = DBConnection.retrieveChildByName(LogInInformation.Username, LogInInformation.Child_name);
            ChildName.Text = childLoader.Child_name;
            Age.Text = DateTimeConverter.timeSpanToString(DateTime.Now - childLoader.birthdate);
            Date.Text = DateTime.Now.ToString("MM/dd/yyyy");
            TotalCoin.Content = childLoader.total_coin;
            #endregion

            #region Reward Load
            Behavior behaviorLoader = DBConnection.retrieveBehaviorByName(LogInInformation.Behavior_name);
            Reward51.Content = behaviorLoader.star5_reward1;
            Reward52.Content = behaviorLoader.star5_reward2;
            Reward53.Content = behaviorLoader.star5_reward3;

            Reward101.Content = behaviorLoader.star5_reward1;
            Reward102.Content = behaviorLoader.star5_reward2;
            Reward103.Content = behaviorLoader.star5_reward3;

            Reward151.Content = behaviorLoader.star5_reward1;
            Reward152.Content = behaviorLoader.star5_reward2;

            Reward20.Content = behaviorLoader.star20_reward;
            #endregion
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            Note.Text = "";
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            DBConnection.insertReport(LogInInformation.Username, LogInInformation.Child_name, LogInInformation.Behavior_name, Date.SelectedDate, LogInInformation.coin_earned, Note.Text);
        }

        private void Chart_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Undo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Buy_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
