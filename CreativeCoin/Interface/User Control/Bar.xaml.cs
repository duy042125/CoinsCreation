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
    /// Interaction logic for Bar.xaml
    /// </summary>
    public partial class Bar : UserControl
    {
        #region Constructor Setup

        public Bar()
        {
            InitializeComponent();
            IsClick = false;
        }

        static Bar()
        {
            BarColorProperty = DependencyProperty.Register("BarColor", typeof(Brush), typeof(Bar), new PropertyMetadata(Brushes.Black, new PropertyChangedCallback(BarColorChange)));
            ValueProperty = DependencyProperty.Register("Value", typeof(int), typeof(Bar), new PropertyMetadata(0, new PropertyChangedCallback(ValueAndRatioChange)));
            RatioProperty = DependencyProperty.Register("Ratio", typeof(int), typeof(Bar), new PropertyMetadata(100, new PropertyChangedCallback(ValueAndRatioChange)));
            LabelContentProperty = DependencyProperty.Register("LabelContent", typeof(string), typeof(Bar), new PropertyMetadata("Label", new PropertyChangedCallback(LabelContentChange)));
            LabelColorProperty = DependencyProperty.Register("LabelColor", typeof(Brush), typeof(Bar), new PropertyMetadata(Brushes.Black, new PropertyChangedCallback(LabelColorChange)));
        }

        #endregion

        #region Dependency Properties

        public static readonly DependencyProperty BarColorProperty;         //Brush

        public static readonly DependencyProperty ValueProperty;            //int   Value = (1 - 5)

        public static readonly DependencyProperty RatioProperty;            //int

        public static readonly DependencyProperty LabelContentProperty;     //string

        public static readonly DependencyProperty LabelColorProperty;       //Brush
        
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

        public bool IsClick { get; set; }

        public string date { get; set; }

        #endregion

        #region Events

        private static void BarColorChange(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            Bar bar = obj as Bar;
            if (bar != null)
            {
                bar.ColumnBorder.Stroke = bar.BarColor;
                bar.ColumnBorder.Opacity = 0.5;
                bar.ColumnInside.Fill = bar.BarColor;

                /*
                Brush newColor = (Brush)e.NewValue;
                bar.Column.Fill = newColor;
                bar.Column.Stroke = newColor;
                */
            }
        }

        private static void ValueAndRatioChange(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            Bar bar = obj as Bar;
            if (bar != null)
            {
                bar.ColumnBorder.Width = bar.Ratio;
                bar.ColumnBorder.Height = bar.Ratio * bar.Value;

                bar.ColumnInside.Width = bar.Ratio - 4;
                bar.ColumnInside.Height = bar.Ratio * bar.Value - 4;

                bar.Label.Width = bar.Ratio;
                bar.Label.Height = bar.Ratio / 2;
                bar.Label.FontSize = bar.Ratio / 5;
            }
        }

        private static void LabelContentChange(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            Bar bar = obj as Bar;
            if (bar != null)
            {
                bar.Label.Content = bar.LabelContent;
            }
        }

        private static void LabelColorChange(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            Bar bar = obj as Bar;
            if (bar != null)
            {
                bar.Label.Foreground = bar.LabelColor;
            }
        }

            #region Mouse Events

        private void Bar_Click(object sender, MouseButtonEventArgs e)
        {
            if (IsClick) IsClick = false;
            else IsClick = true;
            BarChart.setUpBarClicked(this);
        }

        private void Bar_MouseEnter(object sender, MouseEventArgs e)
        {
            ColumnInside.Opacity = 0.5;
        }

        private void Bar_MouseLeave(object sender, MouseEventArgs e)
        {
            if (IsClick) ColumnInside.Opacity = 0.5;
            else ColumnInside.Opacity = 1;
        }

            #endregion

        #endregion


    }
}
