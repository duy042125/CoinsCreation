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

        #endregion

        #region Events

        private static void BarColorChange(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            Bar bar = obj as Bar;
            if (bar != null)
            {
                /*
                bar.Column.Fill = bar.BarColor;
                bar.Column.Stroke = bar.BarColor;
               */
                
                Brush newColor = (Brush)e.NewValue;
                bar.Column.Fill = newColor;
                bar.Column.Stroke = newColor;
                
            }
        }

        private static void ValueAndRatioChange(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            Bar bar = obj as Bar;
            if (bar != null)
            {
                bar.Column.Height = bar.Ratio * bar.Value;
                bar.Column.Width = bar.Ratio;

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

        #endregion
    }
}
