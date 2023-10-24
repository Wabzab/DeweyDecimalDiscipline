using DeweyDecimalDiscipline.Content;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

namespace DeweyDecimalDiscipline.Pages
{
    /// <summary>
    /// Interaction logic for IdentifyingAreasPage.xaml
    /// </summary>
    public partial class IdentifyingAreasPage : Page
    {

        public const int MATCH_WIDTH = 4;
        public const int CLEAR_WIDTH = 4;

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

            // Load content into the matching columns
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

            matchA.ItemsSource = MatchA;
            matchB.ItemsSource = MatchB;

        }

        private void SaveMatchedPair(MatchedPair matchedPair)
        {
            List<MatchedPair> removedPairs = new List<MatchedPair>();
            foreach (var pair in MatchedPairs)
            {
                if(pair.A == matchedPair.A || pair.B == matchedPair.B)
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
            foreach (var pair in MatchedPairs)
            {
                Debug.WriteLine("{0} : {1}", pair.A, pair.B);
            }
            Debug.WriteLine("Successfully saved pair: {0}-{1}", matchedPair.A, matchedPair.B);
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
                        if(CurrentMatchedPair.A != string.Empty && CurrentMatchedPair.itemA != null)
                        {
                            updateListItemBrush(CurrentMatchedPair.itemA, (SolidColorBrush)FindResource("duck-egg"), CLEAR_WIDTH);
                        }
                        //obj.
                        KeyValuePair<string, string> item = (KeyValuePair<string, string>)matchA.SelectedItem;
                        CurrentMatchedPair.A = item.Key;
                        CurrentMatchedPair.itemA = (ListBoxItem)obj;
                        updateListItemBrush(CurrentMatchedPair.itemA, CurrentMatchedPair.brush, MATCH_WIDTH);

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
                        if (CurrentMatchedPair.B != string.Empty && CurrentMatchedPair.itemB != null)
                        {
                            updateListItemBrush(CurrentMatchedPair.itemB, (SolidColorBrush)FindResource("duck-egg"), CLEAR_WIDTH);
                        }
                        KeyValuePair<string, string> item = (KeyValuePair<string, string>)matchB.SelectedItem;
                        CurrentMatchedPair.B = item.Key;
                        CurrentMatchedPair.itemB = (ListBoxItem)obj;
                        updateListItemBrush(CurrentMatchedPair.itemB, CurrentMatchedPair.brush, MATCH_WIDTH);

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
