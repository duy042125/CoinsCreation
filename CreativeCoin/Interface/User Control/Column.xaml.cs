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

namespace Interface.User_Control
{
    /// <summary>
    /// Interaction logic for Column.xaml
    /// </summary>
    public partial class Column : UserControl
    {
        public Column()
        {
            InitializeComponent();

            #region Bar Setup

            Bar.Fill = BarColor;
            Bar.Height = Ratio * Value;
            Bar.Width = Ratio;

            #endregion

            #region Label Setup

            Label.Content = LabelContent;
            Label.Foreground = LabelColor;
            Label.Width = Ratio;

            #endregion

        }

        static Column()
        {
            BarColorProperty = DependencyProperty.Register("BarColor", typeof(Brush), typeof(Column));
            ValueProperty = DependencyProperty.Register("Value", typeof(int), typeof(Column));
            RatioProperty = DependencyProperty.Register("Ratio", typeof(int), typeof(Column));
            LabelContentProperty = DependencyProperty.Register("LabelContent", typeof(string), typeof(Column));
            LabelColorProperty = DependencyProperty.Register("LabelColor", typeof(Brush), typeof(Column));
            LabelFontSizeProperty = DependencyProperty.Register("LabelFontSize", typeof(int), typeof(Column));
        }

        #region Dependency Properties

        public static readonly DependencyProperty BarColorProperty;         //Brush

        public static readonly DependencyProperty ValueProperty;            //int   Value = (1 - 5)

        public static readonly DependencyProperty RatioProperty;            //int

        public static readonly DependencyProperty LabelContentProperty;     //string

        public static readonly DependencyProperty LabelColorProperty;       //Brush

        public static readonly DependencyProperty LabelFontSizeProperty;

        #endregion

        #region Properties

        public Brush BarColor
        {
            get { return (Brush)GetValue(BarColorProperty); }
            set { SetValue(BarColorProperty, value); }
        }

        public int Ratio
        {
            get { return (int)GetValue(RatioProperty); }
            set { SetValue(RatioProperty, value); }
        }

        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public string LabelContent
        {
            get { return (string)GetValue(LabelContentProperty); }
            set { SetValue(LabelContentProperty, value); }
        }

        public Brush LabelColor
        {
            get { return (Brush)GetValue(LabelColorProperty); }
            set { SetValue(LabelColorProperty, value); }
        }

        public int LabelFontSize
        {
            get { return (int)GetValue(LabelFontSizeProperty); }
            set { SetValue(LabelFontSizeProperty, value); }
        }
        #endregion

        #region Events

        // Option Event Here when click in the column and the color will change as well as appear in the report section.

        #endregion
    }
}
