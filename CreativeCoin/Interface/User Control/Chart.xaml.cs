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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Interface
{
    /// <summary>
    /// Interaction logic for Chart.xaml
    /// </summary>
    public partial class Chart : UserControl
    {
        public Chart()
        {
            InitializeComponent();

        }

        #region Properties

        public Label XLabel
        {
            get;
            set;
        }


        public Label YLabel
        {
            get;
            set;
        }

        



        #endregion

        #region Dependency Properties



        #endregion

        #region Events



        #endregion



    }
}
