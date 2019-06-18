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
    /// Interaction logic for RewardApp.xaml
    /// </summary>
    public partial class RewardApp : Window
    {

        public RewardApp(Window fillOut)
        {
            InitializeComponent();

        }
        
        public int TotalStar { get{return Behavior1.Value + Behavior2.Value + Behavior3.Value + Behavior4.Value;} }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (TotalStar >= 0 && TotalStar < 10) Progress.Text = "Not Enough";
            else if (TotalStar >= 10 && TotalStar < 12) Progress.Text = "Very Low";
            else if (TotalStar >= 12 && TotalStar < 15) Progress.Text = "Some";
            else if (TotalStar >= 15 && TotalStar < 18) Progress.Text = "Medium";
            else if (TotalStar >= 18 && TotalStar < 20) Progress.Text = "High";
            else if (TotalStar == 20) Progress.Text = "Very High";
        }
    }
}
