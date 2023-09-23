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
using DeweyDecimalDiscipline.Content;

namespace DeweyDecimalDiscipline.Pages
{
    /// <summary>
    /// Interaction logic for ReplaceBookPage.xaml
    /// </summary>
    public partial class ReplaceBookPage : Page
    {

        public List<CallNumber> originalList {  get; set; }
        public ObservableCollection<CallNumber> unsortedList { get; set; }
        public ObservableCollection<CallNumber> userSortedList { get; set; }

        public ReplaceBookPage()
        {
            InitializeComponent();
            originalList = CallNumberHandler.GenerateCallNumbers(10);
            unsortedList = new ObservableCollection<CallNumber>(originalList);
            userSortedList = new ObservableCollection<CallNumber>();

            UnsortedCallNumbers.ItemsSource = unsortedList;
            SortedCallNumbers.ItemsSource = userSortedList;
        }


        private void UnsortedCallNumbers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DependencyObject obj = (DependencyObject)e.OriginalSource;

            while (obj != null && obj != UnsortedCallNumbers)
            {
                if (obj.GetType() == typeof(ListBoxItem))
                {
                    if (UnsortedCallNumbers.SelectedItem != null)
                    {
                        userSortedList.Add((CallNumber)UnsortedCallNumbers.SelectedItem);
                        unsortedList.Remove((CallNumber)UnsortedCallNumbers.SelectedItem);
                    }
                }
                obj = VisualTreeHelper.GetParent(obj);
            }
        }

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

