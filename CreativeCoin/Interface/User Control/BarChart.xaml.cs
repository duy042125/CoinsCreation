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
    /// Interaction logic for BarChart.xaml
    /// </summary>
    public partial class BarChart : UserControl
    {
        public BarChart()
        {
            InitializeComponent();
            this.Loaded += this.Column_Load;
        }

        static BarChart()
        {
            AxisColorProperty = DependencyProperty.Register("AxisColor", typeof(Brush), typeof(BarChart), new FrameworkPropertyMetadata(Brushes.Black, new PropertyChangedCallback(AxisColorChange)));
            YAxisLabelColorProperty = DependencyProperty.Register("YAxisLabelColor", typeof(Brush), typeof(BarChart), new FrameworkPropertyMetadata(Brushes.Black, new PropertyChangedCallback(YAxisLabelColorChange)));
            YAxisValueColorProperty = DependencyProperty.Register("YAxisValueColor", typeof(Brush), typeof(BarChart), new FrameworkPropertyMetadata(Brushes.Black, new PropertyChangedCallback(YAxisValueColorChange)));
            XAxisLabelColorProperty = DependencyProperty.Register("XAxisLabelColor", typeof(Brush), typeof(BarChart), new FrameworkPropertyMetadata(Brushes.Black, new PropertyChangedCallback(XAxisLabelColorChange)));
            BarColorProperty = DependencyProperty.Register("BarColor", typeof(Brush), typeof(BarChart), new FrameworkPropertyMetadata(Brushes.Black, new PropertyChangedCallback(BarColorChange)));
            BarValueListProperty = DependencyProperty.Register("BarValueList", typeof(List<int>), typeof(BarChart), new FrameworkPropertyMetadata(null, new PropertyChangedCallback(BarValueListChange)));
            BarLabelListProperty = DependencyProperty.Register("BarLabelList", typeof(List<string>), typeof(BarChart), new FrameworkPropertyMetadata(null, new PropertyChangedCallback(BarLabelListChange)));
        }

        #region Dependency Properties
        
        public static readonly DependencyProperty AxisColorProperty;

        public static readonly DependencyProperty YAxisLabelColorProperty;

        public static readonly DependencyProperty YAxisValueColorProperty;

        public static readonly DependencyProperty XAxisLabelColorProperty;

        public static readonly DependencyProperty BarColorProperty;

        public static readonly DependencyProperty BarValueListProperty;

        public static readonly DependencyProperty BarLabelListProperty;

        #endregion

        #region Properties

        public Brush AxisColor
        {
            get { return (Brush)GetValue(AxisColorProperty); }
            set { SetValue(AxisColorProperty, value); }
        }

        public Brush YAxisLabelColor
        {
            get { return (Brush)GetValue(YAxisLabelColorProperty); }
            set { SetValue(YAxisLabelColorProperty, value); }
        }

        public Brush YAxisValueColor
        {
            get { return (Brush)GetValue(YAxisValueColorProperty); }
            set { SetValue(YAxisValueColorProperty, value); }
        }

        public Brush XAxisLabelColor
        {
            get { return (Brush)GetValue(XAxisLabelColorProperty); }
            set { SetValue(XAxisLabelColorProperty, value); }
        }

        public Brush BarColor
        {
            get { return (Brush)GetValue(BarColorProperty); }
            set { SetValue(BarColorProperty, value); }
        }
        
        public List<int> BarValueList
        {
            get { return (List<int>)GetValue(BarValueListProperty); }
            set { SetValue(BarValueListProperty, value); }
        }

        public List<string> BarLabelList
        {
            get { return (List<string>)GetValue(BarLabelListProperty); }
            set { SetValue(BarLabelListProperty, value); }
        }

        #endregion

        #region Events

        private static void AxisColorChange(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            BarChart barChart = obj as BarChart;
            if (barChart != null)
            {
                barChart.YAxis.Stroke = barChart.AxisColor;
                barChart.XAxis.Stroke = barChart.AxisColor;
            }
        }

        private static void YAxisLabelColorChange(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            BarChart barChart = obj as BarChart;
            if (barChart != null)
            {
                barChart.YAxisLabel.Foreground = barChart.YAxisLabelColor;
            }
        }

        private static void YAxisValueColorChange(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            BarChart barChart = obj as BarChart;
            if (barChart != null)
            {
                barChart.YAxisValue0.Foreground = barChart.YAxisValueColor;
                barChart.YAxisValue1.Foreground = barChart.YAxisValueColor;
                barChart.YAxisValue2.Foreground = barChart.YAxisValueColor;
                barChart.YAxisValue3.Foreground = barChart.YAxisValueColor;
                barChart.YAxisValue4.Foreground = barChart.YAxisValueColor;
                barChart.YAxisValue5.Foreground = barChart.YAxisValueColor;
            }
        }

        private static void XAxisLabelColorChange(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            BarChart barChart = obj as BarChart;
            if (barChart != null)
            {
                foreach(Bar bar in barChart.BarStackPanel.Children)
                {
                    bar.LabelColor = barChart.XAxisLabelColor;
                }
            }
        }

        private static void BarColorChange(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            BarChart barChart = obj as BarChart;
            if (barChart != null)
            {
                foreach (Bar bar in barChart.BarStackPanel.Children)
                {
                    bar.BarColor = barChart.BarColor;
                }
            }
        }

        private static void BarValueListChange(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            BarChart barChart = obj as BarChart;
            if (barChart != null)
            {
                barChart.BarValueList = barChart.BarValueList;
            }
        }

        private static void BarLabelListChange(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            BarChart barChart = obj as BarChart;
            if (barChart != null)
            {
                barChart.BarLabelList = barChart.BarLabelList;
            }
        }

        #endregion

        #region Methods

        private void Column_Load(object sender, RoutedEventArgs e)
        {
            InitializeBars();
        }

        private void InitializeBars()
        {
            BarStackPanel.Children.Clear();
            
            for (int i = 0; i < BarValueList.Count; i++)
            {
                Bar bar = new Bar();
                bar.BarColor = BarColor;
                bar.Value = BarValueList[i];
                bar.Ratio = (int)(Height / 7);
                bar.LabelContent = BarLabelList[i];
                bar.LabelColor = XAxisLabelColor;

                BarStackPanel.Children.Insert(i, bar);
            }
        }

        #endregion

    }
}
