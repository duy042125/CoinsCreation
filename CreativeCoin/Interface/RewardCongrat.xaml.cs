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

namespace Interface
{
    /// <summary>
    /// Interaction logic for RewardCongrat.xaml
    /// </summary>
    public partial class RewardCongrat : Window
    {
        public RewardCongrat(List<string> boughtItems, int coinSpent)
        {
            InitializeComponent();
            ChildName.Content = LogInInformation.Child_name;

            if (boughtItems.Count == 0 || boughtItems == null)
            {
                Label item = new Label();
                item.Content = "None";
                item.Foreground = Brushes.Red;
                item.FontSize = 30;
                item.VerticalAlignment = VerticalAlignment.Top;
                item.HorizontalAlignment = HorizontalAlignment.Center;
                BoughtItemStackPanel.Children.Insert(0, item);
            }
            else
            {
                for (int i = 0; i < boughtItems.Count; i++)
                {
                    Label item = new Label();
                    item.Content = boughtItems[i];
                    item.Foreground = Brushes.Red;
                    item.FontSize = 30;
                    item.VerticalAlignment = VerticalAlignment.Top;
                    item.HorizontalAlignment = HorizontalAlignment.Center;
                    BoughtItemStackPanel.Children.Insert(i, item);
                }
            }
        }

        private void StackPanel_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
