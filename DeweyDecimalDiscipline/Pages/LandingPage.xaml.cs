using DeweyDecimalDiscipline.Content.Acheivements;
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

        private List<Replacement> replacements = new List<Replacement>();
        private List<Identifying> identifyings = new List<Identifying>();

        public LandingPage()
        {
            InitializeComponent();
            DisplayScores();
            DisplayAchievements();
        }

        // Updates the scores view with most recent scores for the task
        private void DisplayScores()
        {
            replacements = ReplacementDAO.GetAll();
            List<ScoreDescription> rScores = new List<ScoreDescription>();
            foreach (Replacement r in replacements)
            {
                rScores.Add(new ScoreDescription(
                    r.Date.ToString("dd/MM/yyyy"), 
                    (r.Score/10.0*100).ToString()+"%",
                    r.Time.TotalSeconds.ToString("#.##")+"s"
                    ));
            }
            lbReplace.ItemsSource = rScores;

            identifyings = IdentifyingDAO.GetAll();
            List<ScoreDescription> iScores = new List<ScoreDescription>();
            foreach (Identifying i in identifyings)
            {
                iScores.Add(new ScoreDescription(
                    i.Date.ToString("dd/MM/yyyy"),
                    (i.Score / 4.0 * 100).ToString() + "%",
                    i.Time.TotalSeconds.ToString("#.##")+"s"
                    ));
            }
            lbIdentify.ItemsSource = iScores;
        }

        // Updates the achievements view with locked and unlocked achievements for the task
        private void DisplayAchievements()
        {
            lbReplaceAchievements.ItemsSource = ReplaceAchievements.GetAchievements(replacements);
            lbIdentifyAchievements.ItemsSource = IdentifyAchievements.GetAchievements(identifyings);
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
            FindingCallNumbersPage findingCallNumbersPage = new FindingCallNumbersPage();
            NavigationService.Navigate(findingCallNumbersPage);
        }
    }

    // Holds information about the scoresheet for tasks for displaying in a list box
    class ScoreDescription
    {
        public string Date { get; set; }
        public string Time { get; set; }
        public string Score { get; set; }
        public ScoreDescription(string date, string score, string time) 
        { 
            Date = date;
            Time = time;
            Score = score;
        }
    }
}
