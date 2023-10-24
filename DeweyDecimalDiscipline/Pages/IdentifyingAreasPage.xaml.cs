using DeweyDecimalDiscipline.Content;
using DeweyDecimalDiscipline.Data;
using DeweyDecimalDiscipline.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace DeweyDecimalDiscipline.Pages
{
    /// <summary>
    /// Interaction logic for IdentifyingAreasPage.xaml
    /// </summary>
    public partial class IdentifyingAreasPage : Page
    {

        public const int MATCH_WIDTH = 4;
        public const int CLEAR_WIDTH = 4;

        // Time Objects used to keep track of time
        DispatcherTimer timer { get; set; }
        DateTime startTime { get; set; }
        TimeSpan time { get; set; }

        public List<CallNumber> CallNumbers { get; set; }
        public Dictionary<string, string> MatchA { get; set; }
        public Dictionary<string, string> MatchB { get; set; }
        public List<MatchedPair> MatchedPairs { get; set; } = new List<MatchedPair>();
        public MatchedPair? CurrentMatchedPair { get; set; }

        public IdentifyingAreasPage()
        {
            InitializeComponent();

            CallNumbers = CallNumberMatchHandler.GetCallNumbers();
            MatchA = new Dictionary<string, string>();
            MatchB = new Dictionary<string, string>();

            // Select random call number groups and descriptions
            Random random = new Random();
            for (int i = 0; i < 4; i++)
            {
                CallNumber callNumber = CallNumbers[random.Next(CallNumbers.Count)];
                CallNumbers.Remove(callNumber);

                MatchA.Add(callNumber.Name, callNumber.Description);
                MatchB.Add(callNumber.Name, callNumber.Description);
            }
            for (int i = 0; i < 3; i++)
            {
                CallNumber callNumber = CallNumbers[random.Next(CallNumbers.Count)];
                CallNumbers.Remove(callNumber);

                MatchB.Add(callNumber.Name, callNumber.Description);
            }

            // Randomise content and then visualise it
            matchA.ItemsSource = MatchA.OrderBy(x => random.Next()).ToDictionary(item => item.Key, item => item.Value);
            matchB.ItemsSource = MatchB.OrderBy(x => random.Next()).ToDictionary(item => item.Key, item => item.Value);

            // Start timer
            startTime = DateTime.Now;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.1);
            timer.Tick += timer_Tick;
            timer.Start();

        }

        // Update clock every timer tick
        void timer_Tick(object sender, EventArgs e)
        {
            time = DateTime.Now.Subtract(startTime);
            lblTime.Content = string.Format("{0}:{1}", (time.Minutes).ToString("D2"), (time.Seconds).ToString("D2"));
        }

        private void GameOver()
        {
            // Stop timer
            timer.Stop();
            // Record score sheet data
            
            // Inform user of result
            string endMessage = string.Format("You have finished the game!");
            MessageBox.Show(endMessage);
            // Store score sheet
            //ReplacementDAO.Add(scoreSheet);
            // Navigate to home page
            LandingPage landingPage = new LandingPage();
            this.NavigationService.Navigate(landingPage);
        }

        private void SaveMatchedPair(MatchedPair matchedPair)
        {
            List<MatchedPair> removedPairs = new List<MatchedPair>();
            foreach (var pair in MatchedPairs)
            {
                updateListItemBrush(pair.itemA, pair.brush, MATCH_WIDTH);
                updateListItemBrush(pair.itemB, pair.brush, MATCH_WIDTH);
                if (pair.A == matchedPair.A || pair.B == matchedPair.B)
                {
                    removedPairs.Add(pair);
                }
            }
            foreach (var pair in removedPairs)
            {
                if (pair.A != matchedPair.A) { updateListItemBrush(pair.itemA, (SolidColorBrush)FindResource("duck-egg"), CLEAR_WIDTH); }
                if (pair.B != matchedPair.B) { updateListItemBrush(pair.itemB, (SolidColorBrush)FindResource("duck-egg"), CLEAR_WIDTH); }
                MatchedPairs.Remove(pair);
            }
            MatchedPairs.Add(matchedPair);
            updateListItemBrush(matchedPair.itemA, matchedPair.brush, MATCH_WIDTH);
            updateListItemBrush(matchedPair.itemB, matchedPair.brush, MATCH_WIDTH);
            
            if(MatchedPairs.Count == 4)
            {
                GameOver();
            }
        }

        private void OnNameDoubleClick(object sender, RoutedEventArgs e)
        {
            DependencyObject obj = (DependencyObject)e.OriginalSource;

            while (obj != null && obj != matchA)
            {
                if (obj.GetType() == typeof(ListBoxItem))
                {
                    if (matchA.SelectedItem != null)
                    {
                        if(CurrentMatchedPair == null)
                        {
                            CurrentMatchedPair = new MatchedPair();
                        }
                        KeyValuePair<string, string> item = (KeyValuePair<string, string>)matchA.SelectedItem;
                        CurrentMatchedPair.A = item.Key;
                        CurrentMatchedPair.itemA = (ListBoxItem)obj;

                        if (CurrentMatchedPair.B != string.Empty)
                        {
                            SaveMatchedPair(CurrentMatchedPair);
                            matchA.SelectedIndex = -1;
                            matchB.SelectedIndex = -1;
                            CurrentMatchedPair = null;
                        }
                    }
                }
                obj = VisualTreeHelper.GetParent(obj);
            }
        }

        private void OnDescriptionDoubleClick(object sender, RoutedEventArgs e)
        {
            DependencyObject obj = (DependencyObject)e.OriginalSource;

            while (obj != null && obj != matchB)
            {
                if (obj.GetType() == typeof(ListBoxItem))
                {
                    if (matchB.SelectedItem != null)
                    {
                        if (CurrentMatchedPair == null)
                        {
                            CurrentMatchedPair = new MatchedPair();
                        }
                        KeyValuePair<string, string> item = (KeyValuePair<string, string>)matchB.SelectedItem;
                        CurrentMatchedPair.B = item.Key;
                        CurrentMatchedPair.itemB = (ListBoxItem)obj;

                        if (CurrentMatchedPair.A != string.Empty)
                        {
                            SaveMatchedPair(CurrentMatchedPair);
                            matchA.SelectedIndex = -1;
                            matchB.SelectedIndex = -1;
                            CurrentMatchedPair = null;
                        }
                    }
                }
                obj = VisualTreeHelper.GetParent(obj);
            }
        }

        public void updateListItemBrush(ListBoxItem item, Brush brush, int width)
        {
            // https://learn.microsoft.com/en-us/dotnet/desktop/wpf/data/how-to-find-datatemplate-generated-elements?view=netframeworkdesktop-4.8&redirectedfrom=MSDN
            ContentPresenter myContentPresenter = FindVisualChild<ContentPresenter>(item);
            DataTemplate myDataTemplate = myContentPresenter.ContentTemplate;
            Border border = (Border)myDataTemplate.FindName("border", myContentPresenter);
            border.BorderBrush = brush;
            border.BorderThickness = new Thickness(width);
        }

        private childItem FindVisualChild<childItem>(DependencyObject obj) where childItem : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is childItem)
                {
                    return (childItem)child;
                }
                else
                {
                    childItem childOfChild = FindVisualChild<childItem>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }

    }

    public class MatchedPair {
        public string A { get; set; } = string.Empty;
        public string B { get; set; } = string.Empty;
        public ListBoxItem? itemA { get; set; }
        public ListBoxItem? itemB { get; set; }
        public Brush brush { get; set; }

        public MatchedPair() {
            Random r = new Random();
            Color color = Color.FromRgb((byte)r.Next(255), (byte)r.Next(255), (byte)r.Next(255));
            brush = new SolidColorBrush(color);
        }
    }
}
