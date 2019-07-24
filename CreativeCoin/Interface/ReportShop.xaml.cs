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
            // import a chart report with saparate window
        }

        private static int coinInCart = 0;
        

        private void Buy_Click(object sender, RoutedEventArgs e)
        {
            DBConnection.useCoin(LogInInformation.Username, LogInInformation.Child_name, coinInCart);
            LogInInformation.previousCart = coinInCart;
            coinInCart = 0;
            uncheckAll();
        }

        private void Undo_Click(object sender, RoutedEventArgs e)
        {
            DBConnection.addCoin(LogInInformation.Username, LogInInformation.Child_name, LogInInformation.previousCart);
            LogInInformation.previousCart = 0;
        }

        #region Checkbox Coin Count

        private void Reward5_Checked(object sender, RoutedEventArgs e)
        {
            coinInCart += 5;
        }

        private void Reward10_Checked(object sender, RoutedEventArgs e)
        {
            coinInCart += 10;
        }

        private void Reward15_Checked(object sender, RoutedEventArgs e)
        {
            coinInCart += 15;
        }

        private void Reward20_Checked(object sender, RoutedEventArgs e)
        {
            coinInCart += 20;
        }

        private void Reward5_Unchecked(object sender, RoutedEventArgs e)
        {
            coinInCart -= 5;
        }

        private void Reward10_Unchecked(object sender, RoutedEventArgs e)
        {
            coinInCart -= 10;
        }

        private void Reward15_Unchecked(object sender, RoutedEventArgs e)
        {
            coinInCart -= 15;
        }

        private void Reward20_Unchecked(object sender, RoutedEventArgs e)
        {
            coinInCart -= 20;
        }

        private void uncheckAll()
        {
            Reward51.IsChecked = false;
            Reward52.IsChecked = false;
            Reward53.IsChecked = false;

            Reward101.IsChecked = false;
            Reward102.IsChecked = false;
            Reward103.IsChecked = false;

            Reward151.IsChecked = false;
            Reward152.IsChecked = false;

            Reward20.IsChecked = false;
        }

        #endregion
    }
}
