using DeweyDecimalDiscipline.Data;
using DeweyDecimalDiscipline.Models;
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

namespace DeweyDecimalDiscipline.Pages
{
    /// <summary>
    /// Interaction logic for LandingPage.xaml
    /// </summary>
    public partial class LandingPage : Page
    {
        public LandingPage()
        {
            InitializeComponent();
            DisplayReplaceScores();
        }


        private void DisplayReplaceScores()
        {
            List<Replacement> replacements = ReplacementDAO.GetAll();
            List<ReplaceScore> scores = new List<ReplaceScore>();
            foreach (Replacement r in replacements)
            {
                scores.Add(new ReplaceScore() { Description = string.Format("{0}: {1} | {2}s", r.Date.ToString("dd/MM/yyyy"), r.Score.ToString("D2"), r.Time.TotalSeconds.ToString("#.##")) });
            }
            lbReplace.ItemsSource = scores;
        }

        // Handle Book Replacement Task Selection
        private void btnReplaceBook_Click(object sender, RoutedEventArgs e)
        {   
            ReplaceBookPage replaceBookPage = new ReplaceBookPage();
            NavigationService.Navigate(replaceBookPage);
        }

        // Handle Area Identification Task Selection
        private void btnIdentifyArea_Click(object sender, RoutedEventArgs e)
        {
            IdentifyingAreasPage identifyingAreasPage = new IdentifyingAreasPage();
            NavigationService.Navigate(identifyingAreasPage);
        }

        // Handle Call Number Quiz Task Selection
        private void btnFindCallNum_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to FindCallNumPage when necessary
            return;
        }
    }

    class ReplaceScore
    {
        public string Description { get; set; }
    }
}
