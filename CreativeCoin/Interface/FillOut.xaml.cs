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

namespace Interface
{
    /// <summary>
    /// Interaction logic for FillOut.xaml
    /// </summary>
    public partial class FillOut : Window
    {
        public FillOut()
        {
            InitializeComponent();
        }


        private void Tip_Click(object sender, RoutedEventArgs e) // this might need a custom window for it.
        {
            string message = "5 coins (1 week of perfect behavior) = favorite meal, ice cream, a dollar store item, craft/bake together \n" +
                "10 coins (2 weeks of perfect behavior) = extra video game hour, extra TV hour, one chore-free night \n" +
                "15 coins (3 weeks of perfect behavior) = sleepover, movie, choice of restauraunt, download game app \n" +
                "20 coins (4 weeks of perfect hehavior) = trip to FunZone, trip to Edge Waterpark, new console game";
            MessageBox.Show(message, "Tip", MessageBoxButton.OK, MessageBoxImage.Information);
        }


        private void Back_Click(object sender, RoutedEventArgs e)
        {

        }


        private void Next_Click(object sender, RoutedEventArgs e)
        {
            RewardApp reward = new RewardApp(this);
            reward.Show();
            this.Close();
        }
    }
}
