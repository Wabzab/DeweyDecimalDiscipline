using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Security.RightsManagement;
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
using System.Windows.Threading;
using DeweyDecimalDiscipline.Content;
using DeweyDecimalDiscipline.Data;
using DeweyDecimalDiscipline.Models;

namespace DeweyDecimalDiscipline.Pages
{
    /// <summary>
    /// Interaction logic for ReplaceBookPage.xaml
    /// </summary>
    public partial class ReplaceBookPage : Page
    {
        // Time Objects used to keep track of time
        DispatcherTimer timer {  get; set; }
        DateTime startTime { get; set; }
        TimeSpan time { get; set; }

        public List<CallNumber> originalList {  get; set; }
        public ObservableCollection<CallNumber> unsortedList { get; set; }
        public ObservableCollection<CallNumber> userSortedList { get; set; }


        public ReplaceBookPage()
        {
            InitializeComponent();
            // Store call number data in lists
            originalList = CallNumberHandler.GenerateCallNumbers(10);
            unsortedList = new ObservableCollection<CallNumber>(originalList);
            userSortedList = new ObservableCollection<CallNumber>();

            // Connect lists to item grid UI
            UnsortedCallNumbers.ItemsSource = unsortedList;
            SortedCallNumbers.ItemsSource = userSortedList;

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

        // Handle game over
        private void EndGame()
        {
            // Stop timer
            timer.Stop();
            // Record replacement score sheet data
            Replacement scoreSheet = new Replacement();
            scoreSheet.Time = time;
            scoreSheet.Date = startTime;
            scoreSheet.Score = CallNumberHandler.GetScore(new List<CallNumber>(userSortedList));
            // Inform user of result
            string endMessage = string.Format("You got {0}/10 in {1}s!", scoreSheet.Score, scoreSheet.Time.TotalSeconds.ToString("#.##"));
            MessageBox.Show(endMessage);
            // Store score sheet
            ReplacementDAO.Add(scoreSheet);
            // Navigate to home page
            LandingPage landingPage = new LandingPage();
            this.NavigationService.Navigate(landingPage);
        }

        // Handle call number transfer to sorted list on double click
        private void UnsortedCallNumbers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            // Get signal source
            DependencyObject obj = (DependencyObject)e.OriginalSource;

            // Find ListBoxItem that was double clicked
            while (obj != null && obj != UnsortedCallNumbers)
            {
                if (obj.GetType() == typeof(ListBoxItem))
                {
                    if (UnsortedCallNumbers.SelectedItem != null)
                    {
                        // Move call number to other list
                        userSortedList.Add((CallNumber)UnsortedCallNumbers.SelectedItem);
                        unsortedList.Remove((CallNumber)UnsortedCallNumbers.SelectedItem);
                        // Check game over
                        if (unsortedList.Count == 0)
                        {
                            EndGame();
                        }
                    }
                }
                obj = VisualTreeHelper.GetParent(obj);
            }
        }

        // Handle call number transfer to unsorted list on double click
        private void SortedCallNumbers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DependencyObject obj = (DependencyObject)e.OriginalSource;

            while (obj != null && obj != SortedCallNumbers)
            {
                if (obj.GetType() == typeof(ListBoxItem))
                {
                    if (SortedCallNumbers.SelectedItem != null)
                    {
                        unsortedList.Add((CallNumber)SortedCallNumbers.SelectedItem);
                        userSortedList.Remove((CallNumber)SortedCallNumbers.SelectedItem);
                    }
                }
                obj = VisualTreeHelper.GetParent(obj);
            }
        }
    }
}

