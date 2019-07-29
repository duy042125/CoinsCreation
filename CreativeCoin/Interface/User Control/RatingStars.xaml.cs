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
    /// Interaction logic for RatingStars.xaml
    /// </summary>
    public partial class RatingStars : UserControl
    {
        SolidColorBrush fillStar = new SolidColorBrush(Color.FromRgb(214, 214, 67));
        SolidColorBrush clearStar = new SolidColorBrush(Color.FromRgb(0, 0, 0));
        private int starNum;

        System.Media.SoundPlayer starSound;
        
        
        public RatingStars()
        {
            InitializeComponent();
            starNum = 0;
            starSound = new System.Media.SoundPlayer(Interface.Properties.Resources.star);
        }

        #region Mouse Enter Stars

        private void Star_MouseEnter(object sender, MouseEventArgs e)
        {
            fillOneStar();
        }

        #endregion

        #region Mouse Leave Stars

        private void Star_MouseLeave(object sender, MouseEventArgs e)
        {
            Star1.Fill = clearStar;
            Star2.Fill = clearStar;
            Star3.Fill = clearStar;
            Star4.Fill = clearStar;
            Star5.Fill = clearStar;
        }

        #endregion

        #region Mouse CLick Stars

        private void Star1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            starNum = 1;
            fillOneStar();
            starSound.Play();
        }

        private void Star2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            starNum = 2;
            fillTwoStar();
            starSound.Play();
        }

        private void Star3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            starNum = 3;
            fillThreeStar();
            starSound.Play();
        }

        private void Star4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            starNum = 4;
            fillFourStar();
            starSound.Play();
        }

        private void Star5_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            starNum = 5;
            fillFiveStar();
            starSound.Play();
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            switch (starNum)
            {
                case 1:
                    fillOneStar();
                    break;
                case 2:
                    fillTwoStar();
                    break;
                case 3:
                    fillThreeStar();
                    break;
                case 4:
                    fillFourStar();
                    break;
                case 5:
                    fillFiveStar();
                    break;
            }
        }

        #endregion

        #region Fill Stars

        private void fillOneStar()
        {
            Star1.Fill = fillStar;
            Star2.Fill = clearStar;
            Star3.Fill = clearStar;
            Star4.Fill = clearStar;
            Star5.Fill = clearStar;
        }

        private void fillTwoStar()
        {
            Star1.Fill = fillStar;
            Star2.Fill = fillStar;
            Star3.Fill = clearStar;
            Star4.Fill = clearStar;
            Star5.Fill = clearStar;
        }

        private void fillThreeStar()
        {
            Star1.Fill = fillStar;
            Star2.Fill = fillStar;
            Star3.Fill = fillStar;
            Star4.Fill = clearStar;
            Star5.Fill = clearStar;
        }

        private void fillFourStar()
        {
            Star1.Fill = fillStar;
            Star2.Fill = fillStar;
            Star3.Fill = fillStar;
            Star4.Fill = fillStar;
            Star5.Fill = clearStar;
        }

        private void fillFiveStar()
        {
            Star1.Fill = fillStar;
            Star2.Fill = fillStar;
            Star3.Fill = fillStar;
            Star4.Fill = fillStar;
            Star5.Fill = fillStar;
        }

        #endregion
        
        public int Value { get { return starNum; } } //get the star value
    }
}
