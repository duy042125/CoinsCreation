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
        
        private static int totalCoin, previousCoinInCart;

        private void updateTotalCoin()
        {
            TotalCoin.Content = totalCoin;
        }

        private void Information_Loaded(object sender, RoutedEventArgs e)
        {
            #region Report Load
            Child childLoader = DBConnection.retrieveChildByName(LogInInformation.Username, LogInInformation.Child_name);
            ChildName.Text = childLoader.Child_name;
            Age.Text = DateTimeConverter.timeSpanToString(DateTime.Now - childLoader.birthdate);
            Date.Text = DateTime.Now.ToString("MM/dd/yyyy");
            TotalCoin.Content = childLoader.total_coin;
            totalCoin = childLoader.total_coin;
            #endregion

            #region Reward Load
            Behavior behaviorLoader = DBConnection.retrieveBehaviorByName(LogInInformation.Behavior_name);
            if (behaviorLoader.star5_reward1.Equals(""))
            {
                Reward51.Content = "(5 Coins) " + "None";
                Reward51.IsEnabled = false;
            }
            else Reward51.Content = "(5 Coins) " + behaviorLoader.star5_reward1;
            if (behaviorLoader.star5_reward2.Equals(""))
            {
                Reward52.Content = "(5 Coins) " + "None";
                Reward52.IsEnabled = false;
            }
            else Reward52.Content = "(5 Coins) " + behaviorLoader.star5_reward2;
            if (behaviorLoader.star5_reward3.Equals(""))
            {
                Reward53.Content = "(5 Coins) " + "None";
                Reward53.IsEnabled = false;
            }
            else Reward53.Content = "(5 Coins) " + behaviorLoader.star5_reward3;
            if (behaviorLoader.star10_reward1.Equals(""))
            {
                Reward101.Content = "(10 Coins) " + "None";
                Reward101.IsEnabled = false;
            }
            else Reward101.Content = "(10 Coins) " + behaviorLoader.star10_reward1;
            if (behaviorLoader.star10_reward2.Equals(""))
            {
                Reward102.Content = "(10 Coins) " + "None";
                Reward102.IsEnabled = false;
            }
            else Reward102.Content = "(10 Coins) " + behaviorLoader.star10_reward2;
            if (behaviorLoader.star10_reward3.Equals(""))
            {
                Reward103.Content = "(10 Coins) " + "None";
                Reward103.IsEnabled = false;
            }
            else Reward103.Content = "(10 Coins) " + behaviorLoader.star10_reward3;
            if (behaviorLoader.star15_reward1.Equals(""))
            {
                Reward151.Content = "(15 Coins) " + "None";
                Reward151.IsEnabled = false;
            }
            else Reward151.Content = "(15 Coins) " + behaviorLoader.star15_reward1;
            if (behaviorLoader.star15_reward2.Equals(""))
            {
                Reward152.Content = "(15 Coins) " + "None";
                Reward152.IsEnabled = false;
            }
            else Reward152.Content = "(15 Coins) " + behaviorLoader.star15_reward2;
            if (behaviorLoader.star20_reward.Equals(""))
            {
                Reward20.Content = "(20 Coins) " + "None";
                Reward20.IsEnabled = false;
            }
            else Reward20.Content = "(20 Coins) " + behaviorLoader.star20_reward;
            #endregion
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            Note.Text = "";
            uncheckAll();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            DBConnection.insertReport(LogInInformation.Username, LogInInformation.Child_name, LogInInformation.Behavior_name, Date.SelectedDate, LogInInformation.coin_earned, Note.Text);
        }

        private void Chart_Click(object sender, RoutedEventArgs e)
        {
            // import a chart report with saparate window
        }

        private void Buy_Click(object sender, RoutedEventArgs e)
        {
            DBConnection.useCoin(LogInInformation.Username, LogInInformation.Child_name, coinInCart());
            totalCoin -= coinInCart();
            previousCoinInCart = coinInCart();
            uncheckAll();
            updateTotalCoin();
        }

        private void Undo_Click(object sender, RoutedEventArgs e)
        {
            DBConnection.addCoin(LogInInformation.Username, LogInInformation.Child_name, previousCoinInCart);
            totalCoin += previousCoinInCart;
            previousCoinInCart = 0;
            updateTotalCoin();
        }
        private int coinInCart()
        {
            int totalCoin = 0;
            totalCoin += (bool)Reward51.IsChecked ? 5 : 0;
            totalCoin += (bool)Reward52.IsChecked ? 5 : 0;
            totalCoin += (bool)Reward53.IsChecked ? 5 : 0;
            totalCoin += (bool)Reward101.IsChecked ? 10 : 0;
            totalCoin += (bool)Reward102.IsChecked ? 10 : 0;
            totalCoin += (bool)Reward103.IsChecked ? 10 : 0;
            totalCoin += (bool)Reward151.IsChecked ? 15 : 0;
            totalCoin += (bool)Reward152.IsChecked ? 15 : 0;
            totalCoin += (bool)Reward20.IsChecked ? 20 : 0;
            return totalCoin;
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
    }
}
