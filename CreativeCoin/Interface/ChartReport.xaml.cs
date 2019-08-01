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
        public ChartReport(List<int> valueList, List<string> labelList)
        {
            InitializeComponent();
            BarGraph.BarValueList = valueList;
            BarGraph.BarLabelList = labelList;
        }
    }
}
