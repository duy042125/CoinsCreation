using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Middleware;
using Word = Microsoft.Office.Interop.Word;

namespace Interface
{
    /// <summary>
    /// Interaction logic for RewardCongrat.xaml
    /// </summary>
    public partial class RewardCongrat : Window
    {
        private string itemContent;
        private int coinSpent;

        public RewardCongrat(List<string> boughtItems, int theCoinSpent)
        {
            InitializeComponent();
            ChildName.Content = LogInInformation.Child_name;
            coinSpent = theCoinSpent;
            CoinSpent.Content = coinSpent;

            if (boughtItems.Count == 0 || boughtItems == null)
            {
                Label item = new Label();
                item.Content = "None";
                itemContent = "None";
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
                    itemContent += (boughtItems[i] + Environment.NewLine);
                }
            }
        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"CertificationTemplate.Docx"))
                {

                    #region Template

                    Word.Document certificationTemplate = null;
                    Word.Application wordAppTemplate = new Word.Application();
                    wordAppTemplate.ShowAnimation = false;
                    wordAppTemplate.Visible = false;

                    #endregion

                    #region Certification

                    Word.Document certification = null;
                    Word.Application wordApp = new Word.Application();
                    wordApp.ShowAnimation = false;
                    wordApp.Visible = false;

                    #endregion

                    #region Properties

                    object openFileName = AppDomain.CurrentDomain.BaseDirectory + @"CertificationTemplate.Docx";
                    string savePath = @"C:\Users\Public\CCApp";
                    object saveFileName = savePath + "\\" + DateTime.Now.Month + "_" + DateTime.Now.Day + "_" + DateTime.Now.Year + ".Docx";

                    #endregion

                    #region Copy Template

                    if (!Directory.Exists(savePath))
                    {
                        Directory.CreateDirectory(savePath);
                    }

                    certificationTemplate = wordAppTemplate.Documents.Open(ref openFileName);
                    certificationTemplate.Activate();
                    certificationTemplate.SaveAs(ref saveFileName);
                    certificationTemplate.Close();
                    wordAppTemplate.Quit();

                    #endregion
                    
                    certification = wordApp.Documents.Open(ref saveFileName);
                    certification.Activate();

                    Word.Find fnd = wordApp.ActiveWindow.Selection.Find;
                    string childName = LogInInformation.Child_name;
                    string coinSpentString = coinSpent.ToString();

                    FindAndReplace(wordApp, "<NameHere>", childName);
                    FindAndReplace(wordApp, "<CoinHere>", coinSpentString);
                    FindAndReplace(wordApp, "<BoughtItemHere>", itemContent);
                    certification.Save();
                    certification.Close();

                    MessageBox.Show("Successfully print out the certification");
                    wordApp.Quit();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        private void FindAndReplace(Word.Application certification, object findText, object replaceWithText)
        {
            #region Replace Options

            object matchCase = false;
            object matchWholeWord = true;
            object matchWildCards = false;
            object matchSoundsLike = false;
            object matchAllWordForms = false;
            object forward = true;
            object format = false;
            object matchKashida = false;
            object matchDiacritics = false;
            object matchAlefHamza = false;
            object matchControl = false;
            object read_only = false;
            object visible = true;
            object replace = 2;
            object wrap = 1;

            #endregion

            certification.Selection.Find.Execute(ref findText, ref matchCase, ref matchWholeWord, ref matchWildCards, 
                ref matchSoundsLike, ref matchAllWordForms, ref forward, ref wrap, ref format, ref replaceWithText, 
                ref replace, ref matchKashida, ref matchDiacritics, ref matchAlefHamza, ref matchControl);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}